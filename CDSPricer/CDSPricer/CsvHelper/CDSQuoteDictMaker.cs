using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CDSPricer.Market;

namespace CDSPricer.CsvHelper
{
    public static class CDSQuoteDictMaker
    {
        public static Dictionary<CDSQuoteKey, CDSQuoteRate> GetDict(string filepath, bool ignoreHeader=true)
        {
            var res = new Dictionary<CDSQuoteKey, CDSQuoteRate>();
            var lines = File.ReadLines(filepath).Skip(1).ToList();

            foreach (var line in lines)
            {
                var splitted = line.Split(',');
                var kvpair = CsvParser.ParseCDSQuote(splitted);
                res.Add(kvpair.Key, kvpair.Value);
            }

            return res;
        }
    }
}
