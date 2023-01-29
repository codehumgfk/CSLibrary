using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDataHelper.Conventions;
using MarketDataHelper.InterestRate;
using MarketDataHelper.MarketProduct;

namespace MarketDataHelper.CsvHelper
{
    internal static class CsvDataParser
    {
        public static KeyValuePair<InterestRateIndex, OisConvention> ParseOisConvention(string[] line)
        {
            var kind = line[0];
            var splitted = kind.Split('.');
            var irIndex = new InterestRateIndex(splitted[1], splitted[2], splitted[0]);
            var dcc = line[1];
            var bdc = line[2];
            var spotBcs = new List<string> { line[3] };
            var bcs = new List<string> { line[4] };
            var spotLag = int.Parse(line[5]);
            var fixinglag = int.Parse(line[6]);
            var paymentLag = int.Parse(line[7]);
            var frequency = int.Parse(line[8]);
            var isLastOdd = line[9] == "TRUE" ? true : false;
            var ignoreEOM = line[10] == "TRUE" ? true : false;
            var oisConvention = new OisConvention(dcc, bdc, spotBcs, bcs, spotLag, fixinglag, paymentLag, frequency, isLastOdd, ignoreEOM);

            return new KeyValuePair<InterestRateIndex, OisConvention>(irIndex, oisConvention);
        }
        public static KeyValuePair<QuoteKey, double> ParseQuotedRate(string[] line)
        {
            var kind = line[0];
            var splitted = kind.Split('.');
            var irIndex = new InterestRateIndex(splitted[1], splitted[2], splitted[0]);
            var product = line[1];
            var term = line[2];
            var qkey = new QuoteKey(irIndex, product, term);
            var rate = double.Parse(line[3]);

            return new KeyValuePair<QuoteKey, double>(qkey, rate);
        }
        public static KeyValuePair<string, List<DateTime>> ParseHolidays(string[] line)
        {
            var holidayList = new List<DateTime>();
            var city = line[0];
            var year = int.Parse(line[1]);

            for (var i = 2; i < line.Length; i++)
            {
                if (line[i] == "") break;
                var splitted = line[i].Split('/').Select(d => int.Parse(d)).ToList();
                holidayList.Add(new DateTime(year, splitted[0], splitted[1]));
            }

            return new KeyValuePair<string, List<DateTime>>(city, holidayList);
        }
    }
}
