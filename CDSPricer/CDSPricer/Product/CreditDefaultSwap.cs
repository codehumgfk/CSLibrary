using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurveBuilder.DFCurve;
using CDSPricer.HazardCurve;
using CDSPricer.Schedular;
using EquationSolver;


namespace CDSPricer.Product
{
    public class CreditDefaultSwap
    {
        public string Entity;
        public string Currency;
        public int Tenor; // months
        public double Notional;
        public double RunningCoupon;
        public double ParSpread;
        public double RecoveryRate;
        public CDSSchedule Schedule;
        public CDSAssumption Assumption;
        public bool IsProtectionBuyer;

        public CreditDefaultSwap(double notinal, double parSpread, double coupon, double recoveryRate, CDSSchedule schedule, CDSAssumption assumption, bool isProtBuyer=true)
        {
            Notional = notinal;
            ParSpread = parSpread;
            RunningCoupon = coupon;
            RecoveryRate = recoveryRate;
            Schedule = schedule;
            Assumption = assumption;
            IsProtectionBuyer = isProtBuyer;
        }
        private CreditDefaultSwap Copy()
        {
            return new CreditDefaultSwap(Notional, ParSpread, RunningCoupon, RecoveryRate, Schedule, Assumption, IsProtectionBuyer);
        }
        public double GetPresentValue(DiscountFactorSeries dfSeries, HazardRateSeries hazard)
        {
            var protLeg = GetProtLeg(dfSeries, hazard);
            var premLeg = GetPremLeg(dfSeries, hazard);
            var sign = IsProtectionBuyer ? 1.0 : -1.0;

            return sign * (protLeg - premLeg);
        }
        private double GetProtLeg(DiscountFactorSeries dfSeries, HazardRateSeries hazard)
        {
            if (Assumption.PayProtNextPayDate) return CalcProtLegProtNextPayDate(dfSeries, hazard);
            return CalcProtLeg(dfSeries, hazard);
        }
        private double CalcProtLegProtNextPayDate(DiscountFactorSeries dfSeries, HazardRateSeries hazard)
        {
            var pv = 0.0;
            var asof = Schedule.TradeDate;
            var Q = new SurvivalProbSeries(hazard);

            foreach (var accrualSchedule in Schedule.AccrualSchedules)
            {
                var t1 = (accrualSchedule.PaymentDate - asof).TotalDays;
                var t0 = (accrualSchedule.StartDate - asof).TotalDays;

                pv += dfSeries[t1] * (Q[t0] - Q[t1]);
            }

            var tv = (Schedule.CashSettleDate - asof).TotalDays;
            pv *= Notional * (1.0 - RecoveryRate) / dfSeries[tv];

            return pv;
        }
        private double e(double x)
        {
            return 1.0 + x / 2.0 + Math.Pow(x, 2) / 6.0 + Math.Pow(x, 3) / 24.0;
        }
        private double CalcProtLeg(DiscountFactorSeries dfSeries, HazardRateSeries hazard)
        {
            var pv = 0.0;
            var asof = Schedule.TradeDate;
            var tau = dfSeries.Days.Union(hazard.Days).Where(d => d < Schedule.MaturityDays).ToList();
            tau.Add(Schedule.MaturityDays);
            var Q = new SurvivalProbSeries(hazard);

            for (var i = 0; i < tau.Count() - 1; i++)
            {
                var f1 = Math.Log(dfSeries[tau[i]]) - Math.Log(dfSeries[tau[i + 1]]);
                var h1 = Math.Log(Q[tau[i]]) - Math.Log(Q[tau[i + 1]]);
                var x = f1 + h1;
                var B0 = dfSeries[tau[i]] * Q[tau[i]];
                var B1 = dfSeries[tau[i + 1]] * Q[tau[i + 1]];

                if (Math.Abs(x) < 1e-3) pv += B0 * h1 * e(-x);
                else pv += h1 / x * (B0 - B1);
            }

            var tv = (Schedule.CashSettleDate - asof).TotalDays;
            pv *= Notional * (1.0 - RecoveryRate) / dfSeries[tv];

            return pv;
        }
        private double GetPremLeg(DiscountFactorSeries dfSeries, HazardRateSeries hazard, bool calcPUF=false)
        {
            if (Assumption.DefaultMidWay) return CalcPremLegDefaultMidWay(dfSeries, hazard, calcPUF);
            return CalcPremLeg(dfSeries, hazard, calcPUF);
        }
        private double CalcPremLegDefaultMidWay(DiscountFactorSeries dfSeries, HazardRateSeries hazard, bool calcPUF)
        {
            var pv = 0.0;
            var asof = Schedule.TradeDate;
            var C = calcPUF ? RunningCoupon : ParSpread;
            var Q = new SurvivalProbSeries(hazard);

            foreach (var accrualSchedule in Schedule.AccrualSchedules)
            {
                var t1 = (accrualSchedule.PaymentDate - asof).TotalDays;
                var t0 = (accrualSchedule.StartDate - asof).TotalDays;

                pv += accrualSchedule.Dcf * dfSeries[t1] * (Q[t0] + Q[t1]) / 2.0;
            }

            var tv = (Schedule.CashSettleDate - asof).TotalDays;
            pv /= dfSeries[tv];
            pv -= Schedule.AccruedDcf;
            pv *= Notional * C;

            return pv;
        }
        private double ee(double x)
        {
            return 0.5 + x / 3.0 + Math.Pow(x, 2) / 8.0 + Math.Pow(x, 3) / 30.0;
        }
        private double CalcPremLeg(DiscountFactorSeries dfSeries, HazardRateSeries hazard, bool calcPUF)
        {
            var pv = 0.0;
            var Q = new SurvivalProbSeries(hazard);
            var asof = Schedule.TradeDate;
            var C = calcPUF ? RunningCoupon : ParSpread;
            var eta = 365.0 / 360.0;

            foreach (var schedule in Schedule.AccrualSchedules)
            {
                var ek = (schedule.EndDate - asof).TotalDays;
                var sk = (schedule.StartDate - asof).TotalDays;
                var t0 = sk;
                var Delta = schedule.Dcf;
                
                var Ik = 0.0;
                var tau = dfSeries.Days.Union(hazard.Days).Where(d => sk < d && d < ek).ToList();
                tau.Add(ek);
                foreach (var t1 in tau)
                {
                    var f1 = Math.Log(dfSeries[t0]) - Math.Log(dfSeries[t1]);
                    var h1 = Math.Log(Q[t0]) - Math.Log(Q[t1]);
                    var x = f1 + h1;
                    var B0 = dfSeries[t0] * Q[t0];
                    var B1 = dfSeries[t1] * Q[t1];
                    var Delta0 = t1 - t0;

                    if (Math.Abs(x) < 1e-2) Ik += h1 * B0 * ((t0 - sk) * e(-x) + Delta0 * ee(-x));
                    else Ik += h1 / x * (Delta0 * ((B0 - B1) / x - B1) + (t0 - sk * (B0 - B1)));
                    t0 = t1;
                }

                pv += Delta * dfSeries[ek] * Q[ek] - eta * Ik;
            }

            var tv = (Schedule.CashSettleDate - asof).TotalDays;
            pv /= dfSeries[tv];
            pv -= Schedule.AccruedDcf;
            pv *= Notional * C;

            return pv;
        }
        public double GetPointUpFront(DiscountFactorSeries dfSeries, HazardRateSeries hazard)
        {
            var premLegParSpread = GetPremLeg(dfSeries, hazard);
            var premLegCoupon = GetPremLeg(dfSeries, hazard, true);

            return (premLegParSpread - premLegCoupon) / Notional;
        }
        public double GetImpliedParSpread(DiscountFactorSeries dfSeries, HazardRateSeries hazard)
        {
            var tempCds = Copy();
            var sInfo = new SolverInfo(ESolver.Bisection, new SolverConfig(1e-6));
            var solver = SolverFactory.GetSolver(sInfo);

            Func<double, double> func = (x) =>
             {
                 tempCds.ParSpread = x;
                 return tempCds.GetPresentValue(dfSeries, hazard);
             };
            var res = solver.Solve(func, 0.0, 0.05, ParSpread);

            return res;
        }
    }
}
