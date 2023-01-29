using System;
using System.Collections.Generic;
using System.Text;

namespace MathUtils.RandomProcess
{
    public class GeometricBrownianMotion : IRandomProcess
    {
        private double _Drift;
        private double _Diffusion;
        private double _CurrentValue;
        private double _InitialValue;
        public readonly bool SaveCurrent;

        public GeometricBrownianMotion(double drift, double diff, double initial, bool saveCurrent=true)
        {
            _Drift = drift;
            _Diffusion = diff;
            _CurrentValue = initial;
            _InitialValue = initial;
            SaveCurrent = saveCurrent;
        }

        public double Generate(double dt, double random)
        {
            var S0 = _CurrentValue;
            var mu = _Drift;
            var std = _Diffusion;

            var res = S0 * Math.Exp((mu - Math.Pow(std, 2.0) / 2.0) * dt + std * random);

            if (SaveCurrent) _CurrentValue = res;

            return res;
        }
        public void Reset()
        {
            _CurrentValue = _InitialValue;
        }
        public double Drift
        {
            get { return _Drift; }
        }
        public double Diffusion
        {
            get { return _Diffusion; }
        }
        public double CurrentValue
        {
            get { return _CurrentValue; }
        }
        public double InitialValue
        {
            get { return _InitialValue; }
        }
        public void ChangeDrift(double drift)
        {
            _Drift = drift;
        }
        public void ChangeDiffusion(double diff)
        {
            _Diffusion = diff;
        }
        public void ChangeCurrentValue(double current)
        {
            _CurrentValue = current;
        }
        public void ChangeInitialValue(double initial)
        {
            _InitialValue = initial;

        }
    }
}
