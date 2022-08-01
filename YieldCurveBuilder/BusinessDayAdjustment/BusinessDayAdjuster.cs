using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper;

namespace BusinessDayAdjustment
{
    public static class BusinessDayAdjuster
    {
        public static DateTime Adjust(DateTime asof, EnumBdc ebdc, Func<DateTime, bool> isHoliday, int days=0)
        {
            switch (ebdc)
            {
                case EnumBdc.Following:
                    throw new NotImplementedException();
                case EnumBdc.ModFollowing:
                    throw new NotImplementedException();
                case EnumBdc.Preceding:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException(string.Format("The {0} is not in EnumBdc.", ebdc));
            }
        }

        private static DateTime GetNextBusinessDay()
        {
            throw new NotImplementedException();
        }

        private static DateTime GetPreviousBusinessDay()
        {
            throw new NotImplementedException();
        }

        private static DateTime GetModifiedNextBusinessDay()
        {
            throw new NotImplementedException();
        }
    }
}
