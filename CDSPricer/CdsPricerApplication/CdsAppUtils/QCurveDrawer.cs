using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDSPricer.HazardCurve;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace CdsPricerApplication.CdsAppUtils
{
    public static class QCurveDrawer
    {
        public static PlotModel Draw(HazardRateSeries hazard)
        {
            var plotModel = new PlotModel();

            var xMin = 0.0;
            var xMax = hazard.Days[hazard.Days.Count() - 1];
            var Q = new SurvivalProbSeries(hazard);

            plotModel.Series.Add(new FunctionSeries((double x) => Q[x] * 100, xMin, xMax, 10));

            var xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Day";
            xAxis.Minimum = xMin;
            xAxis.Maximum = xMax;
            xAxis.MajorStep = 300.0;
            xAxis.TickStyle = TickStyle.Crossing;

            var yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Survival Probability(%)";
            yAxis.Maximum = 100.0;
            yAxis.MajorStep = 10.0;
            yAxis.TickStyle = TickStyle.Crossing;

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            plotModel.Background = OxyColors.White;

            plotModel.InvalidatePlot(true);

            return plotModel;
        }
    }
}
