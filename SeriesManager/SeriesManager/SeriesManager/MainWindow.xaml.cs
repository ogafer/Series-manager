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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"~\SeriesManager\SeriesManager";
            var result = dialog.ShowDialog();

            var item = File.ReadAllLines(dialog.FileName);
            string[] aux = new string[100];
            List<string> data = new List<string>();
            foreach (var i in item)
            {
                aux = i.Split(';');
                for (int j = 0; j < aux.Length; j++)
                {
                    data.Add(aux[j]);
                }
            }
            for (int z = 0; z < data.Count; z++)
            {
                lstSeries.Items.Add(data[z].ToString());
            }
        }
    }
}
