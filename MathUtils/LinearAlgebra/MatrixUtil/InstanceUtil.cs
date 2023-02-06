using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.LinearAlgebra.MatrixUtil
{
    public static class InstanceUtil
    {
        public static Matrix GetIdentityMatrix(int n)
        {
            var res = new Matrix(n, n);

            for (var i = 0; i < n; i++)
            {
                res[i, i] = 1.0;
            }

            return res;
        }
    }
}
