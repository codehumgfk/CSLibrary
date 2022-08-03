using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;
using MarketDataHelper;

namespace MarketProduct
{
    public abstract class AbstractProduct : IMarketProduct
    {
        public readonly ScheduleHolder Schedules;

        public AbstractProduct(ScheduleHolder schedules)
        {
            Schedules = schedules;
        }
        public abstract double GetPresentValue(IInterpolator dfInterpolator);

        public DateTime FinalPaymentDate
        {
            get { return Schedules.FixedSide[Schedules.FixedSide.Count - 1].PaymentDate; }
        }
        public double DaysToFinalPayment
        {
            get { return (FinalPaymentDate - Schedules.AsOf).TotalDays; }
        }
    }
}
