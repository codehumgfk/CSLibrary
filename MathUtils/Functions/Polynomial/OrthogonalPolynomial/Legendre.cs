﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.Functions.Polynomial.OrthogonalPolynomial
{
    public static class Legendre
    {
        public static Func<double, double> GetFunction(int n)
        {
            if (n < 0) throw new ArgumentException("An integer must be positive.");
            if (n == 0) return (x) => 1.0;
            if (n == 1) return (x) => x;
            return (x) => (2.0 * (n - 1.0) * x * GetFunction(n - 1)(x) - (n - 1.0) * GetFunction(n - 2)(x)) / n;
        }

    }
}
