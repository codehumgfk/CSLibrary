using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.DayCountFraction;

namespace MarketDataHelper.Conventions
{
    public class OisConvention : SwapConvention
    {
        public readonly bool IsLastOdd;
        public readonly bool IgnoreEOM; 
        public OisConvention(EnumDcc eDcc, EnumBdc eBdc, List<string> spotBcs, List<string> bcs, int spotLag, int fixingLag, int paymentLag, int frequency, bool isLastOdd, bool ignoreEOM)
            : base(eDcc, eBdc, spotBcs, bcs, spotLag, fixingLag, paymentLag, frequency)
        {
            IsLastOdd = isLastOdd;
            IgnoreEOM = ignoreEOM;
        }
        public OisConvention(string eDcc, string eBdc, List<string> spotBcs, List<string> bcs, int spotLag, int fixingLag, int paymentLag, int frequency, bool isLastOdd, bool ignoreEOM)
            : base(eDcc, eBdc, spotBcs, bcs, spotLag, fixingLag, paymentLag, frequency)
        {
            IsLastOdd = isLastOdd;
            IgnoreEOM = ignoreEOM;
        }
    }
}
