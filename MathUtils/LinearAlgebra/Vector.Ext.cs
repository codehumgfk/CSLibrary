using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.LinearAlgebra
{
    public static class VectorExtension
    {
        #region Statistics Methods and Properties
        public static TNum CalcUnbiasedVariance<TNum>(this Vector<TNum> vec) where TNum : IFloatingPointIeee754<TNum>
        {
            if (vec.Length == 1) throw new NotSupportedException("Cannnot calculate an unbiased variance for 1 sample.");
            var mean = vec.Mean;
            var res = TNum.Zero;

            for (var i = 0; i < vec.Length; i++)
            {
                var diff = vec[i] - mean;
                res += TNum.Pow(diff, TNum.CreateSaturating(2.0));
            }
            res /= TNum.CreateSaturating(vec.Length - 1.0);

            return res;
        }
        public static TNum CalcStandardDeviation<TNum>(this Vector<TNum> vec) where TNum : IFloatingPointIeee754<TNum>
        {
            return TNum.Sqrt(vec.CalcUnbiasedVariance());
        }
        public static TNum CalcStandardError<TNum>(this Vector<TNum> vec) where TNum : IFloatingPointIeee754<TNum>
        {
            return vec.CalcStandardDeviation() / TNum.Sqrt(TNum.CreateSaturating(vec.Length));
        }
        public static TNum CalculateNorm<TNum>(this Vector<TNum> vec) where TNum : IFloatingPointIeee754<TNum>
        {
            var res = TNum.Zero;
            for (var i = 0; i < vec.Length; i++)
            {
                res += TNum.Pow(vec[i], TNum.CreateSaturating(2.0));
            }

            return TNum.Sqrt(res);
        }
        #endregion
    }
}
