using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper;

namespace MarketProduct.Instances
{
    public class IRS : Swap
    {
        public IRS(double irsRate, ScheduleHolder schedules, bool isFixedSide) : base(irsRate, schedules, isFixedSide) { }
    }
}
