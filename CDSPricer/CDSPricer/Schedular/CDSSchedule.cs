using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;

namespace CDSPricer.Schedular
{
    public class CDSSchedule
    {
        public DateTime TradeDate;
        public DateTime CashSettleDate;
        public DateTime AccrualBeginDate;
        public double AccruedDcf;
        public DateTime MaturityDate;
        public List<AccrualSchedule> AccrualSchedules;

        public CDSSchedule(DateTime tradeDate, DateTime cashSettleDate, DateTime accrualBeginDate, double accruedDcf, DateTime maturityDate, List<AccrualSchedule> accrualSchedules)
        {
            TradeDate = tradeDate;
            CashSettleDate = cashSettleDate;
            AccrualBeginDate = accrualBeginDate;
            AccruedDcf = accruedDcf;
            MaturityDate = maturityDate;
            AccrualSchedules = accrualSchedules;
        }
        public double MaturityDays
        {
            get { return (MaturityDate - TradeDate).TotalDays; }
        }
    }
}
