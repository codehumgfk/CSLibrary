using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;
using MarketDataHelper;

namespace DFCurveBuilder
{
    public class DiscountFactorSeries
    {
        public readonly InterestRateIndex IRIndex;
        public readonly DateTime AsOf; 
        private List<double> _Days;
        private List<double> _DF;

        public readonly EInterpolator InterpolatorKind;
        public bool AllowExtrapolation;
        public IInterpolator Interpolator;

        public DiscountFactorSeries(InterestRateIndex irIndex, EInterpolator eInterpolator, bool allowExtrapolation=false)
        {
            IRIndex = irIndex.Copy();
            InterpolatorKind = eInterpolator;
            AllowExtrapolation = allowExtrapolation;

            _Days = new List<double> { 0.0 };
            _DF = new List<double> { 1.0 };
        }
        public DiscountFactorSeries(DiscountFactorSeries dfS)
        {
            IRIndex = dfS.IRIndex.Copy();
            InterpolatorKind = dfS.InterpolatorKind;
            AllowExtrapolation = dfS.AllowExtrapolation;

            _Days = dfS.Days;
            _DF = dfS.DF;
            Interpolator = InterpolatorFactory.GetInterpolator(InterpolatorKind, _Days, _DF);
        }
        public void Add(double day, double df)
        {
            _Days.Add(day);
            _DF.Add(df);
            Interpolator = InterpolatorFactory.GetInterpolator(InterpolatorKind, _Days, _DF);
        }
        public double this[double day]
        {
            get { return Interpolate(day); }
        }
        private double Interpolate(double x)
        {
            return Interpolator.Interpolate(x, AllowExtrapolation);
        }
        public List<double> Days
        {
            get { return new List<double>(_Days); }
        }
        public List<double> DF
        {
            get { return new List<double>(_DF); }
        }
    }
}
