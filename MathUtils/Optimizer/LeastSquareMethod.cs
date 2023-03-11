using MathUtils.LinearAlgebra;
using System.Numerics;

namespace MathUtils.Optimizer
{
    public static class LeastSquareMethod
    {
        public static RowVector<TNum> Optimize<TNum>(Matrix<TNum> A, RowVector<TNum> b) where TNum : INumberBase<TNum>
        {
            var regA = A.T * A;

            return regA.Inv * A.T * b;
        }
    }
}
