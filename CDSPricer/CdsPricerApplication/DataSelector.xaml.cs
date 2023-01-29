using System;
using System.Collections.Generic;
using System.Linq;
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
using CDSPricer.Market;

namespace CdsPricerApplication
{
    /// <summary>
    /// DataSelector.xaml の相互作用ロジック
    /// </summary>
    public partial class DataSelector : UserControl
    {
        public Dictionary<CDSQuoteKey, CDSQuoteRate> CdsQuote;
        public DataSelector()
        {
            InitializeComponent();
        }
        public void InitializeData(Dictionary<CDSQuoteKey, CDSQuoteRate> cdsQuote)
        {
            CdsQuote = cdsQuote;
            FirmName.ItemsSource = CdsQuote.Keys.Select(key => key.Entity).OrderBy(e => e).ToList();
        }

        private void FirmName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirmName.SelectedItem == null)
            {
                RunningCoupon.Content = "";
                ParSpread.Content = "";
                RecoveryRate.Content = "";

                return;
            }
            var entity = (string)FirmName.SelectedItem;
            var key = CdsQuote.Keys.Where(key => key.Entity == entity).FirstOrDefault();
            var quote = CdsQuote[key];
            RunningCoupon.Content = quote.Coupon * 10000;
            ParSpread.Content = quote.ParSpread * 10000;
            RecoveryRate.Content = quote.RecoveryRate * 100;
        }
    }
}
