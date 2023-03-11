using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MathUtils.LinearAlgebra
{
    public static class MatrixExtensions
    {
        #region Sum Methods
        public static Matrix<TNum> Sum<TNum>(this Matrix<TNum> matrix, EMatrixAxis axis) where TNum : INumberBase<TNum>
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
        private static Matrix<TNum> SumUpColumn<TNum>(this Matrix<TNum> matrix) where TNum : INumberBase<TNum>
        {
            var rowLength = matrix.Shape[0];
            var colLength = matrix.Shape[1];
            var res = new Matrix<TNum>(rowLength, 1);

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                for (var colIdx = 0; colIdx < colLength; colIdx++)
                {
                    res[rowIdx, 0] += matrix[rowIdx, colIdx];
                }
            }

            return res;
        }
        private static Matrix<TNum> SumUpRow<TNum>(this Matrix<TNum> matrix) where TNum : INumberBase<TNum>
        {
            var rowLength = matrix.Shape[0];
            var colLength = matrix.Shape[1];
            var res = new Matrix<TNum>(1, colLength);

            for (var colIdx = 0; colIdx < colLength; colIdx++)
            {
                for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
                {
                    res[0, colIdx] += matrix[rowIdx, colIdx];
                }
            }
            
            return res;
        }
        public static Matrix<TNum> Sum<TNum>(this Matrix<TNum> matrix) where TNum : INumberBase<TNum>
        {
            var shape = matrix.Shape;
            var rowLength = shape[0];
            var colLength = shape[1];
            var res = new Matrix<TNum>(1, 1);

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                for (var colIdx = 0; colIdx < colLength; colIdx++)
                {
                    res[0, 0] += matrix[rowIdx, colIdx];
                }
            }

            return res;
        }
        #endregion

        public static Matrix<TNum> MapElementwise<TNum>(this Matrix<TNum> matrix, Func<TNum, TNum> func) where TNum : INumberBase<TNum>
        {
            var shape = matrix.Shape;
            var rowLength = shape[0];
            var colLength = shape[1];
            var res = new Matrix<TNum>(rowLength, colLength);

            for (var rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                for (var colIdx = 0; colIdx < colLength; colIdx++)
                {
                    res[rowIdx, colIdx] = func(matrix[rowIdx, colIdx]);
                }
            }

            return res;
        }
        public static List<int[]> FilterElementwise<TNum>(this Matrix<TNum> matrix, Func<TNum, bool> func) where TNum : INumberBase<TNum>
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
