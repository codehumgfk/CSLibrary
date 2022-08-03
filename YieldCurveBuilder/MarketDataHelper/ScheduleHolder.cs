using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public class ScheduleHolder
    {
        public readonly DateTime AsOf;
        public readonly List<Schedule> FixedSide;
        public readonly List<Schedule> FloatingSide;

        public ScheduleHolder(DateTime asof, List<Schedule> fixedSide, List<Schedule> floatingSide)
        {
            AsOf = asof;
            FixedSide = fixedSide;
            FloatingSide = floatingSide;
        }

        public ScheduleHolder Copy()
        {
            return new ScheduleHolder(AsOf, new List<Schedule>(FixedSide), new List<Schedule>(FloatingSide));
        }
    }
}
