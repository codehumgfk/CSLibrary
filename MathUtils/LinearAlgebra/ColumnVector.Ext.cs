using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.LinearAlgebra
{
    public static class ColumnVectorExtensions
    {
        public static ColumnVector MapElementwise(this ColumnVector vec, Func<double, double> func)
        {
            var colLength = vec.Length;
            var res = new ColumnVector(colLength);

            for (var colIdx = 0; colIdx < colLength; colIdx++)
            {
                res[colIdx] = func(vec[colIdx]);
            }

            return res;
        }
        public static List<int> FilterElementwise(this ColumnVector vec, Func<double, bool> func)
        {
            var colLength = vec.Length;
            var res = new List<int>();

            for (var colIdx = 0; colIdx < colLength; colIdx++)
            {
                if (func(vec[colIdx])) res.Add(colIdx);
            }

            return res;
        }
    }
}
