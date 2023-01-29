using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interpolator;

namespace CDSPricer.HazardCurve
{
    public class SurvivalProbSeries : HazardRateSeries
    {
        private List<double> _Days;
        private List<double> _SurvivalProbs; 
        public new EInterpolator InterpolatorType;
        public new bool AllowExtrapolation;
        private IInterpolator Interpolator;
        public SurvivalProbSeries(HazardRateSeries hazard, EInterpolator eInterpolator=EInterpolator.Linear, bool allowExtrapolation=true) : base(hazard)
        {
            _Days = hazard.Days;
            _SurvivalProbs = _Days.Select(d => Math.Exp(-IntegrateByTime(d))).ToList();
            InterpolatorType = eInterpolator;
            Interpolator = InterpolatorFactory.GetInterpolator(InterpolatorType, _Days, _SurvivalProbs.Select(p => Math.Log(p)).ToList());
            AllowExtrapolation = allowExtrapolation;
        }
        public new double this[double day]
        {
            get { return Math.Exp(Interpolator.Interpolate(day, AllowExtrapolation)); }
        }
    }
}
