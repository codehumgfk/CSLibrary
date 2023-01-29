using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.LinearAlgebra
{
    public abstract class Vector
    {
        public readonly int Length;
        public Vector(int length)
        {
            Length = length;
        }
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
        public abstract void SetValue(double[] arr);
        public abstract double[] To1DArray();
    }
}
