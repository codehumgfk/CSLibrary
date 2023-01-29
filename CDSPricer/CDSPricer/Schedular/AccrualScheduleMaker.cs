using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.DayCountFraction;
using CDSPricer.Convention;

namespace CDSPricer.Schedular
{
    public static class AccrualScheduleMaker
    {
        public static List<AccrualSchedule> GetScedules(DateTime accrualBeginDate, DateTime maturityDate, CDSConvention convention, Func<DateTime, bool> isHoliday)
        {
            var res = new List<AccrualSchedule>();
            var dcc = convention.Dcc;
            var bdc = convention.Bdc;
            var interval = convention.Interval;
            var tenor = (maturityDate.Year - accrualBeginDate.Year) * 12 + (maturityDate.Month - accrualBeginDate.Month);
            var prevEndDate = BusinessDayAdjuster.Adjust(accrualBeginDate, bdc, isHoliday);

            for (var i = 0; i < tenor / interval - 1; i++)
            {
                var startDate = prevEndDate;
                var nextIMMDate = SchedularUtils.GetFollowingIMMDate(prevEndDate);
                var paymentDate = BusinessDayAdjuster.Adjust(nextIMMDate, bdc, isHoliday);
                var endDate = paymentDate;
                var dcf = DCFCalculator.GetDCF(startDate, endDate, dcc);
                res.Add(new AccrualSchedule(startDate, endDate, paymentDate, dcf));
                prevEndDate = endDate;
            }

            var startDateFinal = prevEndDate;
            var nextIMMDateFinal = SchedularUtils.GetFollowingIMMDate(prevEndDate);
            var paymentDateFinal = BusinessDayAdjuster.Adjust(nextIMMDateFinal, bdc, isHoliday);
            var endDateFinal = maturityDate;
            var dcfFinal = DCFCalculator.GetDCF(startDateFinal, endDateFinal, dcc);
            res.Add(new AccrualSchedule(startDateFinal, endDateFinal, paymentDateFinal, dcfFinal));

            return res;
        }
    }
}
