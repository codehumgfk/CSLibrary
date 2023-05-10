using System;
using System.Numerics;

namespace MathUtils.RandomSource.CauchyDist
{
    public static class InverseFunctionMethod
    {
        public static TFloat Generate<TFloat>(ref Random rndSrc) where TFloat : IFloatingPointIeee754<TFloat>
        {
            var x = TFloat.CreateSaturating(rndSrc.NextDouble());
            var half = TFloat.CreateSaturating(0.5);

            return TFloat.Tan(TFloat.Pi * (x - half));
        }
    }
}
