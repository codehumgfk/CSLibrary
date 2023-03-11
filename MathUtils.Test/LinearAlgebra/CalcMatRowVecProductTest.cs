using System;
using System.Collections.Generic;
using System.Text;
using MathUtils.LinearAlgebra;

namespace MathUtils.Test.LinearAlgebra
{
    public static class CalcMatRowVecProductTest
    {
        public static void Execute()
        {
            var arr = new double[2, 2] { { 1, 2 }, { 3, 4 } };
            var matrix = new Matrix<double>(arr);
            var arr2 = new double[2] { 1, 1 };
            var rowVec = new RowVector<double>(arr2);

            Console.WriteLine(matrix * rowVec); // asn must be [[3], [7]]
        }
    }
}
