using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;

namespace CDSPricer.Schedular
{
    public static class SchedularUtils
    {
        public static DateTime GetFollowingIMMDate(DateTime asof)
        {
            var immDate = new DateTime(asof.Year, 3, 20);
            for (var i = 0; i < 4; i++)
            {
                if (immDate > asof) return immDate;
                immDate = immDate.AddMonths(3);
            }
            return immDate;
        }
        public static DateTime GetPreviousIMMDate(DateTime asof)
        {
            var immDate = new DateTime(asof.Year, 12, 20);
            for (var i = 0; i < 4; i++)
            {
                if (immDate > asof) return immDate;
                immDate = immDate.AddMonths(-3);
            }
            return immDate;
        }
        public static DateTime GetFollowingWorkingDate(DateTime asof, EnumBdc bdc, Func<DateTime, bool> isHoliday, int days = 1)
        {
            var candidate = BusinessDayAdjuster.Adjust(asof, bdc, isHoliday);
            for (var i = 0; i < days; i++)
            {
                candidate = BusinessDayAdjuster.Adjust(candidate.AddDays(1), bdc, isHoliday);
            }

            return candidate;
        }
        public static Func<DateTime, bool> GetIsHoliday(List<DateTime> holidays)
        {
            if (holidays == null) return (DateTime x) => { return x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday; };
            return (DateTime x) => { return x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(x); };
        }
    }
}
