using System;
using System.Collections.Generic;
using System.Text;
using MathUtils.LinearAlgebra;

namespace MathUtils.Test.LinearAlgebra
{
    public static class CalcColVecMatProductTest
    {
        public static void Execute()
        {
            var arr = new double[2, 2] { { 1, 2 }, { 3, 4 } };
            var matrix = new Matrix(arr);
            var arr2 = new double[2] { 1, 1 };
            var colVec = new ColumnVector(arr2);

            Console.WriteLine(colVec * matrix); // ans must be [[4, 6]]
        }
    }
}
