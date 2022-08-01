using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolator
{
    public interface IInterpolator
    {
        public double Interpolate(double x, bool allowExtrapolation);
    }
}
