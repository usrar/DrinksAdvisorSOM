using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrinksAdvisorSOM.Models
{
    class DrinksContainer
    {
        // key - drink id
        public Dictionary<int, Drink> DrinksDictionary { get; private set; }
        public string[] ColumnNames { get; private set; }

        public DrinksContainer(Dictionary<int, Drink> drinksDictionary, string[] columnNames)
        {
            DrinksDictionary = drinksDictionary;
            ColumnNames = columnNames;
        }


    }
}
