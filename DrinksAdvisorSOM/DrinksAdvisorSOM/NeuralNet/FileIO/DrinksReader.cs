using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DrinksAdvisorSOM.Models;

namespace DrinksAdvisorSOM.NeuralNet.FileIO
{
    class DrinksReader
    {
        private const int COLUMNS_TO_SKIP = 3;

        public DrinksContainer LoadDrinks(string source)
        {
            using (StringReader stringReader = new StringReader(source))
            {
                List<Drink> trainingSet = new List<Drink>();

                string line = stringReader.ReadLine();
                string[] columnNames = line.Split(';').Skip(COLUMNS_TO_SKIP).ToArray();


                while ((line = stringReader.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    int id = int.Parse(values[0]);
                    string drinkName = values[1],
                           url = values[2];

                    double[] trainingRow = new double[values.Length - COLUMNS_TO_SKIP];

                    for (int i = 0; i < trainingRow.Length - 1; i++)
                    {
                        trainingRow[i] = double.Parse(values[i + COLUMNS_TO_SKIP], System.Globalization.CultureInfo.InvariantCulture);
                    }

                    trainingSet.Add(new Drink(id, drinkName, url, trainingRow));
                }

                return new DrinksContainer(trainingSet.ToDictionary(r => r.ID, r => r), columnNames);
            }
            
        }
    }
}
