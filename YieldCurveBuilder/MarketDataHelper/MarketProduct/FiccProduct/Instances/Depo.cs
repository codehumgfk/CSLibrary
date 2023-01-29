using System;
using System.Collections.Generic;
using System.Text;
using Interpolator;
using MarketDataHelper.Schedular;

namespace MarketDataHelper.MarketProduct.FiccProduct.Instances
{
    public class Depo : AbstractFiccProduct
    {
        public readonly double DepoRate;
        public Depo(double depoRate, ScheduleHolder schedules) : base(schedules)
        {
            DepoRate = depoRate;
        }
        public override double GetPresentValue(IInterpolator dfInterpolator)
        {
            throw new NotImplementedException();
        }
        public override double GetPerRate(IInterpolator interpolator)
        {
            throw new NotImplementedException();
        }
    }
}
