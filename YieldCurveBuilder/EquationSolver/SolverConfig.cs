using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public class SolverConfig
    {
        public double Accuracy;
        public int MaxSteps;

        public SolverConfig(double accuracy, int maxSteps)
        {
            Accuracy = accuracy;
            MaxSteps = maxSteps;
        }

        public SolverConfig Copy()
        {
            return new SolverConfig(Accuracy, MaxSteps);
        }
    }
}
