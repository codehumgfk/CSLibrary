using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolator
{
    public static class InterpolatorFactory
    {
        public static IInterpolator GetInterpolator(EInterpolator eInterpolator, IList<double> xs, IList<double> ys)
        {
            switch (eInterpolator)
            {
                case EInterpolator.Linear:
                    return new Instances.LinearInterpolator(xs, ys);
                case EInterpolator.PiecewiseConstant:
                    return new Instances.PiecewiseConstantInterpolator(xs, ys);
                default:
                    throw new ArgumentException(string.Format("'case {0}' must be added in 'InterPolatorFactory.GetInterpolator'.", eInterpolator));
            }
        }
    }
}
