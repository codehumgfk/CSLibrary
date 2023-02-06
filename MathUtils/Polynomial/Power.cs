using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.PolynomialFunctions
{
    public static class Power
    {
        public static Func<double, double> GetFunction(int dim)
        {
            return (double x) => Math.Pow(x, dim);
        }
    }
}
