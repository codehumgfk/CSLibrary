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
using CDSPricer.Convention;
using CDSPricer.HazardCurve;
using CurveBuilder.DFCurve;

namespace CdsPricerApplication
{
    /// <summary>
    /// UserInputCds.xaml の相互作用ロジック
    /// </summary>
    public partial class UserInputCds : UserControl
    {
        private bool IsReady;
        private double RecoveryRate;
        private DiscountFactorSeries Ois;
        private CDSConvention CdsConvention;
        private HazardRateSeries Hazard;
        private List<DateTime> Holiday;
        public UserInputCds()
        {
            InitializeComponent();
        }
        public void InitializeData(double recovRate, DiscountFactorSeries ois, CDSConvention convention, HazardRateSeries hazard, List<DateTime> holiday=null)
        {
            RecoveryRate = recovRate;
            Ois = ois;
            CdsConvention = convention;
            Hazard = hazard;
            Holiday = holiday;

        }
        private void CalcStart_Click(object sender, RoutedEventArgs e)
        {
            if (IsReady)
            {
                var notional = double.Parse(Notional.Text);
                var isProtectionBuyer = (string)Protection.SelectedItem == "Buy" ? true : false;
                var tenor = (int)TenorY.SelectedItem * 12 + (int)(TenorM.SelectedItem);
                var parSpread = double.Parse(ParSpread.Text) / 10000.0;
                var input = new CdsAppUtils.UserInput(notional, isProtectionBuyer, tenor, parSpread);
                PV.Content = (int)CdsAppUtils.CdsPricerCaller.GetPresentValue(input, RecoveryRate, CdsConvention, Ois, Hazard, Holiday);
                PUF.Content = CdsAppUtils.CdsPricerCaller.GetPointUpFront(input, RecoveryRate, CdsConvention, Ois, Hazard, Holiday) * 10000;
                ImpliedParSpread.Content = CdsAppUtils.CdsPricerCaller.GetImpliedParSpread(input, RecoveryRate, CdsConvention, Ois, Hazard, Holiday) * 10000;
            }
            else 
            {
                MessageBox.Show("Fill inputs first!");
                return;
            }
        }

        private void ParSpread_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParSpread.Text == null)
            {
                IsReady = false;
                return;
            }
            try
            {
                CdsAppUtils.TextChecker.Double(ParSpread.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ParSpread.Text = "";
            }
           
            IsReady = true;
        }

        private void TenorM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParSpread.Text = "";
        }

        private void Protection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TenorY.SelectedItem = null;
        }

        private void Notional_TextChanged(object sender, TextChangedEventArgs e)
        {
            Protection.SelectedItem = null;
        }

        private void TenorY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TenorY.SelectedItem == null)
            {
                TenorM.SelectedItem = null;
                return;
            }
            var selected = (int)TenorY.SelectedItem;

            if (selected == 0) TenorM.ItemsSource = CdsAppUtils.DefaultValueSetter.TenorMY0;
            else if (selected == 5) TenorM.ItemsSource = CdsAppUtils.DefaultValueSetter.TenorMY5;
            else TenorM.ItemsSource = CdsAppUtils.DefaultValueSetter.TenorMY1;

            TenorM.SelectedItem = null;
        }
    }
}
