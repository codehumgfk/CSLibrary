using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.Distribution
{
    public static class StandardNormDist
    {
        public static double PDF(double x)
        {
            return Math.Exp(-Math.Pow(x, 2) / 2) / Math.Sqrt(2.0 * Math.PI);
        }
        public static double CDF(double x)
        {
            if (x >= 0) return Erf(x / Math.Sqrt(2)) / 2.0 + 0.5;
            else return 1.0 - CDF(-x);
        }
        private static double Erf(double x)
        {
            var a = new double[6] { 0.0705231, 0.0422820, 0.00927053, 0.000152014, 0.000276567, 0.0000430638 };
            var cache = 0.0;
            for (var i = 0; i < 6; i++)
            {
                cache += a[i] * Math.Pow(x, i + 1);
            }
            var result = 1 - Math.Pow(1 + cache, -16);

            return result;
        }
    }
}
