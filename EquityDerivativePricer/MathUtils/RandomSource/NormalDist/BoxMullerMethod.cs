using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.RandomSource.NormalDist
{
    public static class BoxMullerMethod
    {
        public static List<double> Generate(ref Random rndSrc)
        {
            var x = rndSrc.NextDouble();
            var y = rndSrc.NextDouble();

            var z1 = Math.Sqrt(-2.0 * Math.Log(x)) * Math.Cos(2.0 * Math.PI * y);
            var z2 = Math.Sqrt(-2.0 * Math.Log(x)) * Math.Sin(2.0 * Math.PI * y);

            return new List<double> { z1, z2 };
        }
    }
}
