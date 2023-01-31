using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.LinearAlgebra
{
    public class Matrix
    {
        private int RowLength;
        private int ColumnLength;
        private double[,] _Matrix;

        #region Constructor
        public Matrix(int rowLength, int columnLength)
        {
            RowLength = rowLength;
            ColumnLength = columnLength;
            _Matrix = new double[rowLength, columnLength];
        }
        public Matrix(double[,] array)
        {
            RowLength = array.GetLength(0);
            ColumnLength = array.GetLength(1);
            _Matrix = new double[RowLength, ColumnLength];

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    _Matrix[i, j] = array[i, j];
                }
            }
        }
        public Matrix(List<List<double>> nestedList)
        {
            CheckNestedList(nestedList);
            
            RowLength = nestedList.Count;
            ColumnLength = nestedList[0].Count;
            _Matrix = new double[RowLength, ColumnLength];

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    _Matrix[i, j] = nestedList[i][j];
                }
            }
        }
        private void CheckNestedList(List<List<double>> nested)
        {
            var colLength = nested[0].Count;
            
            foreach(var list in nested)
            {
                if (list.Count != colLength) throw new NotSupportedException();
            }
        }
        #endregion

        #region Indexer
        public double this[int i, int j]
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
        public double[] this[List<int[]> indexList]
        {
            get
            {
                var len = indexList.Count;
                var res = new double[len];
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
        public int[] Shape
        {
            get { return new int[2] { RowLength, ColumnLength }; }
        }
        public bool IsSquare
        {
            get { return RowLength == ColumnLength; }
        }
        public Matrix T
        {
            get { return Transpose(); }
        }
        public Matrix Transpose()
        {
            var transposed = new double[ColumnLength, RowLength];

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    transposed[j, i] = _Matrix[i, j];
                }
            }

            return new Matrix(transposed);
        }
        public double Tr => GetTrace();
        public double GetTrace()
        {
            if (!IsSquare) throw new NotSupportedException();
            var res = 0.0;

            for (var i = 0; i < RowLength; i++)
            {
                res += _Matrix[i, i];
            }

            return res;
        }
        public double Det
        {
            get { return GetDeterminant(); }
        }
        public double GetDeterminant()
        {
            if (!IsSquare) throw new NotSupportedException();
            if (RowLength == 1 && ColumnLength == 1) return _Matrix[0, 0];

            var res = 0.0;
            for (var i = 0; i < RowLength; i++)
            {
                res += _Matrix[i, 0] * GetCofactor(i, 0);
            }

            return res;
        }
        public double GetCofactor(int rowIdx, int colIdx)
        {
            CheckRowColIndex(rowIdx, colIdx);
            var sign = Math.Pow(-1, rowIdx + 1 + colIdx + 1);
            var subMat = RemoveRow(rowIdx).RemoveColumn(colIdx);

            return sign * subMat.Det;
        }
        public bool IsRegular
        {
            get { return Det != 0.0; }
        }
        public Matrix Inv
        {
            get { return GetInverseMatrix(); }
        }
        public Matrix GetInverseMatrix()
        {
            if (!IsRegular) throw new NotSupportedException();

            var det = Det;
            var adjMat = GetAdjugateMatrix();

            return adjMat / det;
        }
        public Matrix GetAdjugateMatrix()
        {
            var res = new Matrix(RowLength, ColumnLength);

            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    res[j, i] = GetCofactor(i, j);
                }
            }

            return res;
        }
        public RowVector InnerProduct(RowVector vec)
        {
            CheckVectorLengthForInnerProduct(vec);
            var arr = new double[RowLength];

            for (var i = 0; i < RowLength; i++)
            {
                var colVec = GetColumnVector(i);
                arr[i] = colVec * vec;
            }

            return new RowVector(arr);
        }
        private void CheckVectorLengthForInnerProduct(Vector vec)
        {
            if (vec.Length != ColumnLength) throw new ArgumentException("Impossible to calculate an inner product. Wrong length.");
        }
        public Matrix InnerProduct(Matrix b)
        {
            CheckMatrixShapeForInnerProduct(b);
            var newColumnLength = b.Shape[1];
            var resMat = new Matrix(RowLength, newColumnLength);

            for (var k = 0; k < newColumnLength; k++)
            {
                var vec = b.GetRowVector(k);
                var resVec = InnerProduct(vec);
                resMat.SetRowVector(k, resVec);
            }

            return resMat;
        }
        private void CheckMatrixShapeForInnerProduct(Matrix b)
        {
            var bshape = b.Shape;

            if (bshape[0] != ColumnLength) throw new ArgumentException("Impossible to take an inner product. Wrong shape.");
        }
        public Matrix HadamarProduct(Matrix b)
        {
            CheckMatrixShapeForHadamarProduct(b);
            var res = new Matrix(RowLength, ColumnLength);
            for (var i = 0; i < RowLength; i++)
            {
                for (var j = 0; j < ColumnLength; j++)
                {
                    res[i, j] = _Matrix[i, j] * b[i, j];
                }
            }

            return res;
        }
        private void CheckMatrixShapeForHadamarProduct(Matrix b)
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
        public ColumnVector GetColumnVector(int rowIdx)
        {
            CheckRowIndex(rowIdx);
            var arr = new double[ColumnLength];
            for (var j = 0; j < ColumnLength; j++)
            {
                arr[j] = _Matrix[rowIdx, j];
            }

            return new ColumnVector(arr);
        }
        public RowVector GetRowVector(int colIdx)
        {
            CheckColIndex(colIdx);
            var arr = new double[RowLength];
            for (var i = 0; i < RowLength; i++)
            {
                arr[i] = _Matrix[i, colIdx];
            }

            return new RowVector(arr);
        }
        public void SetRowVector(int i, RowVector vec)
        {
            CheckColIndex(i);
            if (vec.Length != RowLength) throw new ArgumentException();

            for (var rowIdx = 0; rowIdx < RowLength; rowIdx++)
            {
                _Matrix[rowIdx, i] = vec[rowIdx];
            }
        }
        public void SetColumnVector(int row, ColumnVector vec)
        {
            CheckRowIndex(row);
            if (vec.Length != ColumnLength) throw new ArgumentException();

            for (var colIdx = 0; colIdx < ColumnLength; colIdx++)
            {
                _Matrix[row, colIdx] = vec[colIdx];
            }
        }
        public Matrix RemoveRow(int rowIdx)
        {
            CheckRowIndex(rowIdx);
            var res = new Matrix(RowLength - 1, ColumnLength);

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
        public Matrix RemoveColumn(int colIdx)
        {
            CheckColIndex(colIdx);
            var res = new Matrix(RowLength, ColumnLength - 1);

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
        public RowVector ToRowVector()
        {
            if (ColumnLength != 1) throw new NotSupportedException("This matrix cannot be converted to RowVector.");

            var res = new RowVector(RowLength);
            
            for (var i = 0; i < RowLength; i++)
            {
                res[i] = _Matrix[i, 0];
            }

            return res;
        }
        public ColumnVector ToColumnVector()
        {
            if (RowLength != 1) throw new NotSupportedException("This matrix cannot be converted to ColumnVector.");

            var res = new ColumnVector(ColumnLength);

            for (var i = 0; i < ColumnLength; i++)
            {
                res[i] = _Matrix[0, i];
            }

            return res;
        }
        #endregion

        #region Operator Overload
        public static Matrix operator +(Matrix a, Matrix b)
        {
            CheckShapeForLinearOperator(a, b);
            var shape = a.Shape;
            var res = new Matrix(shape[0], shape[1]);

            for (var i = 0; i < shape[0]; i++)
            {
                for (var j = 0; j < shape[1]; j++)
                {
                    res[i, j] = a[i, j] + b[i, j];
                }
            }

            return res;
        }
        public static Matrix operator -(Matrix a)
        {
            var shape = a.Shape;
            var res = new Matrix(shape[0], shape[1]);

            for (var i = 0; i < shape[0]; i++)
            {
                for (var j = 0; j < shape[1]; j++)
                {
                    res[i, j] = -a[i, j];
                }
            }

            return res;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            var c = -b;

            return a + c;
        }
        private static void CheckShapeForLinearOperator(Matrix a, Matrix b)
        {
            var aShape = a.Shape;
            var bShape = b.Shape;

            if (aShape[0] != bShape[0] || aShape[1] != bShape[1]) throw new ArgumentException("Impossible to treat matrices with different shapes.");
        }
        public static Matrix operator *(double n, Matrix mat)
        {
            var shape = mat.Shape;
            var res = new Matrix(shape[0], shape[1]);

            for (var i = 0; i < shape[0]; i++)
            {
                for (var j = 0; j < shape[1]; j++)
                {
                    res[i, j] = n * mat[i, j];
                }
            }

            return res;
        }
        public static Matrix operator *(Matrix mat, double n)
        {
            return n * mat;
        }
        public static RowVector operator *(Matrix a, RowVector b)
        {
            return a.InnerProduct(b);
        }
        public static ColumnVector operator *(ColumnVector a, Matrix b)
        {
            var newColVec = a.ToMatrix();
            var resMatrix = newColVec * b;

            return resMatrix.GetColumnVector(0);
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            return a.InnerProduct(b);
        }
        public static Matrix operator/ (Matrix a, Matrix b)
        {
            throw new NotSupportedException("Division of matrices is not supported.");
        }
        public static Matrix operator /(Matrix a, double n)
        {
            var shape = a.Shape;
            var res = new Matrix(shape[0], shape[1]);

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
