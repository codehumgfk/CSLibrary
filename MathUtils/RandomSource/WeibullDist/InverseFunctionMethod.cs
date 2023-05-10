using System;
using System.Numerics;

namespace MathUtils.RandomSource.WeibullDist
{
    public static class InverseFunctionMethod
    {
        /// <summary>
        /// Distribution Function F(x)
        /// F(x) = 1 - exp(-(x/alpha)^gamma)
        /// </summary>
        /// <typeparam name="TFloat"></typeparam>
        /// <param name="gamma"></param>
        /// <param name="alpha"></param>
        /// <param name="rndSrc"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static TFloat Generate<TFloat>(TFloat gamma, TFloat alpha, ref Random rndSrc) where TFloat : IFloatingPointIeee754<TFloat>
        {
            var gammaCheck = TFloat.IsNegative(gamma) || TFloat.IsZero(gamma);
            var alphaCheck = TFloat.IsNegative(alpha) || TFloat.IsZero(alpha);
            if (gammaCheck || alphaCheck) throw new ArgumentException("Parameters both gamma and alpha must be positive.");

            var x = TFloat.CreateSaturating(rndSrc.NextDouble());
            var one = TFloat.One;

            return alpha * TFloat.Pow(-TFloat.Log(one - x), one / gamma);
        }
    }
}
