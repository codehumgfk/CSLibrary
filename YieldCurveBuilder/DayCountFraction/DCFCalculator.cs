using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper;

namespace DayCountFraction
{
    public static class DCFCalculator
    {
        public static double GetDCF(DateTime startDate, DateTime endDate, EnumDcc eDcc)
        {
            switch (eDcc)
            {
                case EnumDcc.Act360:
                    throw new NotImplementedException();
                case EnumDcc.Act365:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException(string.Format("The {0} is not in EnumDcc.", eDcc));
            }
        }
    }
}
