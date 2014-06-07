using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrinksAdvisorSOM.Models
{
    class Drink
    {
        public int ID { get; private set; }
        public string Name { get; set; }
        public string Url { get; set; }
        
        public double[] FeaturesArray { get; private set; }

        public Drink(int id, string name, string url, double[] featuresArray)
        {
            ID = id;
            Name = name;
            Url = url;
            FeaturesArray = featuresArray;
        }
    }
}
