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
        public virtual Vector this[List<int> indexList]
        {
            get { throw new NotImplementedException("This method is not overrided."); }
        }
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
        public virtual double Norm => throw new NotImplementedException("This property is not overrided");
        public virtual void SetValue(double[] arr) { throw new NotImplementedException("This method is not overrided."); }
        public virtual double[] To1DArray() { throw new NotImplementedException("This method is not overrided."); }
    }
}
