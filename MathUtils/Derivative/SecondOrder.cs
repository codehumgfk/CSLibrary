using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.Derivative
{
    public static class SecondOrder
    {
        public static double Differentiate(Func<double, double> func, double x, double h, string diffType)
        {
            switch (diffType)
            {
                case "Central":
                    return CentralDiff(func, x, h);
                case "Forward":
                    return ForwardDiff(func, x, h);
                case "Backward":
                    return BackwardDiff(func, x, h);
                default:
                    throw new ArgumentException();
            }
        }
        private static double CentralDiff(Func<double, double> func, double x, double h)
        {
            var result = (func(x + 2 * h) + func(x - 2 * h) - 2 * func(x)) / 4.0 / Math.Pow(h, 2);
            return result;
        }
        private static double ForwardDiff(Func<double, double> func, double x, double h)
        {
            var result = (func(x + 2 * h) + func(x) - 2 * func(x + h)) / Math.Pow(h, 2);
            return result;
        }
        private static double BackwardDiff(Func<double, double> func, double x, double h)
        {
            var result = (func(x) + func(x - 2 * h) - 2 * func(x - h)) / Math.Pow(h, 2);
            return result;
        }
    }
}
