using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.DayCountFraction;

namespace CDSPricer.Convention
{
    public class CDSConvention
    {
        public EnumDcc Dcc;
        public EnumBdc Bdc;
        public int CashLag;
        public int Interval;


        public CDSConvention(EnumDcc dcc, EnumBdc bdc, int cashLag, int interval)
        {
            Dcc = dcc;
            Bdc = bdc;
            CashLag = cashLag;
            Interval = interval;
        }
    }
}
