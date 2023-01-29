using System;
using System.Collections.Generic;
using System.Text;
using CDSPricer.Convention;
using CDSPricer.HazardCurve;
using CDSPricer.Market;
using CDSPricer.Product;
using CDSPricer.Schedular;
using CurveBuilder.DFCurve;
using EquationSolver;

namespace CdsPricerApplication.CdsAppUtils
{
    public static class CdsPricerCaller
    {
        public static HazardRateSeries GetHazardRateSeries(DateTime asof, List<CDSQuoteKey> qkeys, Dictionary<CDSQuoteKey, CDSQuoteRate> cdsQuote, Dictionary<string, CDSConvention> convs, DiscountFactorSeries dfSeries, List<DateTime> holiday=null)
        {
            var sInfo = new SolverInfo(ESolver.Bisection, new SolverConfig(1e-6));

            return HazardCurveBuilder.Build(asof, qkeys, cdsQuote, convs, dfSeries, sInfo, holiday);
        }
        public static double GetPresentValue(UserInput input, double recovRate, CDSConvention convention, DiscountFactorSeries dfSeries, HazardRateSeries hazard, List<DateTime> holiday=null)
        {
            var assm = new CDSAssumption();
            var schedule = CDSScheduleMaker.GetSchedule(DateTime.Today, input.Tenor, convention, holiday);
            var cds = new CreditDefaultSwap(input.Notional, input.ParSpread, 0.0, recovRate, schedule, assm, input.IsProtectionBuyer);

            return cds.GetPresentValue(dfSeries, hazard);
        }
        public static double GetPointUpFront(UserInput input, double recovRate, CDSConvention convention, DiscountFactorSeries dfSeries, HazardRateSeries hazard, List<DateTime> holiday = null)
        {
            var assm = new CDSAssumption();
            var schedule = CDSScheduleMaker.GetSchedule(DateTime.Today, input.Tenor, convention, holiday);
            var cds = new CreditDefaultSwap(input.Notional, input.ParSpread, 0.0, recovRate, schedule, assm, input.IsProtectionBuyer);

            return cds.GetPointUpFront(dfSeries, hazard);
        }
        public static double GetImpliedParSpread(UserInput input, double recovRate, CDSConvention convention, DiscountFactorSeries dfSeries, HazardRateSeries hazard, List<DateTime> holiday = null)
        {
            var assm = new CDSAssumption();
            var schedule = CDSScheduleMaker.GetSchedule(DateTime.Today, input.Tenor, convention, holiday);
            var cds = new CreditDefaultSwap(input.Notional, input.ParSpread, 0.0, recovRate, schedule, assm, input.IsProtectionBuyer);

            return cds.GetImpliedParSpread(dfSeries, hazard);
        }
    }
}
