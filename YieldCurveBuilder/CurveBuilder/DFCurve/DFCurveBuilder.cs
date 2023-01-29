using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquationSolver;
using Interpolator;
using MarketDataHelper;
using MarketDataHelper.InterestRate;
using MarketDataHelper.MarketProduct.FiccProduct.Instances;

namespace CurveBuilder.DFCurve
{
    public static class DFCurveBuilder
    {
        public static DiscountFactorSeries Build(InterestRateIndex irIndex, MarketDataSet dataSet, SolverInfo sInfo, EInterpolator eInterpolator = EInterpolator.Linear)
        {
            switch (irIndex.IndexType)
            {
                case EIndexType.Libor:
                    return BuildFromLibor(irIndex, dataSet, sInfo, eInterpolator);
                case EIndexType.Tonar:
                    return BuildFromOis(irIndex, dataSet, sInfo, eInterpolator);
                default:
                    throw new NotImplementedException(string.Format("A method building DF curve from {0} is not implemented.", irIndex.IndexType));
            }
        }
        private static DiscountFactorSeries BuildFromOis(InterestRateIndex irIndex, MarketDataSet dataSet, SolverInfo sInfo, EInterpolator eInterpolator)
        {
            var dfS = new DiscountFactorSeries(irIndex, eInterpolator);
            var oisQKeys = dataSet.QuoteRates.Keys.OrderBy(qKey => qKey.ProductTerm).ToArray();

            foreach (var oisQKey in oisQKeys)
            {
                var oisRate = dataSet.QuoteRates[oisQKey];
                var oisSchedule = dataSet.Schedules[oisQKey];
                var oisProduct = new Ois(oisRate, oisSchedule);
                var kvPair = GetDFFromOis(oisProduct, dfS, sInfo);
                dfS.Add(kvPair.Key, kvPair.Value);
            }

            return dfS;
        }
        private static KeyValuePair<double, double> GetDFFromOis(Ois oisProduct, DiscountFactorSeries dfS, SolverInfo sInfo)
        {
            var daysToFinalPayment = oisProduct.DaysToFinalPayment;
            var solver = SolverFactory.GetSolver(sInfo);
            Func<double, double> func = (double x) =>
            {
                var tempDFS = new DiscountFactorSeries(dfS);
                tempDFS.Add(daysToFinalPayment, x);
                return oisProduct.GetPresentValue(tempDFS.Interpolator);
            };

            var dfAtPerRate = solver.Solve(func, 0, 2.0, 1.0);

            return new KeyValuePair<double, double>(daysToFinalPayment, dfAtPerRate);
        }

        private static DiscountFactorSeries BuildFromLibor(InterestRateIndex irIndex, MarketDataSet dataSet, SolverInfo sInfo, EInterpolator eInterpolator)
        {
            throw new NotImplementedException();
        }
    }
}
