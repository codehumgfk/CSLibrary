using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;

namespace MarketProduct
{
    public interface IFICCProduct
    {
        public double GetPerRate(IInterpolator dfInterpolator);
    }
}
