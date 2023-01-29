using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper.DayCountFraction
{
    public static class DCFCalculator
    {
        public static double GetDCF(DateTime startDate, DateTime endDate, EnumDcc eDcc)
        {
            switch (eDcc)
            {
                case EnumDcc.Act360:
                    return DcfByAct360(startDate, endDate);
                case EnumDcc.Act365F:
                    return DcfByAct365F(startDate, endDate);
                default:
                    throw new ArgumentException(string.Format("The {0} is not in EnumDcc.", eDcc));
            }
        }
        private static double DcfByAct365F(DateTime startDate, DateTime endDate)
        {
            var delta = (endDate - startDate).TotalDays;
            return delta / 365.0;
        }
        private static double DcfByAct360(DateTime startDate, DateTime endDate)
        {
            var delta = (endDate - startDate).TotalDays;
            return delta / 360.0;
        }
    }
}
