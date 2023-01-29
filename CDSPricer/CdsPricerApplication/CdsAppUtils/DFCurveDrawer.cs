using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurveBuilder.DFCurve;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot.Axes;

namespace CdsPricerApplication.CdsAppUtils
{
    public static class DFCurveDrawer
    {
        public static PlotModel Draw(DiscountFactorSeries ois)
        {
            var plotModel = new PlotModel();

            var xMin = 0.0;
            var xMax = ois.Days[ois.Days.Where(d => d < 1900).Count() - 1];

            plotModel.Series.Add(new FunctionSeries((double x) => ois[x], xMin, xMax, 10));

            var xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Day";
            xAxis.Minimum = xMin;
            xAxis.Maximum = xMax;
            xAxis.MajorStep = 300.0;
            xAxis.TickStyle = TickStyle.Crossing;

            var yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Discount Factor";
            yAxis.Minimum = 0.96;
            yAxis.Maximum = 1.01;
            yAxis.MajorStep = 0.1;
            yAxis.TickStyle = TickStyle.Crossing;

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            plotModel.Background = OxyColors.White;

            plotModel.InvalidatePlot(true);

            return plotModel;
        }
    }
}
