using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MathUtils.LinearAlgebra
{
    public class RowVector : Vector
    {
        private double[,] _Vector;
        public RowVector(int length) : base(length)
        {
            _Vector = new double[Length, 1];
        }
        public RowVector(double[] arr) : base(arr.Length)
        {
            _Vector = new double[Length, 1];
            for (var i = 0; i < Length; i++)
            {
                _Vector[i, 0] = arr[i];
            }
        }
        #region Indexer
        public override double this[int i]
        {
            get 
            {
                CheckIndex(i);
                return _Vector[i, 0];
            }
            set 
            {
                CheckIndex(i);
                _Vector[i, 0] = value;
            }
        }
        public override RowVector this[List<int> indexList]
        {
            get 
            { 
                var len = indexList.Count;
                var res = new RowVector(len);
                for (var i = 0; i < len; i++)
                {
                    CheckIndex(indexList[i]);
                    res[i] = _Vector[indexList[i], 0];
                }
                return res;
            }
        }
        #endregion
        public string Name { get; set; }
        public int[] Shape
        {
            get{ return new int[2] { Length, 1 }; }
        }
        public ColumnVector T
        {
            get { return Transpose(); }
        }
        public ColumnVector Transpose()
        {
            var arr = To1DArray();

            return new ColumnVector(arr);
        }
        public override double Norm => CalculateNorm();
        private double CalculateNorm()
        {
            var res = 0.0;

            for (var rowIdx = 0; rowIdx < Length; rowIdx++)
            {
                res += Math.Pow(_Vector[rowIdx, 0], 2.0);
            }

            return Math.Sqrt(res);
        }
        public override void SetValue(double[] arr)
        {
            CheckArrayLength(arr);
            for (var i = 0; i < Length; i++)
            {
                _Vector[i, 0] = arr[i];
            }
        }
        public static RowVector operator +(RowVector vec1, RowVector vec2)
        {
            CheckVecLength(vec1, vec2);
            var length = vec1.Length;
            var res = new RowVector(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec1[i] + vec2[i];    
            }

            return res;
        }
        public static RowVector operator -(RowVector vec)
        {
            var length = vec.Length;
            var res = new RowVector(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = -vec[i];
            }

            return res;
        }
        public static RowVector operator -(RowVector vec1, RowVector vec2)
        {
            var vec2Neg = -vec2;
            return vec1 + vec2Neg;
        }
        public static RowVector operator *(double n, RowVector rowVec)
        {
            var length = rowVec.Length;
            var res = new RowVector(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = rowVec[i] * n;
            }

            return res;
        }
        public static RowVector operator *(RowVector rowVec, double n)
        {
            return n * rowVec;
        }
        public static Matrix operator *(RowVector rowVec, ColumnVector colVec)
        {
            var rowLength = rowVec.Length;
            var colLength = colVec.Length;
            var res = new Matrix(rowLength, colLength);

            for (var j = 0; j < colLength; j++)
            {
                var newRow = rowVec * colVec[j];
                res.SetRowVector(j, newRow);
            }

            return res;
        }
        public static RowVector operator /(RowVector vec1, RowVector vec2)
        {
            throw new NotSupportedException();
        }
        public static RowVector operator /(RowVector vec, double n)
        {
            var length = vec.Length;
            var res = new RowVector(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec[i] / n;
            }

            return res;
        }
        public override double[] To1DArray()
        {
            var res = new double[Length];

            for (var i = 0; i < Length; i++)
            {
                res[i] = _Vector[i, 0];
            }

            return res;
        }
        public Matrix ToMatrix()
        {
            var res = new Matrix(Length, 1);
            res.SetRowVector(1, this);

            return res;
        }
        public override string ToString()
        {
            var txt = string.Format("Name: {0}\n", Name);
            txt += string.Format("Shape: ({0}, 1)\n", Length);
            for (var i = 0; i < Length; i++)
            {
                var line = "";
                if (i == 0) line += "[[";
                else line += " [";

                line += string.Format("{0:F}]", _Vector[i, 0]);

                if (i == Length - 1) line += "]";
                else line += "\n";

                txt += line;
            }

            return txt;
        }
    }
}
