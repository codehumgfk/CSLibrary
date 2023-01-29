using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurveBuilder.DFCurve;
using EquationSolver;
using CDSPricer.Convention;
using CDSPricer.Market;
using CDSPricer.Product;
using CDSPricer.Schedular;

namespace CDSPricer.HazardCurve
{
    public static class HazardCurveBuilder
    {
        public static HazardRateSeries Build(DateTime tradeDate, List<CDSQuoteKey> cdsKeys, Dictionary<CDSQuoteKey, CDSQuoteRate> quoteDict, Dictionary<string, CDSConvention> convDict, DiscountFactorSeries dfSeires, SolverInfo sInfo, List<DateTime> holiday)
        {
            var hazard = new HazardRateSeries();
            var orderedKeys = cdsKeys.OrderBy(key => key.Tenor).ToList();

            foreach (var cdsKey in orderedKeys)
            {
                var cdsRate = quoteDict[cdsKey];
                var convention = convDict[cdsKey.Currency];
                GetHazadRate(hazard, tradeDate, cdsKey, cdsRate, convention, dfSeires, sInfo, holiday);
            }

            return hazard;
        }
        private static void GetHazadRate(HazardRateSeries hazard, DateTime tradeDate, CDSQuoteKey cdsKey, CDSQuoteRate cdsRate, CDSConvention convention, DiscountFactorSeries dfSeries, SolverInfo sInfo, List<DateTime> holiday)
        {
            var schedule = CDSScheduleMaker.GetSchedule(tradeDate, cdsKey.Tenor, convention, holiday);
            var assm = new CDSAssumption();
            var cds = new CreditDefaultSwap(1.0e8, cdsRate.ParSpread, cdsRate.Coupon, cdsRate.RecoveryRate, schedule, assm);
            var maturityDays = schedule.MaturityDays;

            Func<double, double> func = (x) =>
            {
                var tempHazard = hazard.Copy();
                tempHazard.Add(maturityDays, x);
                return cds.GetPresentValue(dfSeries, tempHazard);
            };

            var h0 = cdsRate.ParSpread / (1.0 - cdsRate.RecoveryRate);
            var solver = SolverFactory.GetSolver(sInfo);
            var h = solver.Solve(func, 0.0, h0 * 10.0, h0);

            hazard.Add(maturityDays, h);
        }
    }
}
