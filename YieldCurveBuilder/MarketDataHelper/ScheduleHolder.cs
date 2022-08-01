using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public class ScheduleHolder
    {
        public Schedule FixedSide;
        public Schedule FloatingSide;

        public ScheduleHolder(Schedule fixedSide, Schedule floatingSide)
        {
            FixedSide = fixedSide;
            FloatingSide = floatingSide;
        }

        public ScheduleHolder Copy()
        {
            return new ScheduleHolder(FixedSide.Copy(), FloatingSide.Copy());
        }
    }
}
