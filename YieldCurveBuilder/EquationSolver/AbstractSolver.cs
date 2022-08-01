using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public abstract class AbstractSolver
    {
        SolverConfig Configuration;

        public abstract double Solve(Func<double,double> func, double xMin, double xMax, double x0);

    }
}
