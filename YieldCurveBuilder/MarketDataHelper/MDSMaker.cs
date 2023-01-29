using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketDataHelper.CsvHelper;
using MarketDataHelper.Conventions;
using MarketDataHelper.InterestRate;
using MarketDataHelper.MarketProduct;
using MarketDataHelper.Schedular;

namespace MarketDataHelper
{
    public static class MDSMaker
    {
        public static MarketDataSet GetMarketDataSet(string convFilePath, string quoteFilePath, string holidayPath, DateTime asof)
        {
            var convs = ConventionDictMaker.GetOisConventionDict(convFilePath);
            var convDict = new Dictionary<EProduct, Dictionary<InterestRateIndex, Convention>> { { EProduct.Ois , convs } };
            var quotes = QuoteDictMaker.GetQuoteDict(quoteFilePath);
            var qkeys = quotes.Keys.ToList();
            var holidays = HolidayDictMaker.GetHolidayDict(holidayPath);
            var schedules = ScheduleHolderMaker.MakeScheduleHolderDictionary(asof, qkeys, convDict, holidays);

            var dataSet = new MarketDataSet {
                Conventions = convs,
                QuoteRates = quotes,
                Holidays = holidays,
                Schedules = schedules
            };

            return dataSet;
        }
    }
}
