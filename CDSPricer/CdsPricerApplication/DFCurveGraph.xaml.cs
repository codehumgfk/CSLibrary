using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CurveBuilder.DFCurve;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Axes;

namespace CdsPricerApplication
{
    /// <summary>
    /// DFCurveGraph.xaml の相互作用ロジック
    /// </summary>
    public partial class DFCurveGraph : UserControl
    {
        public DFCurveGraph()
        {
            InitializeComponent();
        }
        public void DrawDFCurve(DiscountFactorSeries ois)
        {
            DFCurve.Model = CdsAppUtils.DFCurveDrawer.Draw(ois);
        }
    }
}
