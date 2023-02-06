using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.LinearAlgebra
{
    public class Vector
    {
        public readonly int Length;

        #region Constructor
        public Vector(int length)
        {
            Length = length;
        }
        #endregion

        #region Indexer
        public virtual double this[int index] 
        { 
            get { throw new NotImplementedException("This method is not overrided."); }
            set { throw new NotImplementedException("This method is not overrided."); } 
        }
        public virtual Vector this[IList<int> indexList] => throw new NotImplementedException("This method is not overrided.");
        #endregion
        protected void CheckIndex(int i)
        {
            if (i < 0 || Length <= i) throw new ArgumentException("Wrong index");
        }
        protected void CheckArrayLength(double[] arr)
        {
            if (arr.Length != Length) throw new ArgumentException("The input array has different length.");
        }
        internal static void CheckVecLength(Vector vec1, Vector vec2)
        {
            if (vec1.Length != vec2.Length) throw new ArgumentException("Lengths of two vectors are different.");
        }
        
        public virtual void SetValue(double[] arr) { throw new NotImplementedException("This method is not overrided."); }
        public virtual double[] To1DArray() { throw new NotImplementedException("This method is not overrided."); }
        #region Statistics Methods and Properties
        public double Sum => CalculateSum();
        private double CalculateSum()
        {
            var res = 0.0;

            for (var i = 0; i < Length; i++)
            {
                res += this[i];
            }

            return res;
        }
        public double Mean => Sum / (double)Length;
        public double UnbiasedVariance => CalcUnbiasedVariance();
        private double CalcUnbiasedVariance()
        {
            if (Length == 1) throw new NotSupportedException("Cannnot calculate an unbiased variance for 1 sample.");
            var mean = Mean;
            var res = 0.0;

            for (var i = 0; i < Length; i++)
            {
                res += Math.Pow(this[i] - mean, 2.0);
            }
            res /= (Length - 1.0);

            return res;
        }
        public double StdDev => Math.Sqrt(UnbiasedVariance);
        public double StdErr => StdDev / Math.Sqrt(Length);
        public double Norm => CalculateNorm();
        private double CalculateNorm()
        {
            var res = 0.0;
            for (var i = 0; i < Length; i++)
            {
                res += Math.Pow(this[i], 2.0);
            }

            return Math.Sqrt(res);
        }
        #endregion
    }
}
