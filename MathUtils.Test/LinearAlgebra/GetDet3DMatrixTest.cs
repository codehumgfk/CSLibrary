using System;
using System.Collections.Generic;
using System.Text;
using MathUtils.LinearAlgebra;

namespace MathUtils.Test.LinearAlgebra
{
    public static class GetDet3DMatrixTest
    {
        public static void Execute()
        {
            var arr = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var matrix = new Matrix(arr);

            Console.WriteLine(matrix.Det); // ans must be 0
        }
    }
}
