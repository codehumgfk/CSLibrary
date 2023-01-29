using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.DayCountFraction;
using CDSPricer.Convention;


namespace CDSPricer.Schedular
{
    public static class CDSScheduleMaker
    {
        public static CDSSchedule GetSchedule(DateTime tradeDate, int tenor, CDSConvention convention, List<DateTime> holidays=null)
        {
            var dcc = convention.Dcc;
            var bdc = convention.Bdc;
            var cashLag = convention.CashLag;
            var isHoliday = SchedularUtils.GetIsHoliday(holidays);
            var cashSettleDate = SchedularUtils.GetFollowingWorkingDate(tradeDate, bdc, isHoliday, cashLag);
            var accrualBeginDate = SchedularUtils.GetPreviousIMMDate(tradeDate);
            var adjustedAccrualBeginDate = BusinessDayAdjuster.Adjust(accrualBeginDate, bdc, isHoliday);
            var accruedDcf = DCFCalculator.GetDCF(adjustedAccrualBeginDate, tradeDate, dcc);
            var maturityDate = SchedularUtils.GetFollowingIMMDate(tradeDate).AddMonths(tenor);
            var accrualSchedules = AccrualScheduleMaker.GetScedules(accrualBeginDate, maturityDate, convention, isHoliday);

            return new CDSSchedule(tradeDate, cashSettleDate, accrualBeginDate, accruedDcf, maturityDate, accrualSchedules);
        }
    }
}
