using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using System.Drawing;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace SeriesManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        List<string> data = new List<string>();
        Features features = new Features();
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"~\\ManagerOfSeries\SeriesManager\SeriesManager\SeriesManager\bin\Debug";
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            foreach(string series in features.LoadList(fileName))
            {
                lstSeries.Items.Add(series);
                btnAdd.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Visible;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.EditPanel.Visibility = Visibility.Visible;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (string i in lstSeries.Items)
                {
                    if (i.Contains(txtSeriesToEdit.Text))
                    {
                        lstSeries.Items.Remove(i);
                        lstSeries.Items.Add(txtNewName.Text);
                    }
                }
            }
            catch
            {

            }
            List<string> series = new List<string>();
            foreach (string i in lstSeries.Items)
            {
                series.Add(i);
            }
            this.EditPanel.Visibility = Visibility.Hidden;
            File.WriteAllLines("Series.csv", series);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPanel.Visibility = Visibility.Visible;
        }

        private void BtnApplyAddition_Click(object sender, RoutedEventArgs e)
        {
            lstSeries.Items.Add(txtSeriesToBeAdded.Text);
            List<string> series = new List<string>();
            foreach (string i in lstSeries.Items)
            {
                series.Add(i);
            }
            this.AddPanel.Visibility = Visibility.Hidden;
            File.WriteAllLines("Series.csv", series);
        }
    }
}
