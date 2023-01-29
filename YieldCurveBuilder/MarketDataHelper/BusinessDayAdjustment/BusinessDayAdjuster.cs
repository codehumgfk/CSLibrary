using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper.BusinessDayAdjustment
{
    public static class BusinessDayAdjuster
    {
        public static DateTime Adjust(DateTime asof, EnumBdc ebdc, Func<DateTime, bool> isHoliday)
        {
            switch (ebdc)
            {
                case EnumBdc.Following:
                    return GetNextBusinessDay(asof, isHoliday);
                case EnumBdc.ModFollowing:
                    return GetModifiedNextBusinessDay(asof, isHoliday);
                case EnumBdc.Preceding:
                    return GetPreviousBusinessDay(asof, isHoliday);
                default:
                    throw new ArgumentException(string.Format("The {0} is not in EnumBdc.", ebdc));
            }
        }

        private static DateTime GetNextBusinessDay(DateTime asof, Func<DateTime, bool> isHoliday)
        {
            var candidate = asof.AddDays(1.0);

            while (isHoliday(candidate))
            {
                candidate = candidate.AddDays(1.0);
            }

            return candidate;
        }

        private static DateTime GetPreviousBusinessDay(DateTime asof, Func<DateTime, bool> isHoliday)
        {
            var candidate = asof.AddDays(-1.0);

            while (isHoliday(candidate))
            {
                candidate = candidate.AddDays(-1.0);
            }

            return candidate;
        }

        private static DateTime GetModifiedNextBusinessDay(DateTime asof, Func<DateTime, bool> isHoliday)
        {
            var candidate = GetNextBusinessDay(asof, isHoliday);
            if (candidate.Month > asof.Month) candidate = GetPreviousBusinessDay(candidate, isHoliday);

            return candidate;
        }
    }
}
