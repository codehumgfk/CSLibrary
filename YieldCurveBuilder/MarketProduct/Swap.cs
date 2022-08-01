using System;
using System.Collections.Generic;
using System.Text;
using MarketDataHelper;

namespace MarketProduct
{
    public class Swap : AbstractProduct
    {
        public double SwapRate;

        public Swap(double swapRate, List<ScheduleHolder> schedules) : base(schedules)
        {
            SwapRate = swapRate;

        }
        public override double GetPresentValue()
        {
            throw new NotImplementedException();
        }

        public Swap Duplicate()
        {
            return new Swap(SwapRate, new List<ScheduleHolder>(Schedules));
        }
    }
}
