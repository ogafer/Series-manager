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

        #region Open and Load the file
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            lstSeries.Items.Clear();
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"~\\\SeriesManager\SeriesManager\bin\Debug";
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            foreach (string series in features.LoadList(fileName))
            {
                lstSeries.Items.Add(series);

            }
            if (!lstSeries.Items.IsEmpty)
            {
                btnAdd.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Visible;
                btnRemove.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Edit Series
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.EditPanel.Visibility = Visibility.Visible;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewName.Text) && !string.IsNullOrEmpty(txtSeason.Text))
            {
                bool hasSeries = false;
                try
                {
                    foreach (string item in lstSeries.Items)
                    {
                        if (item.Equals(txtNewName.Text))
                        {
                            MessageBox.Show("This Series already exists!");
                            hasSeries = true;
                            break;
                        }
                    }
                    if (!hasSeries)
                    {
                        foreach (string item in lstSeries.Items)
                        {
                            if (item.StartsWith(txtSeriesToEdit.Text))
                            {
                                lstSeries.Items.Remove(item);
                                lstSeries.Items.Add(txtNewName.Text + "-Season " + txtSeason.Text);
                            }
                        }
                        List<string> series = new List<string>();
                        foreach (string i in lstSeries.Items)
                        {
                            series.Add(i);
                        }
                        this.EditPanel.Visibility = Visibility.Hidden;
                        File.WriteAllLines("Series.csv", series);
                    }
                    this.EditPanel.Visibility = Visibility.Hidden;
                }
                catch
                {
                    this.EditPanel.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("Please fill every information!");
            }
        }

        #endregion

        #region Add Series
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPanel.Visibility = Visibility.Visible;
        }

        private void BtnApplyAddition_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSeriesToBeAdded.Text) && !string.IsNullOrEmpty(txtSeasonOfTheSeriesToAdd.Text))
            {
                bool hasSeries = false;
                foreach (string item in lstSeries.Items)
                {
                    if (item.Equals(txtSeriesToBeAdded.Text))
                    {
                        MessageBox.Show("This Series already exists!");
                        this.AddPanel.Visibility = Visibility.Hidden;
                        hasSeries = true;
                        break;
                    }
                }
                if (!hasSeries)
                {
                    lstSeries.Items.Add(txtSeriesToBeAdded.Text + "-Season " + txtSeasonOfTheSeriesToAdd.Text);
                    List<string> series = new List<string>();
                    foreach (string i in lstSeries.Items)
                    {
                        series.Add(i);
                    }
                    this.AddPanel.Visibility = Visibility.Hidden;
                    File.WriteAllLines("Series.csv", series);
                }
            }
            else
                MessageBox.Show("Please fill every information!");

        }

        #endregion

        #region Cancel button
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Hidden;
            AddPanel.Visibility = Visibility.Hidden;
            RemovePanel.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Remove Series
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            RemovePanel.Visibility = Visibility.Visible;
        }

        private void BtnRemoveSeries_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSeriesToBeRemoved.Text))
            {
                try
                {
                    foreach (string seriesName in lstSeries.Items)
                    {
                        if (seriesName.StartsWith(txtSeriesToBeRemoved.Text))
                        {
                            lstSeries.Items.Remove(seriesName);
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

                RemovePanel.Visibility = Visibility.Hidden;
                File.WriteAllLines("Series.csv", series);
            }
        }
        #endregion
    }
}
