using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper.Conventions;
using MarketDataHelper.InterestRate;
using MarketDataHelper.MarketProduct;
using MarketDataHelper.Schedular;

namespace MarketDataHelper
{
    public class MarketDataSet
    {
        public Dictionary<QuoteKey, double> QuoteRates { get; set; }
        public Dictionary<InterestRateIndex, Convention> Conventions { get; set; }
        public Dictionary<string, List<DateTime>> Holidays { get; set; }
        public Dictionary<QuoteKey, ScheduleHolder> Schedules { get; set; }
    }
}
