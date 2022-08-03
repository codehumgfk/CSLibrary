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

namespace YieldCurveBuilderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window LoadingWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowLoadingWindow(bool isLoading)
        {
            if (isLoading)
            {
                this.IsEnabled = false;
                LoadingWindow = new ProgressWindow();
                LoadingWindow.Show();
            }
            else
            {
                LoadingWindow.Close();
                this.IsEnabled = true;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var selected = MessageBox.Show("Do you really close this window?", "You are closing this window.", MessageBoxButton.YesNo);
            if (selected == MessageBoxResult.No) e.Cancel = true;
        }
    }
}
