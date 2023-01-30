using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.RandomProcess
{
    public class ConstantProcess : IRandomProcess
    {
        private double _Constant;

        public ConstantProcess(double constant)
        {
            _Constant = constant;
        }
        public double Generate(double dt, double random)
        {
            return _Constant;
        }
        public void Reset() { }
        public double Constant
        {
            get { return _Constant; }
        }
        public void ChangeConstant(double constant)
        {
            _Constant = constant;
        }
    }
}
