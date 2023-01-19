using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.RandomProcess
{
    public interface IRandomProcess
    {
        public double Generate(double dt, double random);
    }
}
