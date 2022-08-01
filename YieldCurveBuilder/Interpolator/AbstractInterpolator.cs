using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolator
{
    public abstract class AbstractInterpolator : IInterpolator
    {
        public readonly IList<double> xs;
        public readonly IList<double> ys;

        public AbstractInterpolator(IList<double> xList, IList<double> yList)
        {
            xs = xList;
            ys = yList;
        }

        public abstract double Interpolate(double x, bool allowExtrapolation);

        private void CheckXListSorted(IList<double> xList)
        {
            for (var i = 0; i < xList.Count - 1; i++)
            {
                if (xList[i] > xList[i + 1]) throw new ArgumentException("The input xs must be sorted.");
            }
        }

        protected void CheckXIndex(int index, bool allowExtrapolation)
        {
            if (index == -1 && !allowExtrapolation) throw new ArgumentException(string.Format("Extrapolation is not allowed( the input is smaller than xMin:{0}).", xs[0]));
            var length = xs.Count;
            if (index == length && !allowExtrapolation) throw new ArgumentException(string.Format("Extrapolation is not allowed( the input is bigger than xMax:{0}).", xs[length - 1]));
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
