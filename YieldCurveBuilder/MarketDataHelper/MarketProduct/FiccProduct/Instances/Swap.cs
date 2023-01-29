using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;
using MarketDataHelper.Schedular;

namespace MarketDataHelper.MarketProduct.FiccProduct.Instances
{
    public class Swap : AbstractFiccProduct
    {
        public readonly double SwapRate;
        public readonly bool IsFixedSide;

        public Swap(double swapRate, ScheduleHolder schedules, bool isFixedSide = true) : base(schedules)
        {
            SwapRate = swapRate;
            IsFixedSide = isFixedSide;

        }
        public override double GetPresentValue(IInterpolator dfInterpolator)
        {
            var sign = IsFixedSide ? 1.0 : -1.0;
            var fixedSidePV = GetFixedSidePV(dfInterpolator);
            var floatSidePV = GetFloatSidePV(dfInterpolator);

            return sign * (floatSidePV - fixedSidePV);
        }
        public override double GetPerRate(IInterpolator dfInterpolator)
        {
            var floatSidePV = GetFloatSidePV(dfInterpolator);
            var denom = GetDenomOfPerRateCalculation(dfInterpolator);

            return floatSidePV / denom;
        }
        protected double GetFixedSidePV(IInterpolator dfInterpolator)
        {
            var asof = Schedules.AsOf;
            var schedules = Schedules.FixedSide;
            var pv = 0.0;

            foreach (var schedule in schedules)
            {
                var paymentDate = schedule.PaymentDate;
                var tn = (paymentDate - asof).TotalDays;
                var df = dfInterpolator.Interpolate(tn);
                var dcf = schedule.DCF;

                pv = pv + df * dcf * SwapRate;
            }

            return pv;
        }
        protected double GetFloatSidePV(IInterpolator dfInterpolator)
        {
            var asof = Schedules.AsOf;
            var schedules = Schedules.FloatingSide;
            var pv = 0.0;

            foreach (var schedule in schedules)
            {
                var paymentDate = schedule.PaymentDate;
                var tn = (paymentDate - asof).TotalDays;
                var df = dfInterpolator.Interpolate(tn);
                var dcf = schedule.DCF;

                var startDate = schedule.StartDate;
                var tm_1 = (startDate - asof).TotalDays;
                var dftm_1 = dfInterpolator.Interpolate(tm_1);
                var endDate = schedule.EndDate;
                var tm = (endDate - asof).TotalDays;
                var dftm = dfInterpolator.Interpolate(tm);
                var forwardLibor = (dftm_1 / dftm - 1) / dcf;

                pv = pv + df * dcf * forwardLibor;
            }

            return pv;
        }
        protected double GetDenomOfPerRateCalculation(IInterpolator dfInterpolator)
        {
            var denom = 0.0;

            var asof = Schedules.AsOf;
            var schedules = Schedules.FixedSide;
            foreach (var schedule in schedules)
            {
                var paymentDate = schedule.PaymentDate;
                var tn = (paymentDate - asof).TotalDays;
                var df = dfInterpolator.Interpolate(tn);
                var dcf = schedule.DCF;

                denom = denom + df * dcf;
            }

            return denom;
        }
        public Swap Duplicate()
        {
            return new Swap(SwapRate, Schedules.Copy());
        }
    }
}
