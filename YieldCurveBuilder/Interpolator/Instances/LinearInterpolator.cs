using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolator.Instances
{
    public class LinearInterpolator : AbstractInterpolator
    {
        public LinearInterpolator(IList<double> xs, IList<double> ys) : base(xs, ys)
        {
        }
        public override double Interpolate(double x, bool allowExtrapolation)
        {
            throw new NotImplementedException();
        }
    }
}
