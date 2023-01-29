using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.DayCountFraction;

namespace MarketDataHelper.Conventions
{
    public class Convention
    {
        public readonly EnumDcc Dcc;
        public readonly EnumBdc Bdc;
        private List<string> _SpotBcs;
        private List<string> _Bcs;
        public readonly int SpotLag; // days
        public readonly int FixingLag;
        public readonly int PaymentLag; // days

        public Convention(EnumDcc eDcc, EnumBdc eBdc, List<string> spotBcs, List<string> bcs, int spotLag, int fixingLag, int paymentLag)
        {
            Dcc = eDcc;
            Bdc = eBdc;
            _SpotBcs = new List<string>(spotBcs);
            _Bcs = new List<string>(bcs);
            SpotLag = spotLag;
            FixingLag = fixingLag;
            PaymentLag = paymentLag;
        }
        public Convention(string sDcc, string sBdc, List<string> spotBcs, List<string> bcs, int spotLag, int fixingLag, int paymentLag)
        {
            Dcc = (EnumDcc)Enum.Parse(typeof(EnumDcc), sDcc);
            Bdc = (EnumBdc)Enum.Parse(typeof(EnumBdc), sBdc);
            _SpotBcs = new List<string>(spotBcs);
            _Bcs = new List<string>(bcs);
            SpotLag = spotLag;
            FixingLag = fixingLag;
            PaymentLag = paymentLag;
        }
        public List<string> SpotBcs
        {
            get { return new List<string>(_SpotBcs); }
        }
        public List<string> Bcs
        {
            get { return new List<string>(_Bcs); }
        }
    }
}
