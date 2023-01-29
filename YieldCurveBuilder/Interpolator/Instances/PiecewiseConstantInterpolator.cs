using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolator.Instances
{
    public class PiecewiseConstantInterpolator : AbstractInterpolator
    {
        public PiecewiseConstantInterpolator(IList<double> xs, IList<double> ys) : base(xs, ys)
        { }

        public override double Interpolate(double x, bool allowExtrapolation = false)
        {
            var index = FindIndexOfBottomLimit(x);
            if ((index == -1 || xs[xs.Count - 1] < x) && !allowExtrapolation) throw new ArgumentException("Extrapolation is not allowed.");
            if (index == xs.Count - 1) return ys[xs.Count - 1];

            return ys[index + 1];

        }
    }
}
