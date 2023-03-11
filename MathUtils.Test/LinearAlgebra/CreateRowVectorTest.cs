using System;
using System.Collections.Generic;
using System.Text;
using MathUtils.LinearAlgebra;

namespace MathUtils.Test.LinearAlgebra
{
    public static class CreateRowVectorTest
    {
        public static void Execute()
        {
            var arr = new double[2] { 1, 1 };
            var rowVec = new RowVector<double>(arr);

            Console.WriteLine(rowVec.ToString());
        }
    }
}
