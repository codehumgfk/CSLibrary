using System;
using System.Numerics;

namespace MathUtils.RandomSource.ParetoDist
{
    public static class InverseFunctionMethod
    {
        /// <summary>
        /// Distribution function F(x)
        /// F(x) = 1 - (b / x)^a
        /// </summary>
        /// <typeparam name="TFloat"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="rndSrc"></param>
        /// <returns></returns>
        public static TFloat Generate<TFloat>(TFloat a, TFloat b, ref Random rndSrc) where TFloat : IFloatingPointIeee754<TFloat>
        {
            var x = TFloat.CreateSaturating(rndSrc.NextDouble());
            var one = TFloat.One;

            return b / TFloat.Pow(one - x, one / a);
        }
    }
}
