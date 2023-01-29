using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.DayCountFraction;

namespace MarketDataHelper.Conventions
{
    public class DepoConvention : Convention
    {
        public DepoConvention(EnumDcc eDcc, EnumBdc eBdc, List<string> spotBcs, List<string> bcs, int spotLag, int fixingLag, int paymentLag)
            : base(eDcc, eBdc, spotBcs, bcs, spotLag, fixingLag, paymentLag)
        { }
    }
}
