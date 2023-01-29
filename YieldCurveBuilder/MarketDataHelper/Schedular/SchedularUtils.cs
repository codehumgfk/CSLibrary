using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDataHelper.BusinessDayAdjustment;

namespace MarketDataHelper.Schedular
{
    internal static class SchedularUtils
    {
        public static DateTime GetNextNBusinessDay(DateTime asof, EnumBdc eBdc, Func<DateTime, bool> isHoliday, int n = 0)
        {
            var candidate = BusinessDayAdjuster.Adjust(asof, eBdc, isHoliday);
            for (var i = 0; i < n; i++)
            {
                candidate = BusinessDayAdjuster.Adjust(candidate.AddDays(1.0), eBdc, isHoliday);
            }

            return candidate;
        }

        public static Func<DateTime, bool> GetIsHoliday(List<string> citys, Dictionary<string, List<DateTime>> holidays)
        {
            var mergedList = new List<DateTime>();
            foreach (var city in citys)
            {
                mergedList = mergedList.Union(holidays[city]).ToList();
            }

            return (DateTime asof) => { return mergedList.Contains(asof) || asof.DayOfWeek == DayOfWeek.Saturday || asof.DayOfWeek == DayOfWeek.Sunday; };
        }
        public static DateTime AdjustEOM(DateTime asof, bool applyEOM)
        {
            if (applyEOM) return new DateTime(asof.Year, asof.Month, DateTime.DaysInMonth(asof.Year, asof.Month));
            return asof;
        }
        public static bool IsEOM(DateTime asof)
        {
            return DateTime.DaysInMonth(asof.Year, asof.Month) == asof.Day;
        }
    }
}
