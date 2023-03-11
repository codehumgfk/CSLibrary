using System;
using System.Collections.Generic;
using System.Text;
using MathUtils.LinearAlgebra;

namespace MathUtils.Test.LinearAlgebra
{
    public static class CreateColumnVectorTest
    {
        public static void Execute()
        {
            var arr = new double[2] { 1, 1 };
            var colVec = new ColumnVector<double>(arr);

            Console.WriteLine(colVec.ToString());
        }
    }
}
