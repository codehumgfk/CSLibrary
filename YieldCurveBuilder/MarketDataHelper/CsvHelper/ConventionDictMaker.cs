using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.Conventions;
using MarketDataHelper.InterestRate;

namespace MarketDataHelper.CsvHelper
{
    public static class ConventionDictMaker
    {
        public static Dictionary<InterestRateIndex, Convention> GetOisConventionDict(string filepath)
        {
            var lines = CsvReader.ReadCsv(filepath);
            var convDict = new Dictionary<InterestRateIndex, Convention>();

            foreach (var line in lines)
            {
                var splitted = line.Split(',');
                var kvpair = CsvDataParser.ParseOisConvention(splitted);
                convDict.Add(kvpair.Key, kvpair.Value);
            }

            return convDict;
        }
    }
}
