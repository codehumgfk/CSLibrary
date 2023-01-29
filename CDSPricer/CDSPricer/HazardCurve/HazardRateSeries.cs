using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interpolator;

namespace CDSPricer.HazardCurve
{
    public class HazardRateSeries
    {
        private List<double> _Days;
        private List<double> _HazardRates;
        public EInterpolator InterpolatorType;
        public bool AllowExtrapolation;
        private IInterpolator Interpolator;
        public HazardRateSeries(EInterpolator interpolatorType = EInterpolator.PiecewiseConstant, bool allowExtrapolation=true)
        {
            _Days = new List<double> { 0.0 };
            _HazardRates = new List<double> { 0.0 };
            InterpolatorType = interpolatorType;
            AllowExtrapolation = allowExtrapolation;
        }
        protected HazardRateSeries(HazardRateSeries hazard)
        {
            _Days = new List<double>(hazard.Days);
            _HazardRates = new List<double>(hazard.HazardRates);
            InterpolatorType = hazard.InterpolatorType;
            AllowExtrapolation = hazard.AllowExtrapolation;
            Interpolator = InterpolatorFactory.GetInterpolator(InterpolatorType, _Days, _HazardRates);
        }
        public double this[double day]
        {
            get { return Interpolator.Interpolate(day); }
        }
        public void Add(double day, double h)
        {
            _Days.Add(day);
            _HazardRates.Add(h);
            Interpolator = InterpolatorFactory.GetInterpolator(InterpolatorType, _Days, _HazardRates);
        }
        public List<double> Days
        {
            get { return new List<double>(_Days); }
        }
        public List<double> HazardRates
        {
            get { return new List<double>(_HazardRates); }
        }
        public HazardRateSeries Copy()
        {
            return new HazardRateSeries(this);
        }
        protected double IntegrateByTime(double day)
        {
            if (day <= 0.0) return 0.0;
            var res = 0.0;
            var index = _Days.Where(d => d < day).Count() - 1;
            for (var i = 0; i < index - 1; i++)
            {
                res += _HazardRates[i + 1] * (_Days[i + 1] - _Days[i]);
            }
            res += this[day] * (day - _Days[index]);

            return res;
        }
        public List<string> CsvFormat
        {
            get 
            {
                var lines = new List<string>();
                lines.Add("Days,HazardRate,SurvivalRate");
                for (var i = 0; i < _Days.Count; i++)
                {
                    lines.Add(_Days[i].ToString() + "," + _HazardRates[i].ToString() + "," + (Math.Exp(-IntegrateByTime(_Days[i]))).ToString());
                }

                return lines;
            }
        }
    }
}
