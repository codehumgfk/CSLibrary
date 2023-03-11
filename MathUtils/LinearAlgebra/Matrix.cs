using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MathUtils.LinearAlgebra
{
    public class Matrix<TNum> where TNum : INumberBase<TNum>
    {
        private int RowLength;
        private int ColumnLength;
        private TNum[,] _Matrix;

        #region Constructor
        public Matrix(int rowLength, int columnLength)
        {
            if (rowLength < 1 || columnLength < 1) throw new ArgumentException("Lengths must be biggger than 1.");
            RowLength = rowLength;
            ColumnLength = columnLength;
            _Matrix = new TNum[rowLength, columnLength];
        }
        public Matrix(TNum[,] array)
        {
            RowLength = array.GetLength(0);
            ColumnLength = array.GetLength(1);
            _Matrix = new TNum[RowLength, ColumnLength];

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    _Matrix[i, j] = array[i, j];
                }
            }
        }
        public Matrix(List<List<TNum>> nestedList)
        {
            CheckNestedList(nestedList);
            
            RowLength = nestedList.Count;
            ColumnLength = nestedList[0].Count;
            _Matrix = new TNum[RowLength, ColumnLength];

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    _Matrix[i, j] = nestedList[i][j];
                }
            }
        }
        private void CheckNestedList(List<List<TNum>> nested)
        {
            var colLength = nested[0].Count;
            
            foreach(var list in nested)
            {
                if (list.Count != colLength) throw new NotSupportedException();
            }
        }
        #endregion

        #region Indexer
        public TNum this[int i, int j]
        {
            get
            {
                CheckRowColIndex(i, j);
                return _Matrix[i, j];
            }
            set
            {
                CheckRowColIndex(i, j);
                _Matrix[i, j] = value;
            }
        }
        public TNum[] this[List<int[]> indexList]
        {
            get
            {
                var len = indexList.Count;
                var res = new TNum[len];
                for (var i = 0; i < len; i++)
                {
                    var index = indexList[0];
                    if (index.Length != 2) throw new ArgumentException("Wrong format of an index array.");
                    CheckRowColIndex(index[0], index[1]);
                    res[i] = _Matrix[index[0], index[1]];
                }
                return res;
            }
        }
        #endregion
        public string Name { get; set; }
        public int[] Shape => new int[2] { RowLength, ColumnLength };
        
        public bool IsSquare => RowLength == ColumnLength;
        public Matrix<TNum> T => Transpose();
        public Matrix<TNum> Transpose()
        {
            var transposed = new TNum[ColumnLength, RowLength];

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    transposed[j, i] = _Matrix[i, j];
                }
            }

            return new Matrix<TNum>(transposed);
        }
        public TNum Tr => GetTrace();
        public TNum GetTrace()
        {
            if (!IsSquare) throw new NotSupportedException();
            var res = TNum.Zero;

            for (var i = 0; i < RowLength; i++)
            {
                res += _Matrix[i, i];
            }

            return res;
        }
        public TNum Det => GetDeterminant();
        public TNum GetDeterminant()
        {
            if (!IsSquare) throw new NotSupportedException();
            if (RowLength == 1 && ColumnLength == 1) return _Matrix[0, 0];

            var res = TNum.Zero;
            for (var i = 0; i < RowLength; i++)
            {
                res += _Matrix[i, 0] * GetCofactor(i, 0);
            }

            return res;
        }
        public TNum GetCofactor(int rowIdx, int colIdx)
        {
            CheckRowColIndex(rowIdx, colIdx);
            var sign = TNum.CreateSaturating(Math.Pow(-1, rowIdx + 1 + colIdx + 1));
            var subMat = RemoveRow(rowIdx).RemoveColumn(colIdx);

            return sign * subMat.Det;
        }
        public bool IsRegular => Det != TNum.Zero;
        public Matrix<TNum> Inv => GetInverseMatrix();
        public Matrix<TNum> GetInverseMatrix()
        {
            if (!IsRegular) throw new NotSupportedException();

            var det = Det;
            var adjMat = GetAdjugateMatrix();

            return adjMat / det;
        }
        public Matrix<TNum> GetAdjugateMatrix()
        {
            var res = new Matrix<TNum>(RowLength, ColumnLength);

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    res[j, i] = GetCofactor(i, j);
                }
            }

            return res;
        }
        public RowVector<TNum> InnerProduct(RowVector<TNum> vec)
        {
            CheckVectorLengthForInnerProduct(vec);
            var arr = new TNum[RowLength];

            for (var i = 0; i < RowLength; i++)
            {
                var colVec = GetColumnVector(i);
                arr[i] = colVec * vec;
            }

            return new RowVector<TNum>(arr);
        }
        private void CheckVectorLengthForInnerProduct(Vector<TNum> vec)
        {
            if (vec.Length != ColumnLength) throw new ArgumentException("Impossible to calculate an inner product. Wrong length.");
        }
        public Matrix<TNum> InnerProduct(Matrix<TNum> b)
        {
            CheckMatrixShapeForInnerProduct(b);
            var newColumnLength = b.Shape[1];
            var resMat = new Matrix<TNum>(RowLength, newColumnLength);

            for (var k = 0; k < newColumnLength; k++)
            {
                var vec = b.GetRowVector(k);
                var resVec = InnerProduct(vec);
                resMat.SetRowVector(k, resVec);
            }

            return resMat;
        }
        private void CheckMatrixShapeForInnerProduct(Matrix<TNum> b)
        {
            var bshape = b.Shape;

            if (bshape[0] != ColumnLength) throw new ArgumentException("Impossible to take an inner product. Wrong shape.");
        }
        public Matrix<TNum> HadamarProduct(Matrix<TNum> b)
        {
            CheckMatrixShapeForHadamarProduct(b);
            var res = new Matrix<TNum>(RowLength, ColumnLength);
            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    res[i, j] = _Matrix[i, j] * b[i, j];
                }
            }

            return res;
        }
        private void CheckMatrixShapeForHadamarProduct(Matrix<TNum> b)
        {
            var bShape = b.Shape;
            if (bShape[0] != RowLength || bShape[1] != ColumnLength) throw new ArgumentException("Impossible to take Hadamar product. Wrong shape.");
        }  
        private void CheckRowIndex(int i)
        {
            if (i < 0 || RowLength < i) throw new ArgumentException();
        }
        private void CheckColIndex(int j)
        {
            if (j < 0 || ColumnLength < j) throw new ArgumentException();
        }
        private void CheckRowColIndex(int i, int j)
        {
            CheckRowIndex(i);
            CheckColIndex(j);
        }
        public ColumnVector<TNum> GetColumnVector(int rowIdx)
        {
            CheckRowIndex(rowIdx);
            var arr = new TNum[ColumnLength];
            for (var j = 0; j < ColumnLength; j++)
            {
                arr[j] = _Matrix[rowIdx, j];
            }

            return new ColumnVector<TNum>(arr);
        }
        public RowVector<TNum> GetRowVector(int colIdx)
        {
            CheckColIndex(colIdx);
            var arr = new TNum[RowLength];
            for (var i = 0; i < RowLength; i++)
            {
                arr[i] = _Matrix[i, colIdx];
            }

            return new RowVector<TNum>(arr);
        }
        public void SetRowVector(int i, RowVector<TNum> vec)
        {
            CheckColIndex(i);
            if (vec.Length != RowLength) throw new ArgumentException();

            for (var rowIdx = 0; rowIdx < RowLength; rowIdx++)
            {
                _Matrix[rowIdx, i] = vec[rowIdx];
            }
        }
        public void SetColumnVector(int row, ColumnVector<TNum> vec)
        {
            CheckRowIndex(row);
            if (vec.Length != ColumnLength) throw new ArgumentException();

            for (var colIdx = 0; colIdx < ColumnLength; colIdx++)
            {
                _Matrix[row, colIdx] = vec[colIdx];
            }
        }
        public Matrix<TNum> RemoveRow(int rowIdx)
        {
            CheckRowIndex(rowIdx);
            var res = new Matrix<TNum>(RowLength - 1, ColumnLength);

            for (var i = 0; i < rowIdx; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    res[i, j] = _Matrix[i, j];
                }
            }
            for (var i = rowIdx + 1; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    res[i - 1, j] = _Matrix[i, j];
                }
            }

            return res;
        }
        public Matrix<TNum> RemoveColumn(int colIdx)
        {
            CheckColIndex(colIdx);
            var res = new Matrix<TNum>(RowLength, ColumnLength - 1);

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < colIdx; j++)
                {
                    res[i, j] = _Matrix[i, j];
                }

                for (var j = colIdx + 1; j < ColumnLength; j++)
                {
                    res[i, j - 1] = _Matrix[i, j];
                }
            }

            return res;
        }

        #region Convert Method
        public RowVector<TNum> ToRowVector()
        {
            if (ColumnLength != 1) throw new NotSupportedException("This matrix cannot be converted to RowVector.");

            var res = new RowVector<TNum>(RowLength);
            
            for (var i = 0; i < RowLength; i++)
            {
                res[i] = _Matrix[i, 0];
            }

            return res;
        }
        public ColumnVector<TNum> ToColumnVector()
        {
            if (RowLength != 1) throw new NotSupportedException("This matrix cannot be converted to ColumnVector.");

            var res = new ColumnVector<TNum>(ColumnLength);

            for (var i = 0; i < ColumnLength; i++)
            {
                res[i] = _Matrix[0, i];
            }

            return res;
        }
        #endregion

        #region Operator Overload
        public static Matrix<TNum> operator +(Matrix<TNum> a, Matrix<TNum> b)
        {
            CheckShapeForLinearOperator(a, b);
            var shape = a.Shape;
            var res = new Matrix<TNum>(shape[0], shape[1]);

            for (var i = 0; i < shape[0]; i++)
            {
                for (var j = 0; j < shape[1]; j++)
                {
                    res[i, j] = a[i, j] + b[i, j];
                }
            }

            return res;
        }
        public static Matrix<TNum> operator -(Matrix<TNum> a)
        {
            var shape = a.Shape;
            var res = new Matrix<TNum>(shape[0], shape[1]);

            for (var i = 0; i < shape[0]; i++)
            {
                for (var j = 0; j < shape[1]; j++)
                {
                    res[i, j] = -a[i, j];
                }
            }

            return res;
        }
        public static Matrix<TNum> operator -(Matrix<TNum> a, Matrix<TNum> b)
        {
            var c = -b;

            return a + c;
        }
        private static void CheckShapeForLinearOperator(Matrix<TNum> a, Matrix<TNum> b)
        {
            var aShape = a.Shape;
            var bShape = b.Shape;

            if (aShape[0] != bShape[0] || aShape[1] != bShape[1]) throw new ArgumentException("Impossible to treat matrices with different shapes.");
        }
        public static Matrix<TNum> operator *(TNum n, Matrix<TNum> mat)
        {
            var shape = mat.Shape;
            var res = new Matrix<TNum>(shape[0], shape[1]);

            for (var i = 0; i < shape[0]; i++)
            {
                for (var j = 0; j < shape[1]; j++)
                {
                    res[i, j] = n * mat[i, j];
                }
            }

            return res;
        }
        public static Matrix<TNum> operator *(Matrix<TNum> mat, TNum n)
        {
            return n * mat;
        }
        public static RowVector<TNum> operator *(Matrix<TNum> a, RowVector<TNum> b)
        {
            return a.InnerProduct(b);
        }
        public static ColumnVector<TNum> operator *(ColumnVector<TNum> a, Matrix<TNum> b)
        {
            var newColVec = a.ToMatrix();
            var resMatrix = newColVec * b;

            return resMatrix.GetColumnVector(0);
        }
        public static Matrix<TNum> operator *(Matrix<TNum> a, Matrix<TNum> b)
        {
            return a.InnerProduct(b);
        }
        public static Matrix<TNum> operator/ (Matrix<TNum> a, Matrix<TNum> b)
        {
            throw new NotSupportedException("Division of matrices is not supported.");
        }
        public static Matrix<TNum> operator /(Matrix<TNum> a, TNum n)
        {
            var shape = a.Shape;
            var res = new Matrix<TNum>(shape[0], shape[1]);

            for (var i = 0; i < shape[0]; i++)
            {
                for (var j = 0; j < shape[1]; j++)
                {
                    res[i, j] = a[i, j] / n;
                }
            }

            return res;
        }
        #endregion
        public override string ToString()
        {
            var txt = "Name: " + Name + "\n";
            txt += string.Format("Shape: ({0},{1})", RowLength, ColumnLength) + "\n";
            for (var i = 0; i < RowLength; i++)
            {
                var line="";
                
                if (i == 0) line += "[";
                else line += " ";

                line += GetColumnVector(i).ToStringForMatrix();
                
                if (i == RowLength - 1) line += "]\n";
                else line += "\n";

                txt += line;
            }

            return txt;
        }
    }
}
