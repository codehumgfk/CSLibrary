using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.LinearAlgebra.Optimizer
{
    public static class LeastSquareMethod
    {
        public static RowVector Optimize(Matrix A, RowVector b)
        {
            var regA = A.T * A;

            return regA.Inv * A.T * b;
        }
    }
}
