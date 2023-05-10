using System;
using System.Numerics;

namespace MathUtils.RandomSource.LogisticDist
{
    public static class InverseFunctionMethod
    {
        public static TFloat Generate<TFloat>(ref Random rndSrc) where TFloat : IFloatingPointIeee754<TFloat>
        {
            var x = TFloat.CreateSaturating(rndSrc.NextDouble());
            var one = TFloat.One;
            
            return TFloat.Log(x / (one - x));
        }

    }
}
