using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.Conventions;
using MarketDataHelper.DayCountFraction;
using MarketDataHelper.InterestRate;
using MarketDataHelper.MarketProduct;

namespace MarketDataHelper.Schedular
{
    public static class ScheduleHolderMaker
    {
        public static Dictionary<QuoteKey, ScheduleHolder> MakeScheduleHolderDictionary(DateTime asof, List<QuoteKey> qKeys, Dictionary<EProduct, Dictionary<InterestRateIndex, Convention>> conventions, Dictionary<string, List<DateTime>> holidays)
        {
            var result = new Dictionary<QuoteKey, ScheduleHolder>();
            foreach (var qkey in qKeys)
            {
                var convention = conventions[qkey.Product][qkey.IRIndex];
                result[qkey] = MakeScheduleHolder(asof, qkey, convention, holidays);
            }
            return result;
        }
        public static Dictionary<QuoteKey, ScheduleHolder> MakeScheduleHolderDictionaryFaster(DateTime asof, List<QuoteKey> qKeys, Dictionary<EProduct, Dictionary<InterestRateIndex, Convention>> conventions, Dictionary<string, List<DateTime>> holidays)
        {
            var swapQKeys = qKeys.Where(qkey => qkey.Product == EProduct.Swap).ToList();
            var swapConventions = conventions[EProduct.Swap];
            var result = GetSwapScheduleHolderFromLongest(asof, swapQKeys, swapConventions, holidays);

            var remainedQkeys = qKeys.Where(qkey => qkey.Product != EProduct.Swap).ToList();
            foreach (var qkey in remainedQkeys)
            {
                var convention = conventions[qkey.Product][qkey.IRIndex];
                result[qkey] = MakeScheduleHolder(asof, qkey, convention, holidays);
            }
            return result;
        }
        public static ScheduleHolder MakeScheduleHolder(DateTime asof, QuoteKey qkey, Convention convention, Dictionary<string, List<DateTime>> holidays)
        {
            switch (qkey.Product)
            {
                case EProduct.Depo:
                    return GetDepoScheduleHolder(asof);
                case EProduct.Fra:
                    return GetFraScheduleHolder(asof);
                case EProduct.Swap:
                    return GetSwapScheduleHolder(asof, qkey, convention as SwapConvention, holidays);
                case EProduct.Ois:
                    return GetOisScheduleHolder(asof, qkey, convention as OisConvention, holidays);
                default:
                    throw new NotImplementedException(string.Format("A method to make a ScheduleHolder of {0} is not implemented.", qkey.Product));
            }
        }
        private static ScheduleHolder GetDepoScheduleHolder(DateTime asof)
        {
            var fixedSideSchedules = new List<Schedule>();
            var floatSideSchedules = new List<Schedule>();

            return new ScheduleHolder(asof, fixedSideSchedules, floatSideSchedules);
        }

        private static ScheduleHolder GetFraScheduleHolder(DateTime asof)
        {
            var fixedSideSchedules = new List<Schedule>();
            var floatSideSchedules = new List<Schedule>();

            return new ScheduleHolder(asof, fixedSideSchedules, floatSideSchedules);
        }
        private static ScheduleHolder GetSwapScheduleHolder(DateTime asof, QuoteKey qkey, SwapConvention swapConvention, Dictionary<string, List<DateTime>> holidays)
        {
            var floatSideFreq = qkey.IRIndex.Tenor;
            var term = qkey.ProductTerm;
            var dcc = swapConvention.Dcc;
            var bdc = swapConvention.Bdc;
            var spotBcs = swapConvention.SpotBcs;
            var bcs = swapConvention.Bcs;
            var spotLag = swapConvention.SpotLag;
            var paymentLag = swapConvention.PaymentLag;
            var fixedSideFreq = swapConvention.Frequency;

            var isHolidaySpotBcs = SchedularUtils.GetIsHoliday(spotBcs, holidays);
            var isHolidayBcs = SchedularUtils.GetIsHoliday(bcs, holidays);

            var spotDate = SchedularUtils.GetNextNBusinessDay(asof, bdc, isHolidaySpotBcs);

            var fixedSideSchedules = GetSwapFixedSideSchedules(spotDate, dcc, bdc, fixedSideFreq, paymentLag, term, isHolidayBcs);
            var floatSideSchedules = GetSwapFloatSideSchedules(spotDate, dcc, bdc, floatSideFreq, paymentLag, term, isHolidayBcs);

            return new ScheduleHolder(asof, fixedSideSchedules, floatSideSchedules);
        }
        private static List<Schedule> GetSwapFloatSideSchedules(DateTime spotDate, EnumDcc dcc, EnumBdc bdc, double floatSideFreq, int paymentLag, double term, Func<DateTime, bool> isHoliday)
        {
            var floatSideSchedules = new List<Schedule>();
            var prevEndDate = spotDate;

            for (var i = 0; i < (int)(term / floatSideFreq); i++)
            {
                var startDate = prevEndDate;
                var endDate = SchedularUtils.GetNextNBusinessDay(spotDate.AddMonths((i + 1) * (int)floatSideFreq), bdc, isHoliday);
                var paymentDate = SchedularUtils.GetNextNBusinessDay(endDate, bdc, isHoliday, paymentLag);
                var dcf = DCFCalculator.GetDCF(startDate, endDate, dcc);

                floatSideSchedules.Add(new Schedule(startDate, endDate, paymentDate, dcf));
                prevEndDate = endDate;
            }

            return floatSideSchedules;
        }
        private static List<Schedule> GetSwapFixedSideSchedules(DateTime spotDate, EnumDcc dcc, EnumBdc bdc, double fixedSideFreq, int paymentLag, double term, Func<DateTime, bool> isHoliday)
        {
            return GetSwapFloatSideSchedules(spotDate, dcc, bdc, fixedSideFreq, paymentLag, term, isHoliday);
        }
        private static Dictionary<QuoteKey, ScheduleHolder> GetSwapScheduleHolderFromLongest(DateTime asof, List<QuoteKey> swapQKeys, Dictionary<InterestRateIndex, Convention> swapConventions, Dictionary<string, List<DateTime>> holidays)
        {
            var result = new Dictionary<QuoteKey, ScheduleHolder>();
            var orderedSwapQKeys = swapQKeys.OrderByDescending(qkey => qkey.ProductTerm).ToList();
            var longestSwapQKey = orderedSwapQKeys.FirstOrDefault();
            var remainedSwapQKeys = orderedSwapQKeys.Skip(1);

            var longestConvention = swapConventions[longestSwapQKey.IRIndex];
            result[longestSwapQKey] = MakeScheduleHolder(asof, longestSwapQKey, longestConvention, holidays);
            var longestFixedSideSchedules = result[longestSwapQKey].FixedSide;
            var longestFloatSideSchedules = result[longestSwapQKey].FloatingSide;

            foreach (var qkey in remainedSwapQKeys)
            {
                var convention = swapConventions[qkey.IRIndex] as SwapConvention;
                var floatNum = (int)(qkey.ProductTerm / qkey.IRIndex.Tenor);
                var fixedNum = (int)(qkey.ProductTerm / convention.Frequency);
                var floatSideSchedule = longestFloatSideSchedules.GetRange(0, floatNum);
                var fixedSideSchedule = longestFixedSideSchedules.GetRange(0, fixedNum);
                result[qkey] = new ScheduleHolder(asof, fixedSideSchedule, floatSideSchedule);
            }

            return result;
        }

        private static ScheduleHolder GetOisScheduleHolder(DateTime asof, QuoteKey qkey, OisConvention oisConvention, Dictionary<string, List<DateTime>> holidays)
        {
            var floatSideFreq = qkey.IRIndex.Tenor;
            var term = qkey.ProductTerm;
            var dcc = oisConvention.Dcc;
            var bdc = oisConvention.Bdc;
            var spotBcs = oisConvention.SpotBcs;
            var bcs = oisConvention.Bcs;
            var spotLag = oisConvention.SpotLag;
            var paymentLag = oisConvention.PaymentLag;
            var fixedSideFreq = oisConvention.Frequency;
            var isLastOdd = oisConvention.IsLastOdd;
            var ignoreEOM = oisConvention.IgnoreEOM;

            var isHolidaySpotBcs = SchedularUtils.GetIsHoliday(spotBcs, holidays);
            var isHolidayBcs = SchedularUtils.GetIsHoliday(bcs, holidays);

            var spotDate = SchedularUtils.GetNextNBusinessDay(asof, bdc, isHolidaySpotBcs);
            var applyEOM = SchedularUtils.IsEOM(spotDate) && !ignoreEOM;

            var fixedSideSchedules = GetOisFixedSideSchedules(spotDate, dcc, bdc, fixedSideFreq, paymentLag, term, isLastOdd, applyEOM, isHolidayBcs);
            var floatSideSchedules = GetOisFloatSideSchedules(spotDate, dcc, bdc, floatSideFreq, paymentLag, term, isLastOdd, applyEOM, isHolidayBcs);

            return new ScheduleHolder(asof, fixedSideSchedules, floatSideSchedules);
        }
        private static List<Schedule> GetOisFloatSideSchedules(DateTime spotDate, EnumDcc dcc, EnumBdc bdc, double floatSideFreq, int paymentLag, double term, bool isLastOdd, bool applyEOM, Func<DateTime, bool> isHoliday)
        {
            if (term % 1 != 0.0)
            {
                var startDate = spotDate;
                var endDate = SchedularUtils.GetNextNBusinessDay(spotDate.AddDays(term * 30), bdc, isHoliday);
                var paymentDate = SchedularUtils.GetNextNBusinessDay(endDate, bdc, isHoliday, paymentLag);
                var dcf = DCFCalculator.GetDCF(startDate, endDate, dcc);

                return new List<Schedule> { new Schedule(startDate, endDate, paymentDate, dcf) };
            }

            var floatSideSchedules = new List<Schedule>();
            var prevEndDate = spotDate;

            if (!isLastOdd)
            {
                var oddTerm = (int)(term % floatSideFreq);
                var startDate = prevEndDate;
                var endDate = SchedularUtils.GetNextNBusinessDay(SchedularUtils.AdjustEOM(spotDate.AddMonths(oddTerm), applyEOM), bdc, isHoliday);
                var paymentDate = SchedularUtils.GetNextNBusinessDay(endDate, bdc, isHoliday, paymentLag);
                var dcf = DCFCalculator.GetDCF(startDate, endDate, dcc);

                floatSideSchedules.Add(new Schedule(startDate, endDate, paymentDate, dcf));
                prevEndDate = endDate;
            }

            for (var i = 0; i < (int)(term / floatSideFreq); i++)
            {
                var startDate = prevEndDate;
                var endDate = SchedularUtils.GetNextNBusinessDay(SchedularUtils.AdjustEOM(spotDate.AddMonths((i + 1) * (int)floatSideFreq), applyEOM), bdc, isHoliday);
                var paymentDate = SchedularUtils.GetNextNBusinessDay(endDate, bdc, isHoliday, paymentLag);
                var dcf = DCFCalculator.GetDCF(startDate, endDate, dcc);

                floatSideSchedules.Add(new Schedule(startDate, endDate, paymentDate, dcf));
                prevEndDate = endDate;
            }

            if (isLastOdd)
            {
                var startDate = prevEndDate;
                var endDate = SchedularUtils.GetNextNBusinessDay(SchedularUtils.AdjustEOM(spotDate.AddMonths((int)term), applyEOM), bdc, isHoliday);
                var paymentDate = SchedularUtils.GetNextNBusinessDay(endDate, bdc, isHoliday, paymentLag);
                var dcf = DCFCalculator.GetDCF(startDate, endDate, dcc);

                floatSideSchedules.Add(new Schedule(startDate, endDate, paymentDate, dcf));
            }

            return floatSideSchedules;
        }
        private static List<Schedule> GetOisFixedSideSchedules(DateTime spotDate, EnumDcc dcc, EnumBdc bdc, double fixedSideFreq, int paymentLag, double term, bool isLastOdd, bool applyEOM, Func<DateTime, bool> isHoliday)
        {
            return GetOisFloatSideSchedules(spotDate, dcc, bdc, fixedSideFreq, paymentLag, term, isLastOdd, applyEOM, isHoliday);
        }
    }
}
