using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public abstract class AbstractSolver
    {
        public readonly SolverConfig Config;

        public AbstractSolver(SolverConfig sConfig) 
        {
            Config = sConfig;
        }
        public abstract double Solve(Func<double,double> func, double xMin, double xMax, double x0);

        protected void CheckInputParams(Func<double, double> func, double xMin, double xMax, double x0)
        {
            if (xMin >= xMax) throw new ArgumentException("xMin < xMax must hold.");
            if (x0 < xMin || xMax < x0) throw new ArgumentException("xMin <= x0 <= xMax must hold.");
            if (func(xMin) * func(xMax) > 0) throw new ArgumentException("func(xMin) * func(xMax) < 0 must hold.");
        }
    }
}
