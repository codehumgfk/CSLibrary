using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.Functions.ActivationFunction
{
    public static class RectifiedLinearUnit
    {
        public static Func<double, double> GetFunction()
        {
            return (x) => (x > 0 ? 1.0 : 0.0) * x;
        }
    }
}
