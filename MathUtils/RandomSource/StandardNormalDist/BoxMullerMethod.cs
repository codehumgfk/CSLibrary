using System;
using System.Collections.Generic;
using System.Numerics;

namespace MathUtils.RandomSource.StandardNormalDist
{
    public static class BoxMullerMethod
    {
        public static List<TFloat> Generate<TFloat>(ref Random rndSrc) where TFloat : IFloatingPointIeee754<TFloat>
        {
            var x = TFloat.CreateSaturating(rndSrc.NextDouble());
            var y = TFloat.CreateSaturating(rndSrc.NextDouble());
            var two = TFloat.CreateSaturating(2.0);
            
            var z1 = TFloat.Sqrt(-two * TFloat.Log(x)) * TFloat.Cos(two * TFloat.Pi * y);
            var z2 = TFloat.Sqrt(-two * TFloat.Log(x)) * TFloat.Sin(two * TFloat.Pi * y);

            return new List<TFloat> { z1, z2 };
        }
    }
}
