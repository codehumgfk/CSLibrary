using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;
using MarketDataHelper;

namespace MarketProduct.Instances
{
    public class OIS : Swap
    {
        public OIS(double oisRate, ScheduleHolder schedules, bool isFixedSide=true) : base(oisRate, schedules, isFixedSide)
        {}

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
        protected override double GetFloatSidePV(IInterpolator dfInterpolator)
        {
            var asof = Schedules.AsOf;
            var schedules = Schedules.FloatingSide;
            var spotDate = schedules[0].StartDate;
            var t0 = (spotDate - asof).TotalDays;
            var lastEndDate = schedules[schedules.Count - 1].PaymentDate;
            var tM = (lastEndDate - asof).TotalDays;
            var dft0 = dfInterpolator.Interpolate(t0);
            var dftM = dfInterpolator.Interpolate(tM);

            return dft0 - dftM;
        }
        public double OisRate
        {
            get { return SwapRate; }
        }
    }
}
