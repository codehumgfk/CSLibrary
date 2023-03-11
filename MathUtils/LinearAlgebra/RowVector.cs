using System;
using System.Collections.Generic;
using System.Numerics;

namespace MathUtils.LinearAlgebra
{
    public class RowVector<TNum> : Vector<TNum> where TNum : INumberBase<TNum>
    {
        private TNum[,] _Vector;
        #region Constructor
        public RowVector(int length) : base(length)
        {
            _Vector = new TNum[Length, 1];
        }
        public RowVector(TNum[] arr) : base(arr.Length)
        {
            _Vector = new TNum[Length, 1];
            SetValue(arr);
        }
        private RowVector(RowVector<TNum> rowVec) : base(rowVec.Length)
        {
            _Vector = new TNum[Length, 1];
            SetValue(rowVec.To1DArray());
        }
        #endregion
        #region Indexer
        public override TNum this[int i]
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
        public override RowVector<TNum> this[IList<int> indexList]
        {
            get 
            { 
                var len = indexList.Count;
                var res = new RowVector<TNum>(len);
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
        public int[] Shape=> new int[2] { Length, 1 };
        public ColumnVector<TNum> T=> Transpose();
        public ColumnVector<TNum> Transpose()
        {
            var arr = To1DArray();

            return new ColumnVector<TNum>(arr);
        }
        public override void SetValue(TNum[] arr)
        {
            CheckArrayLength(arr);
            for (var i = 0; i < Length; i++)
            {
                _Vector[i, 0] = arr[i];
            }
        }
        #region Operator Overload
        public static RowVector<TNum> operator +(RowVector<TNum> vec1, RowVector<TNum> vec2)
        {
            CheckVecLength(vec1, vec2);
            var length = vec1.Length;
            var res = new RowVector<TNum>(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec1[i] + vec2[i];    
            }

            return res;
        }
        public static RowVector<TNum> operator -(RowVector<TNum> vec)
        {
            var length = vec.Length;
            var res = new RowVector<TNum>(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = -vec[i];
            }

            return res;
        }
        public static RowVector<TNum> operator -(RowVector<TNum> vec1, RowVector<TNum> vec2)
        {
            var vec2Neg = -vec2;
            return vec1 + vec2Neg;
        }
        public static RowVector<TNum> operator *(TNum n, RowVector<TNum> rowVec)
        {
            var length = rowVec.Length;
            var res = new RowVector<TNum>(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = rowVec[i] * n;
            }

            return res;
        }
        public static RowVector<TNum> operator *(RowVector<TNum> rowVec, TNum n)
        {
            return n * rowVec;
        }
        public static Matrix<TNum> operator *(RowVector<TNum> rowVec, ColumnVector<TNum> colVec)
        {
            var rowLength = rowVec.Length;
            var colLength = colVec.Length;
            var res = new Matrix<TNum>(rowLength, colLength);

            for (var j = 0; j < colLength; j++)
            {
                var newRow = rowVec * colVec[j];
                res.SetRowVector(j, newRow);
            }

            return res;
        }
        public static RowVector<TNum> operator /(RowVector<TNum> vec1, RowVector<TNum> vec2)
        {
            throw new NotSupportedException();
        }
        public static RowVector<TNum> operator /(RowVector<TNum> vec, TNum n)
        {
            var length = vec.Length;
            var res = new RowVector<TNum>(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec[i] / n;
            }

            return res;
        }
        #endregion
        public override TNum[] To1DArray()
        {
            var res = new TNum[Length];

            for (var i = 0; i < Length; i++)
            {
                res[i] = _Vector[i, 0];
            }

            return res;
        }
        public override RowVector<TNum> Clone() => new RowVector<TNum>(this);
        public override RowVector<TNum> MapElementwise(Func<TNum, TNum> func)
        {
            var res = new RowVector<TNum>(Length);

            for (var rowIdx = 0; rowIdx < Length; rowIdx++)
            {
                res[rowIdx] = func(this[rowIdx]);
            }

            return res;
        }
        public Matrix<TNum> ToMatrix()
        {
            var res = new Matrix<TNum>(Length, 1);
            res.SetRowVector(1, this);

            return res;
        }
        #region Object Method Override
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
        #endregion
    }
}
