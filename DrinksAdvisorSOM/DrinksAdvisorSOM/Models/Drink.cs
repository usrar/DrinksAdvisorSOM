using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrinksAdvisorSOM.Models
{
    class Drink
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string ImageUrl { get; private set; }

        public double[] FeaturesArray { get; private set; }

        public Drink(int id, string name, string url, double[] featuresArray, string imageUrl)
        {
            ID = id;
            Name = name;
            Url = url;
            FeaturesArray = featuresArray;
            ImageUrl = imageUrl;
        }
    }
}
