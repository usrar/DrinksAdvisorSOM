using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DrinksAdvisorSOM.NeuralNet.FileIO
{
    class NeuralNetReader
    {
        public IEnumerable<Node> LoadNodes(string filename)
        {
            List<Node> list_nodes = new List<Node>();
            XElement root = XElement.Load(filename);

            XElement xdimensionsCount = (from el in root.Elements("Meta")
                                         select el.Element("Dimensions")).First();

            int dimensionsCount = int.Parse(xdimensionsCount.Value.Trim());

            IEnumerable<XElement> xnodes = from el in root.Descendants("Data").Elements()
                                           select el;

            foreach (XElement xnode in xnodes)
            {
                var xDrinkID = (from el in xnode.Elements("DrinkID")
                                select el).First();

                int drinkID = int.Parse(xDrinkID.Value);

                var X = (from el in xnode.Elements("X")
                                    select el).First();
                
                var Y = (from el in xnode.Elements("Y")
                         select el).First();

                var xWeights = (from el in xnode.Elements("Weights")
                                select el).First(); ;

                string[] sWeights = xWeights.Value.Split(';');

                double[] weights = new double[sWeights.Length];


                for (int i = 0; i < sWeights.Length; i++)
                {
                    weights[i] = double.Parse(sWeights[i]);
                }

                list_nodes.Add(new Node(weights, float.Parse(X.Value), float.Parse(Y.Value), drinkID));
            }

            return list_nodes;

        }
    }
}
