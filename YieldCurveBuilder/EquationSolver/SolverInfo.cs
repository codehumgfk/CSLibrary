using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public class SolverInfo
    {
        public ESolver SolverKind;
        public SolverConfig Config;

        public SolverInfo(ESolver eSolver, SolverConfig sConfig)
        {
            SolverKind = eSolver;
            Config = sConfig;
        }
    }
}
