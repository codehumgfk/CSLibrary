using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.Polynomial.OrthogonalPolynomial
{
    public static class ChebyshevFirst
    {
        public static Func<double, double> GetFunction(int n)
        {
            if (n < 0) throw new ArgumentException("An integer must be positive.");
            return (double x) => Math.Cos(n * x);
        }
    }
}
