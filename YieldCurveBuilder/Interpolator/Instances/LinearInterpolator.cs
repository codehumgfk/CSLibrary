using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolator.Instances
{
    public class LinearInterpolator : AbstractInterpolator
    {
        public LinearInterpolator(IList<double> xs, IList<double> ys) : base(xs, ys)
        {}
        public override double Interpolate(double x, bool allowExtrapolation=false)
        {
            var index = FindIndexOfBottomLimit(x);
            if ((index == -1 || xs[xs.Count - 1] < x) && !allowExtrapolation) throw new ArgumentException("Extrapolation is not allowed.");
            if (index == -1) return ys[0];
            if (index == xs.Count - 1) return ys[xs.Count - 1];

            var x1 = xs[index];
            var x2 = xs[index + 1];
            var y1 = ys[index];
            var y2 = ys[index + 1];

            var slope = (y2 - y1) / (x2 - x1);

            return y1 + slope * (x - x1);
        }
    }
}
