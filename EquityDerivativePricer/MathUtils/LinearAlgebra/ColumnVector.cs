using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.LinearAlgebra
{
    public class ColumnVector : Vector
    {
        private double[,] _Vector;

        public ColumnVector(int length) : base(length)
        {
            _Vector = new double[1, Length];
        }
        public ColumnVector(double[] arr) : base(arr.Length)
        {
            _Vector = new double[1, Length];
            for (var i = 0; i < Length; i++)
            {
                _Vector[0, i] = arr[i];
            }
        }
        public double this[int i]
        {
            get 
            {
                CheckIndex(i);
                return _Vector[0, i];
            }
            set
            {
                CheckIndex(i);
                _Vector[0, i] = value;
            }
        }
        public string Name { get; set; }
        public int[] Shape
        {
            get { return new int[2] { 1, Length }; }
        }
        public RowVector T
        {
            get { return Transpose(); }
        }
        public RowVector Transpose()
        {
            var arr = To1DArray();

            return new RowVector(arr);
        }
        public override void SetValue(double[] arr)
        {
            for (var i = 0; i < Length; i++)
            {
                _Vector[0, i] = arr[i];
            }
        }
        public override double[] To1DArray()
        {
            var res = new double[Length];

            for (var i = 0; i < Length; i++)
            {
                res[i] = _Vector[0, i];
            }

            return res;
        }
        public Matrix ToMatrix()
        {
            var res = new Matrix(1, Length);
            res.SetColumnVector(0, this);

            return res;
        }
        public static ColumnVector operator +(ColumnVector vec1, ColumnVector vec2)
        {
            CheckVecLength(vec1, vec2);
            var length = vec1.Length;
            var res = new ColumnVector(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec1[i] + vec2[i];
            }

            return res;
        }
        public static ColumnVector operator -(ColumnVector vec)
        {
            var length = vec.Length;
            var res = new ColumnVector(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = -vec[i];
            }

            return res;
        }
        public static ColumnVector operator -(ColumnVector vec1, ColumnVector vec2)
        {
            var vec2Neg = -vec2;
            return vec1 + vec2Neg;
        }
        public static ColumnVector operator *(double n, ColumnVector vec)
        {
            var length = vec.Length;
            var res = new ColumnVector(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec[i] * n;
            }

            return res;
        }
        public static ColumnVector operator *(ColumnVector rowVec, double n)
        {
            return n * rowVec;
        }
        public static double operator *(ColumnVector colVec, RowVector rowVec)
        {
            CheckVecLength(colVec, rowVec);
            var res = 0.0;

            for (var i = 0; i < colVec.Length; i++)
            {
                res += colVec[i] * rowVec[i];
            }

            return res;
        }
        public static ColumnVector operator /(ColumnVector vec1, ColumnVector vec2)
        {
            throw new NotSupportedException();
        }
        public static ColumnVector operator /(ColumnVector vec, double n)
        {
            var length = vec.Length;
            var res = new ColumnVector(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec[i] / n;
            }

            return res;
        }
        public string ToStringForMatrix()
        {
            var txt = "[";

            var arr = To1DArray();
            txt += string.Join(',', arr);
            txt += "]";

            return txt;
        }
        public override string ToString()
        {
            var txt = string.Format("Name: {0}\n", Name);
            txt += string.Format("Shape: (1, {0})\n", Length);

            txt += "[" + ToStringForMatrix() + "]";

            return txt;
        }
    }
}
