using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public class SolverConfig
    {
        public double Accuracy;
        public int MaxSteps;

        public SolverConfig(double accuracy=1e-8, int maxSteps=1000)
        {
            CheckInputs(accuracy, maxSteps);
            Accuracy = accuracy;
            MaxSteps = maxSteps;
        }

        public SolverConfig Copy()
        {
            return new SolverConfig(Accuracy, MaxSteps);
        }

        private void CheckInputs(double accuracy, int maxSteps)
        {
            if (accuracy <= 0) throw new ArgumentException("Accuracy must be strictly nonnegative.");
            if (maxSteps <= 0) throw new ArgumentException("MaxSteps must be strictly nonnegative.");
        }
    }
}
