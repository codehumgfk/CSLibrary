using System;
using System.Collections.Generic;
using System.Text;

namespace CDSPricer.Market
{
    public class CDSQuoteRate
    {
        public double Coupon;
        public double ParSpread;
        public double RecoveryRate;

        public CDSQuoteRate(double coupon, double parSpread, double recoveryRate)
        {
            Coupon = coupon;
            ParSpread = parSpread;
            RecoveryRate = recoveryRate;
        }
    }
}
