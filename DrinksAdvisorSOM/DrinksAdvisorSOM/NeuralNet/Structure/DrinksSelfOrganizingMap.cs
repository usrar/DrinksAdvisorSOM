using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrinksAdvisorSOM.NeuralNet.Structure
{
    class DrinksSelfOrganizingMap
    {
        public Node[] NeuralNet { get; private set; }
        public int NeuralMapWidth { get; private set; }
        public int NeuralMapHeight { get; private set; }
        public float DistanceBetweenNeurons { get; private set; }


        
        private Dictionary<int, List<PointF>> nodesByDrinkIdDictionary;
        private Dictionary<PointF, int> nodesByCoordinatesDictionary;

        public DrinksSelfOrganizingMap(Node[] neuralNet, int neuralMapWidth, int neuralMapHeight, float distanceBetweenNeurons)
        {
            NeuralNet = neuralNet;
            NeuralMapWidth = neuralMapWidth;
            NeuralMapHeight = neuralMapHeight;
            DistanceBetweenNeurons = distanceBetweenNeurons;

            nodesByDrinkIdDictionary = ComputeNodesByDrinkIdDictionary(neuralNet);
            nodesByCoordinatesDictionary = ComputeNodesByCoordinatesDictionary(neuralNet);
        }


        public List<PointF> GetCoordinatesListByDrinkID(int id)
        {
            if (nodesByDrinkIdDictionary.ContainsKey(id))
            {
                return nodesByDrinkIdDictionary[id];
            }
            else
            {
                return null;
            }
        }

        public int? GetDrinkIDByCoordinates(PointF pointF)
        {
            if (nodesByCoordinatesDictionary.ContainsKey(pointF))
            {
                return nodesByCoordinatesDictionary[pointF];
            }
            else
            {
                return null;
            }  
        }

        
        private Dictionary<int, List<PointF>> ComputeNodesByDrinkIdDictionary(Node[] nodesArray)
        {
            Dictionary<int, List<PointF>> result = new Dictionary<int, List<PointF>>();

            foreach (Node node in nodesArray)
            {
                if (!result.ContainsKey(node.DrinkID))
                    result.Add(node.DrinkID, new List<PointF>());

                result[node.DrinkID].Add(new PointF(node.X, node.Y));
            }

            return result;
        }

        private Dictionary<PointF, int> ComputeNodesByCoordinatesDictionary(Node[] nodesArray)
        {
            Dictionary<PointF, int> result = new Dictionary<PointF, int>();

            foreach (Node node in nodesArray)
            {
                result.Add(new PointF(node.X, node.Y), node.DrinkID);
            }

            return result;
        }

    }
}
