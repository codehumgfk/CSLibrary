using System;
using System.Collections.Generic;
using System.Text;
using CDSPricer.Convention;
using CDSPricer.Market;
using MarketDataHelper.BusinessDayAdjustment;
using MarketDataHelper.DayCountFraction;

namespace CDSPricer.CsvHelper
{
    public static class CsvParser
    {
        public static KeyValuePair<CDSQuoteKey, CDSQuoteRate> ParseCDSQuote(string[] line)
        {
            var entity = line[0];
            var ccy = "JPY";
            var tenor = 5 * 12;
            var key = new CDSQuoteKey(entity, ccy, tenor);
            var coupon = double.Parse(line[1]) / 10000;
            var parSpread = double.Parse(line[4]) / 10000;
            var recoveryRate = 0.35;
            var rate = new CDSQuoteRate(coupon, parSpread, recoveryRate);

            return new KeyValuePair<CDSQuoteKey, CDSQuoteRate>(key, rate);
        }
        public static KeyValuePair<string, CDSConvention> ParseCDSConvention(string[] line)
        {
            var ccy = line[0];
            var dcc = (EnumDcc)Enum.Parse(typeof(EnumDcc), line[1]);
            var bdc = (EnumBdc)Enum.Parse(typeof(EnumBdc), line[2]);
            var cashLag = int.Parse(line[3]);
            var interval = int.Parse(line[4]);
            var convention = new CDSConvention(dcc, bdc, cashLag, interval);

            return new KeyValuePair<string, CDSConvention>(ccy, convention);
        }
    }
}
