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
using CDSPricer.HazardCurve;
using OxyPlot;

namespace CdsPricerApplication
{
    /// <summary>
    /// SurvivalCurveGraph.xaml の相互作用ロジック
    /// </summary>
    public partial class SurvivalCurveGraph : UserControl
    {
        public SurvivalCurveGraph()
        {
            InitializeComponent();
        }
        public void DrawSurvivalCurve(HazardRateSeries hazard)
        {
            SurvivalCurve.Model = CdsAppUtils.QCurveDrawer.Draw(hazard);
        }
    }
}
