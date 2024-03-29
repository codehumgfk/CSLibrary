﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.Functions.Polynomial.OrthogonalPolynomial
{
    public static class ChebyshevSecond
    {
        public static Func<double, double> GetFunction(int n)
        {
            if (n < 0) throw new ArgumentException("An integer must be positive.");
            return (x) => Math.Sin(n * x) / Math.Sin(x);
        }
    }
}
