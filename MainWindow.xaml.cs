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

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool vanErtekMegadva = false;
        String nev;
        String datum;
        String tantargy;
        int jegy;
        public MainWindow()
        {
            InitializeComponent();
            dpDatum.SelectedDate = DateTime.Today;
            lblJegy.Content = sliJegy.Value;
        }

        private void cboTantargy_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void dpDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void sliJegy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblJegy.Content = Math.Round(sliJegy.Value);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            vanErtekMegadva = true;
        }
    }
}
