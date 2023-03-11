using System;
using System.Collections.Generic;
using System.Text;
using MathUtils.LinearAlgebra;

namespace MathUtils.Test.LinearAlgebra
{
    public static class GetDet2DMatrixTest
    {
        public static void Execute()
        {
            var arr = new double[2, 2] { { 1, 2 }, { 3, 4 } };
            var matrix = new Matrix<double>(arr);

            Console.WriteLine(matrix.Det); // asn must be -2
        }
    }
}
