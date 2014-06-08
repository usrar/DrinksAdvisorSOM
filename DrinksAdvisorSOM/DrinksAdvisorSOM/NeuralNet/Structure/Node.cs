using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrinksAdvisorSOM.NeuralNet.Structure
{
    class Node
    {
        public int DrinkID { get; set; }
        public double[] Weights { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }

        public Node(double[] weights, float x, float y, int drinkID)
        {
            DrinkID = drinkID;
            Weights = weights;
            X = x;
            Y = y;
        }

        public void AdjustWeights(double theta, double learningRate, double[] inputVector)
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] += theta * learningRate * (inputVector[i] - Weights[i]);

                if (Weights[i] > 1)
                    Weights[i] = 1;

                if (Weights[i] < 0)
                    Weights[i] = 0;

            }
        }
    }
}
