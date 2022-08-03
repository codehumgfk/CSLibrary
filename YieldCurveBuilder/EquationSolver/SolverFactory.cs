using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public static class SolverFactory
    {
        public static ISolver GetSolver(SolverInfo solverInfo)
        {
            return GetSolver(solverInfo.SolverKind, solverInfo.Config);
        }
        public static ISolver GetSolver(ESolver eSolver, SolverConfig sConfig)
        {
            switch (eSolver)
            {
                case ESolver.Bisection:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException(string.Format("The selected solver, {0}, cannot be made in SolverFactory.", eSolver));
            }
        }
    }
}
