using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver.Instances
{
    public class BisectionSolver : AbstractSolver
    {
        public BisectionSolver(SolverConfig sConfig) : base(sConfig) { }

        public override double Solve(Func<double, double> func, double xMin, double xMax, double x0)
        {
            CheckInputParams(func, xMin, xMax, x0);

            var a = xMin;
            var b = xMax;
            var m = (xMin + xMax) / 2.0;
            
            for (var i = 0; i < Config.MaxSteps; i++)
            {
                if (Math.Abs(func(m)) < Config.Accuracy) return m;
                if (func(a) * func(m) > 0) a = m;
                else b = m;
                m = (a + b) / 2.0;
            }

            return m;
        }
    }
}
