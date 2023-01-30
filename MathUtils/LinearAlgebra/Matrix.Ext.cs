using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.LinearAlgebra
{
    public static class MatrixExtensions
    {
        public static Matrix Sum(this Matrix matrix, EMatrixAxis axis)
        {
            switch (axis)
            {
                case EMatrixAxis.Column:
                    return SumUpColumn(matrix);
                case EMatrixAxis.Row:
                    return SumUpRow(matrix);
                default:
                    throw new NotSupportedException();
            }
        }
        private static Matrix SumUpColumn(this Matrix matrix)
        {
            var rowLength = matrix.Shape[0];
            var colLength = matrix.Shape[1];
            var res = new Matrix(rowLength, 1);

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                for (var colIdx = 0; colIdx < colLength; colIdx++)
                {
                    res[rowIdx, 0] += matrix[rowIdx, colIdx];
                }
            }

            return res;
        }
        private static Matrix SumUpRow(this Matrix matrix)
        {
            var rowLength = matrix.Shape[0];
            var colLength = matrix.Shape[1];
            var res = new Matrix(1, colLength);

            for (var colIdx = 0; colIdx < colLength; colIdx++)
            {
                for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
                {
                    res[0, colIdx] += matrix[rowIdx, colIdx];
                }
            }
            
            return res;
        }
        public static Matrix Sum(this Matrix matrix)
        {
            var shape = matrix.Shape;
            var rowLength = shape[0];
            var colLength = shape[1];
            var res = new Matrix(1, 1);

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                for (var colIdx = 0; colIdx < colLength; colIdx++)
                {
                    res[0, 0] += matrix[rowIdx, colIdx];
                }
            }

            return res;
        }
        public static Matrix MapElementwise(this Matrix matrix, Func<double, double> func)
        {
            var shape = matrix.Shape;
            var rowLength = shape[0];
            var colLength = shape[1];
            var res = new Matrix(rowLength, colLength);

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                for (var colIdx = 0; colIdx < colLength; colIdx++)
                {
                    res[rowIdx, colIdx] = func(matrix[rowIdx, colIdx]);
                }
            }

            return res;
        }
        public static List<int[]> FilterElementwise(this Matrix matrix, Func<double, bool> func)
        {
            var shape = matrix.Shape;
            var rowLength = shape[0];
            var colLength = shape[1];
            var res = new List<int[]>();

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                for (var colIdx = 0; colIdx < colLength; colIdx++)
                {
                    if (func(matrix[rowIdx, colIdx])) res.Add(new int[2] { rowIdx, colIdx });
                }
            }

            return res;
        }
    }
}
