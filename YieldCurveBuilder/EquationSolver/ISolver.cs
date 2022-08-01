using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public interface ISolver
    {
        public double Solve(Func<double, double> func, double xMin, double xMax, double x0);
    }
}
