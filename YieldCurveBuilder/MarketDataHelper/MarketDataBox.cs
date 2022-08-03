using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public class MarketDataBox
    {
        public Dictionary<string, List<DateTime>> Holidays { get; set; }
        public Dictionary<QuoteKey, double> MarketQuote { get; set; }
        public Dictionary<QuoteKey, ScheduleHolder> Schedules { get; set; }
    }

}
