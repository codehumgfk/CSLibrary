using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.MarketProduct;

namespace MarketDataHelper.CsvHelper
{
    public static class QuoteDictMaker
    {
        public static Dictionary<QuoteKey, double> GetQuoteDict(string filepath)
        {
            var lines = CsvReader.ReadCsv(filepath);
            var quoteDict = new Dictionary<QuoteKey, double>();

            foreach (var line in lines)
            {
                var splitted = line.Split(',');
                var kvpair = CsvDataParser.ParseQuotedRate(splitted);
                quoteDict.Add(kvpair.Key, kvpair.Value);
            }

            return quoteDict;
        }
    }
}
