using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;

namespace MarketDataHelper.MarketProduct.FiccProduct
{
    public interface IFiccProduct
    {
        public double GetPresentValue(IInterpolator dfInterpolator);
        public double GetPerRate(IInterpolator dfInterpolator);
    }
}
