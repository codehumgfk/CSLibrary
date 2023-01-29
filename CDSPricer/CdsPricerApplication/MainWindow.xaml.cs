using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using CDSPricer.Convention;
using CDSPricer.CsvHelper;
using CDSPricer.HazardCurve;
using CDSPricer.Market;
using EquationSolver;
using Interpolator;
using MarketDataHelper;
using MarketDataHelper.CsvHelper;
using MarketDataHelper.InterestRate;

namespace CdsPricerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<DateTime> TKYHoliday;
        private SolverInfo SInfo;
        private DiscountFactorSeries Ois;
        private Dictionary<string, CDSConvention> CdsConvention;
        private Dictionary<CDSQuoteKey, CDSQuoteRate> CdsQuote;
        private HazardRateSeries Hazard;
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            var asof = DateTime.Today;
            TKYHoliday = HolidayDictMaker.GetHolidayDict(CdsAppUtils.PathSetter.Holiday)["TKY"];

            var mds = MDSMaker.GetMarketDataSet(CdsAppUtils.PathSetter.OisConvention, CdsAppUtils.PathSetter.OisMarket, CdsAppUtils.PathSetter.Holiday, asof);
            var irIndex = new InterestRateIndex(EIndexType.Tonar, 1.0, "JPY");
            SInfo = new SolverInfo(ESolver.Bisection, new SolverConfig());
            Ois = DFCurveBuilder.Build(irIndex, mds, SInfo);
            DFGraph.DrawDFCurve(Ois);
            CdsConvention = CDSConventionDictMaker.GetDict(CdsAppUtils.PathSetter.CDSConvention);
            CdsQuote = CDSQuoteDictMaker.GetDict(CdsAppUtils.PathSetter.CDSMarket);
            MarketData.InitializeData(CdsQuote);
            //var filename = "TOYOTA-MOTOR-CORPORATION.csv";
            //File.WriteAllLines(Path.Combine(CdsAppUtils.PathSetter.OutputDir, filename), hazard.CsvFormat);
        }

        private void QCuveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MarketData.RecoveryRate.Content == null)
            {
                MessageBox.Show("Choose Entity first!");
                return;
            }

            var qkeys = CdsQuote.Keys.Where(key => key.Entity == (string)MarketData.FirmName.SelectedItem).ToList();
            Hazard = CdsAppUtils.CdsPricerCaller.GetHazardRateSeries(DateTime.Today, qkeys, CdsQuote, CdsConvention, Ois, TKYHoliday);
            UserInput.InitializeData(CdsQuote[qkeys[0]].RecoveryRate, Ois, CdsConvention[qkeys[0].Currency], Hazard, TKYHoliday);
            QGraph.DrawSurvivalCurve(Hazard);
        }
    }
}
