using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolator
{
    public abstract class AbstractInterpolator : IInterpolator
    {
        protected IList<double> xs;
        protected IList<double> ys;

        public AbstractInterpolator(IList<double> xList, IList<double> yList)
        {
            xs = xList;
            ys = yList;
        }
        public abstract double Interpolate(double x, bool allowExtrapolation=false);
        private void CheckInputList()
        {
            if (xs == null || ys == null) throw new ArgumentException("Input lists must not be null.");
            if (xs.Count != ys.Count) throw new ArgumentException("Counts of input lists must be same.");
            CheckXListSorted(xs);
        }
        private void CheckXListSorted(IList<double> xList)
        {
            for (var i = 0; i < xList.Count - 1; i++)
            {
                if (xList[i] > xList[i + 1]) throw new ArgumentException("The input xs must be sorted.");
            }
        }
        
        protected int FindIndexOfBottomLimit(double x)
        {
            if (x < xs[0]) return -1;
            var length = xs.Count;
            if (xs[length - 1] <= x) return length - 1;
            for (var i = 0; i < length - 1; i++)
            {
                if (xs[i] <= x && x < xs[i + 1]) return i;
            }
            throw new ArgumentOutOfRangeException(string.Format("Impossible to find an index. Inspect the member xs( input: {0}).", x));
        }
    }
}
