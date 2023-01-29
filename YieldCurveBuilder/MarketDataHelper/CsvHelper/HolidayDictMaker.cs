using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketDataHelper.CsvHelper
{
    public static class HolidayDictMaker
    {
        public static Dictionary<string, List<DateTime>> GetHolidayDict(string filepath)
        {
            var lines = CsvReader.ReadCsv(filepath);
            var parsedHolidays = new List<KeyValuePair<string, List<DateTime>>>();

            foreach (var line in lines)
            {
                var splitted = line.Split(',');
                var kvpair = CsvDataParser.ParseHolidays(splitted);
                parsedHolidays.Add(kvpair);
            }

            var result = IntegrateHolidayLists(parsedHolidays);

            return result;
        }
        private static Dictionary<string, List<DateTime>> IntegrateHolidayLists(List<KeyValuePair<string, List<DateTime>>> parsedHolidays)
        {
            //var resultDict = new Dictionary<string, List<DateTime>>();
            //var citys = parsedHolidays.Select(holidays => holidays.Key).Distinct().ToArray();

            //foreach (var city in citys)
            //{
            //    var integratedHolidays = parsedHolidays.Where(holidays => holidays.Key == city).SelectMany(holidays => holidays.Value).ToList();
            //    resultDict[city] = integratedHolidays;
            //}

            var resultDict = parsedHolidays.GroupBy(holidays => holidays.Key).ToDictionary(g => g.Key, g => g.SelectMany(hs => hs.Value).ToList());

            return resultDict;
        }
    }
}
