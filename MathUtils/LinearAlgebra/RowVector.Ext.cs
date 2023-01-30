using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.LinearAlgebra
{
    public static class RowVectorExtensions
    {
        public static RowVector MapElementwise(this RowVector vec, Func<double, double> func)
        {
            var rowLength = vec.Length;
            var res = new RowVector(rowLength);

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                res[rowIdx] = func(vec[rowIdx]);
            }

            return res;
        }
        public static List<int> FilterElementwise(this RowVector vec, Func<double, bool> func)
        {
            var rowLength = vec.Length;
            var res = new List<int>();

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                if (func(vec[rowIdx])) res.Add(rowIdx);
            }

            return res;
        }
    }
}
