using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MarketDataHelper.Parsers
{
    public static class TenorParser
    {
        public static double GetDoubleTenorFromString(string sTenor)
        {
            if (sTenor.Contains("x")) return ParseFraTenor(sTenor);
            return ParseStringTenor(sTenor);
        }
        private static double ParseStringTenor(string sTenor)
        {
            if (sTenor == "O/N") return 1.0 / 30.0; //assume 1 month = 30 days
            if (sTenor.Contains("W")) return GetWeekTenor(sTenor);
            if (sTenor.Contains("M"))
            {
                if (!sTenor.Contains("Y")) return GetMonthTenor(sTenor);
                return GetYearAndMonthTenor(sTenor);
            }
            return GetYearTenor(sTenor);
            
        }
        private static double GetWeekTenor(string sTenor)
        {
            var pattern = @"([1-3])W";
            var matched = new Regex(pattern).Match(sTenor).Groups[1].Value;

            return double.Parse(matched) * 7.0 / 30.0; // assume 1 month = 30 days
        }
        private static double GetMonthTenor(string sTenor)
        {
            var pattern = @"([0-9]+)M";
            var matched = new Regex(pattern).Match(sTenor).Groups[1].Value;

            return double.Parse(matched);
        }
        private static double GetYearAndMonthTenor(string sTenor)
        {
            var pattern = @"([1-9])Y([1-9])M";
            var matchedGroups = new Regex(pattern).Match(sTenor).Groups;
            var matchedYear = double.Parse(matchedGroups[1].Value);
            var matchedMonth = double.Parse(matchedGroups[2].Value);

            return matchedYear * 12 + matchedMonth;
        }
        private static double GetYearTenor(string sTenor)
        {
            var pattern = @"([0-9]+)Y";
            var matched = new Regex(pattern).Match(sTenor).Groups[1].Value;

            return double.Parse(matched) * 12;
        }
        private static double ParseFraTenor(string sTenor)
        {
            var splitted = sTenor.Split('x');
            var startMonth = GetMonthTenor(splitted[0]);
            var endMonth = GetMonthTenor(splitted[1]);

            return startMonth * 10.0 + endMonth;
        }
    }
}
