using System;
using System.Numerics;

namespace MathUtils.RandomSource.ExponentialDist
{
    public static class InverseFunctionMethod
    {
        public static TFloat Generate<TFloat>(TFloat lambda, ref Random rndSrc) where TFloat : IFloatingPointIeee754<TFloat>
        {
            if (TFloat.IsNegative(lambda) || TFloat.IsZero(lambda)) throw new ArgumentException("The parameter lambda must be positive.");
            
            var x = TFloat.CreateSaturating(rndSrc.NextDouble());

            return -TFloat.Log(TFloat.CreateSaturating(1) - x) / lambda;
        }
    }
}
