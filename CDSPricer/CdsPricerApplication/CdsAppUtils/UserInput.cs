using System;
using System.Collections.Generic;
using System.Text;

namespace CdsPricerApplication.CdsAppUtils
{
    public class UserInput
    {
        public double Notional;
        public bool IsProtectionBuyer;
        public int Tenor;
        public double ParSpread;

        public UserInput(double notional, bool isProtectionBuyer, int tenor, double parSpread)
        {
            Notional = notional;
            IsProtectionBuyer = isProtectionBuyer;
            Tenor = tenor;
            ParSpread = parSpread;
        }
    }
}
