using System.Numerics;

namespace MathUtils.LinearAlgebra.MatrixUtil
{
    public static class InstanceUtil
    {
        public static Matrix<TNum> GetIdentityMatrix<TNum>(int n) where TNum : INumberBase<TNum>
        {
            var res = new Matrix<TNum>(n, n);

            for (var i = 0; i < n; i++)
            {
                res[i, i] = TNum.MultiplicativeIdentity;
            }

            return res;
        }
    }
}
