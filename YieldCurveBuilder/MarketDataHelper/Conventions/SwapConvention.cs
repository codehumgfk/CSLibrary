using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.DayCountFraction;

namespace MarketDataHelper.Conventions
{
    public class SwapConvention : Convention
    {
        public readonly int Frequency; // days

        public SwapConvention(EnumDcc eDcc, EnumBdc eBdc, List<string> spotBcs, List<string> bcs, int spotLag, int fixingLag, int paymentLag, int frequency)
            : base(eDcc, eBdc, spotBcs, bcs, spotLag, fixingLag, paymentLag)
        {
            Frequency = frequency;
        }
        public SwapConvention(string eDcc, string eBdc, List<string> spotBcs, List<string> bcs, int spotLag, int fixingLag, int paymentLag, int frequency)
            : base(eDcc, eBdc, spotBcs, bcs, spotLag, fixingLag, paymentLag)
        {
            Frequency = frequency;
        }
    }
}
