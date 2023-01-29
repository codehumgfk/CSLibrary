using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;
using MarketDataHelper.InterestRate;

namespace CurveBuilder.DFCurve
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

        public DiscountFactorSeries(InterestRateIndex irIndex, EInterpolator eInterpolator, bool allowExtrapolation = false)
        {
            IRIndex = irIndex;
            InterpolatorKind = eInterpolator;
            AllowExtrapolation = allowExtrapolation;

            _Days = new List<double> { 0.0 };
            _DF = new List<double> { 1.0 };
        }
        public DiscountFactorSeries(DiscountFactorSeries dfS)
        {
            IRIndex = dfS.IRIndex;
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
        public void AddKnownDF(double days, double df)
        {
            var newDaysList = new List<double> { CheckIsBeforeAsOf(days) };
            newDaysList.AddRange(_Days);
            _Days = newDaysList;
            var newDFList = new List<double> { df };
            newDFList.AddRange(_DF);
            _DF = newDFList;
        }
        private double CheckIsBeforeAsOf(double days)
        {
            if (days > 0) throw new ArgumentException("'days' must be before 'asof'.");
            return days;
        }

        public void AddKnownDFRange(List<double> days, List<double> df)
        {
            var newDaysList = new List<double>(days);
            newDaysList.AddRange(_Days);
            _Days = newDaysList;
            var newDFList = new List<double>(df);
            newDFList.AddRange(_DF);
            _DF = newDFList;
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
        public List<string> CsvFormat()
        {
            var txt = new List<string>();
            var header = "Day,DF";
            txt.Add(header);

            for (var i = 0; i < _Days.Count; i++)
            {
                txt.Add(_Days[i].ToString() + "," + _DF[i].ToString());
            }

            return txt;
        }
    }
}
