using System.Numerics;

namespace MathUtils.LinearAlgebra.MatrixUtil
{
    public static class MethodUtil
    {
        public static (Matrix<TNum>, Matrix<TNum>) Diagonalize<TNum>(Matrix<TNum> a) where TNum : INumberBase<TNum>
        {
            var P = new Matrix<TNum>(1, 1);
            var D = new Matrix<TNum>(1, 1);

            return (P, D);
        }
    }
}
