using System;
using System.Collections.Generic;
using System.Numerics;

namespace MathUtils.LinearAlgebra
{
    public class Vector<TNum> where TNum : INumberBase<TNum>
    {
        public readonly int Length;

        #region Constructor
        public Vector(int length)
        {
            Length = length;
        }
        #endregion

        #region Indexer
        public virtual TNum this[int index] 
        { 
            get { throw new NotImplementedException("This method is not overrided."); }
            set { throw new NotImplementedException("This method is not overrided."); } 
        }
        public virtual Vector<TNum> this[IList<int> indexList] => throw new NotImplementedException("This method is not overrided.");
        #endregion
        protected void CheckIndex(int i)
        {
            if (i < 0 || Length <= i) throw new ArgumentException("Wrong index");
        }
        protected void CheckArrayLength(TNum[] arr)
        {
            if (arr.Length != Length) throw new ArgumentException("The input array has different length.");
        }
        internal static void CheckVecLength(Vector<TNum> vec1, Vector<TNum> vec2)
        {
            if (vec1.Length != vec2.Length) throw new ArgumentException("Lengths of two vectors are different.");
        }
        
        public virtual void SetValue(TNum[] arr) { throw new NotImplementedException("This method is not overrided."); }
        public virtual TNum[] To1DArray() { throw new NotImplementedException("This method is not overrided."); }
        public virtual Vector<TNum> Clone() => throw new NotImplementedException("This method is not overrided.");
        public virtual Vector<TNum> MapElementwise(Func<TNum, TNum> func) => throw new NotImplementedException("This method is not overrided.");
        public List<int> FilterElementwise(Func<TNum, bool> func)
        {
            var colLength = Length;
            var res = new List<int>();

            for (var colIdx = 0; colIdx < colLength; colIdx++)
            {
                if (func(this[colIdx])) res.Add(colIdx);
            }

            return res;
        }
        #region Statistics Methods and Properties
        public TNum Sum => CalculateSum();
        private TNum CalculateSum()
        {
            var res = TNum.Zero;

            for (var i = 0; i < Length; i++)
            {
                res += this[i];
            }

            return res;
        }
        public TNum Mean => Sum / TNum.CreateSaturating<int>(Length);
        public TNum UnbiasedVariance => CalcUnbiasedVariance();
        private TNum CalcUnbiasedVariance()
        {
            if (Length == 1) throw new NotSupportedException("Cannnot calculate an unbiased variance for 1 sample.");
            var mean = Mean;
            var res = TNum.Zero;

            for (var i = 0; i < Length; i++)
            {
                var diff = this[i] - mean;
                res += diff * diff;
            }
            res /= TNum.CreateSaturating(Length - 1.0);

            return res;
        }
        public TNum StdDev => TNum.CreateSaturating(Math.Sqrt(double.CreateSaturating(UnbiasedVariance)));
        public TNum StdErr => StdDev / TNum.CreateSaturating(double.Sqrt(Length));
        public TNum Norm => CalculateNorm();
        private TNum CalculateNorm()
        {
            var res = TNum.Zero;
            for (var i = 0; i < Length; i++)
            {
                res += this[i] * this[i];
            }
            
            return TNum.CreateSaturating(double.Sqrt(double.CreateSaturating(res)));
        }
        #endregion
    }
}
