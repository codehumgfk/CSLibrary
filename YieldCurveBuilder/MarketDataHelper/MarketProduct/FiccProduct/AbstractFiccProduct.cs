using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;
using MarketDataHelper.Schedular;

namespace MarketDataHelper.MarketProduct.FiccProduct
{
    public abstract class AbstractFiccProduct : IFiccProduct
    {
        public ScheduleHolder Schedules;

        public AbstractFiccProduct(ScheduleHolder schedules)
        {
            Schedules = schedules;
        }
        public abstract double GetPresentValue(IInterpolator dfInterpolator);
        public abstract double GetPerRate(IInterpolator dfInterpolator);
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
