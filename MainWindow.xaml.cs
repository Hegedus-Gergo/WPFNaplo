using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace WpfOsztalyzas
{
    public partial class MainWindow : Window
    {
        string fajlNev = "naplo.txt";
        ObservableCollection<Osztalyzat> jegyek = new ObservableCollection<Osztalyzat>();

        public MainWindow()
        {
            InitializeComponent();
            var dialog = new OpenFileDialog();
            dialog.FileName = "naplo.csv";
            dialog.DefaultExt = ".csv"; 
            dialog.Filter = "Text documents (.csv)|*.csv";
            bool? result = dialog.ShowDialog();
            jegyek.Clear();
            StreamReader sr = new StreamReader(fajlNev);
            while (!sr.EndOfStream)
            {
                string[] mezok = sr.ReadLine().Split(";");
                Osztalyzat ujJegy = new Osztalyzat(mezok[0], mezok[1], mezok[2], int.Parse(mezok[3]));
                jegyek.Add(ujJegy);
            }
            sr.Close();
            dgJegyek.ItemsSource = jegyek;
            lblUtvonal.Content = dialog.FileName;
            lblJegyekSzamaSzam.Content = dgJegyek.Items.Count;
            datDatum.SelectedDate = DateTime.Now;
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            string teljesNev = txtNev.Text;
            string[] nevSplit = teljesNev.Split(" ");
            int nevekSzama = nevSplit.Count();
            bool nevKevesebbMintHaromKarakter = false;
            DateTime currentDateTime = DateTime.Now;
            DateTime selectedDateTime = datDatum.SelectedDate.Value;
            foreach (string nevResz in nevSplit)
            {
                if (nevResz.Count() < 3)
                {
                    nevKevesebbMintHaromKarakter = true;
                    break;
                }
            }

            if (nevekSzama >= 2)
            {
                if (nevKevesebbMintHaromKarakter == false)
                {
                    if (selectedDateTime < currentDateTime)
                    {
                        string csvSor = $"{txtNev.Text};{datDatum.Text};{cboTantargy.Text};{sliJegy.Value}";
                        StreamWriter sw = new StreamWriter(fajlNev, append: true);
                        sw.WriteLine(csvSor);
                        sw.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nem lehet jövőbeli időpontot beállítani!");
                    }
                }
                else
                {
                    MessageBox.Show("A nevek legalább 3 betűből kell álljanak!");
                }
            }
            else
            {
                MessageBox.Show("Ez nem egy teljes név!");
            }
            dgJegyek.ItemsSource = jegyek;
            dgJegyek.Items.Refresh();
        }

        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {
            jegyek.Clear();
            StreamReader sr = new StreamReader(fajlNev);
            while (!sr.EndOfStream)
            {
                string[] mezok = sr.ReadLine().Split(";");
                Osztalyzat ujJegy = new Osztalyzat(mezok[0], mezok[1], mezok[2], int.Parse(mezok[3]));
                jegyek.Add(ujJegy);
            }
            sr.Close();
            dgJegyek.ItemsSource = jegyek;
        }

        private void sliJegy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblJegy.Content = sliJegy.Value; 
        }
    }
}
