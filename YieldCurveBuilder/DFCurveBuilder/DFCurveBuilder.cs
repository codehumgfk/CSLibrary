using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EquationSolver;
using Interpolator;
using MarketDataHelper;
using MarketProduct.Instances;
using MarketProduct.Instances.SwapProduct;

namespace DFCurveBuilder
{
    public static class DFCurveBuilder
    {
        public static DiscountFactorSeries Build(InterestRateIndex irIndex, MarketDataBox dataBox, SolverInfo sInfo, EInterpolator eInterpolator=EInterpolator.Linear)
        {
            switch (irIndex.IndexKind)
            {
                case EIndexKind.Libor:
                    return BuildFromLibor(irIndex, dataBox, sInfo, eInterpolator);
                case EIndexKind.Tonar:
                    return BuildFromOis(irIndex, dataBox, sInfo, eInterpolator);
                default:
                    throw new NotImplementedException(string.Format("A method building DF curve from {0} is not implemented.", irIndex.IndexKind));
            }
        }
        private static DiscountFactorSeries BuildFromOis(InterestRateIndex irIndex, MarketDataBox dataBox, SolverInfo sInfo, EInterpolator eInterpolator)
        {
            var dfS = new DiscountFactorSeries(irIndex, eInterpolator);
            var oisQKeys = dataBox.MarketQuote.Keys.Where(qKey => qKey.Product == EProduct.Ois).OrderBy(qKey => qKey.ProductTerm).ToArray();

            foreach (var oisQKey in oisQKeys)
            {
                var oisRate = dataBox.MarketQuote[oisQKey];
                var oisSchedule = dataBox.Schedules[oisQKey];
                var oisProduct = new OIS(oisRate, oisSchedule);
                var kvPair = GetDFFromOis(oisProduct, dfS, sInfo);
                dfS.Add(kvPair.Key, kvPair.Value);
            }

            return dfS;
        }
        private static KeyValuePair<double, double> GetDFFromOis(OIS oisProduct, DiscountFactorSeries dfS, SolverInfo sInfo)
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

        private static DiscountFactorSeries BuildFromLibor(InterestRateIndex irIndex, MarketDataBox dataBox, SolverInfo sInfo, EInterpolator eInterpolator)
        {
            throw new NotImplementedException();
        }
    }
}
