using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public class MarketDataBox
    {
        public Dictionary<string, List<DateTime>> Holidays;
        public List<ScheduleHolder> Schedules { get; set; }
    }

}
