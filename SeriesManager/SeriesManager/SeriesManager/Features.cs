using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesManager
{
    class Features
    {
        #region Methods

        #region Load CSV Into List
        /// <summary>
        /// Method that will load the csv file with the series information
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<string> LoadList(string fileName)
        {
            List<string> listOfSeries = new List<string>();
            try
            {
                List<string> data = new List<string>();
                var item = File.ReadAllLines(fileName);
                string[] aux = new string[100];
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
                    listOfSeries.Add(data[z].ToString());
                }
                return listOfSeries;
            }
            catch
            {
                return listOfSeries;
            }
        }
        #endregion

        #endregion
    }
}
