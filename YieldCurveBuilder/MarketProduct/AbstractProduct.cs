using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper;

namespace MarketProduct
{
    public abstract class AbstractProduct : IMarketProduct
    {
        public List<ScheduleHolder> Schedules;

        public AbstractProduct(List<ScheduleHolder> schedules)
        {
            Schedules = schedules;
        }
        public abstract double GetPresentValue();
    }
}
