using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;

namespace MarketProduct
{
    public interface IMarketProduct
    {
        public double GetPresentValue(IInterpolator dfInterpolator);
    }
}
