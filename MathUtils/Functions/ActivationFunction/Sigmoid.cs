using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.Functions.ActivationFunction
{
    public static  class Sigmoid
    {
        public static Func<double, double> GetFunction()
        {
            return (double x) => 1.0 / (1.0 + Math.Exp(-x));
        }
    }
}
