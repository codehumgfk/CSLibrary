using System;
using System.Collections.Generic;
using System.Numerics;

namespace MathUtils.LinearAlgebra
{
    public class ColumnVector<TNum> : Vector<TNum> where TNum : INumberBase<TNum>
    {
        private TNum[,] _Vector;

        #region Constructor
        public ColumnVector(int length) : base(length)
        {
            _Vector = new TNum[1, Length];
        }
        public ColumnVector(TNum[] arr) : base(arr.Length)
        {
            _Vector = new TNum[1, Length];
            SetValue(arr);
        }
        private ColumnVector(ColumnVector<TNum> colVec) : base(colVec.Length)
        {
            _Vector = new TNum[1, Length];
            SetValue(colVec.To1DArray());
        }
        #endregion

        #region Indexer
        public override TNum this[int i]
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
        public override ColumnVector<TNum> this[IList<int> indexList]
        {
            get 
            {
                var len = indexList.Count;
                var res = new ColumnVector<TNum>(len);
                for (var i = 0; i < len; i++)
                {
                    CheckIndex(indexList[i]);
                    res[i] = _Vector[0, indexList[i]];
                }
                return res;
            }
        }
        #endregion
        public string Name { get; set; }
        public int[] Shape => new int[2] { 1, Length };
        public RowVector<TNum> T => Transpose();
        public RowVector<TNum> Transpose()
        {
            var arr = To1DArray();

            return new RowVector<TNum>(arr);
        }
        public override void SetValue(TNum[] arr)
        {
            for (var i = 0; i < Length; i++)
            {
                _Vector[0, i] = arr[i];
            }
        }
        public override TNum[] To1DArray()
        {
            var res = new TNum[Length];

            for (var i = 0; i < Length; i++)
            {
                res[i] = _Vector[0, i];
            }

            return res;
        }
        public override ColumnVector<TNum> Clone() => new ColumnVector<TNum>(this);
        public override ColumnVector<TNum> MapElementwise(Func<TNum, TNum> func)
        {
            var res = new ColumnVector<TNum>(Length);

            for (var colIdx = 0; colIdx < Length; colIdx++)
            {
                res[colIdx] = func(this[colIdx]);
            }

            return res;
        }
        public Matrix<TNum> ToMatrix()
        {
            var res = new Matrix<TNum>(1, Length);
            res.SetColumnVector(0, this);

            return res;
        }
        #region Operator Overload
        public static ColumnVector<TNum> operator +(ColumnVector<TNum> vec1, ColumnVector<TNum> vec2)
        {
            CheckVecLength(vec1, vec2);
            var length = vec1.Length;
            var res = new ColumnVector<TNum>(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec1[i] + vec2[i];
            }

            return res;
        }
        public static ColumnVector<TNum> operator -(ColumnVector<TNum> vec)
        {
            var length = vec.Length;
            var res = new ColumnVector<TNum>(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = -vec[i];
            }

            return res;
        }
        public static ColumnVector<TNum> operator -(ColumnVector<TNum> vec1, ColumnVector<TNum> vec2)
        {
            var vec2Neg = -vec2;
            return vec1 + vec2Neg;
        }
        public static ColumnVector<TNum> operator *(TNum n, ColumnVector<TNum> vec)
        {
            var length = vec.Length;
            var res = new ColumnVector<TNum>(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec[i] * n;
            }

            return res;
        }
        public static ColumnVector<TNum> operator *(ColumnVector<TNum> rowVec, TNum n)
        {
            return n * rowVec;
        }
        public static TNum operator *(ColumnVector<TNum> colVec, RowVector<TNum> rowVec)
        {
            CheckVecLength(colVec, rowVec);
            var res = TNum.Zero;

            for (var i = 0; i < colVec.Length; i++)
            {
                res += colVec[i] * rowVec[i];
            }

            return res;
        }
        public static ColumnVector<TNum> operator /(ColumnVector<TNum> vec1, ColumnVector<TNum> vec2)
        {
            throw new NotSupportedException();
        }
        public static ColumnVector<TNum> operator /(ColumnVector<TNum> vec, TNum n)
        {
            var length = vec.Length;
            var res = new ColumnVector<TNum>(length);

            for (var i = 0; i < length; i++)
            {
                res[i] = vec[i] / n;
            }

            return res;
        }
        #endregion
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
