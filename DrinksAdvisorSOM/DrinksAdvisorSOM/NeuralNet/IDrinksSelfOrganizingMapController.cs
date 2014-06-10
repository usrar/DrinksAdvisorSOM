using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DrinksAdvisorSOM.Models;

namespace DrinksAdvisorSOM.NeuralNet
{
    interface IDrinksSelfOrganizingMapController
    {
        void LearnNeuralNet(int epochsCount, double initialLearningRate, float distanceBetweenNeurons, int neuralMapWidth,
            int neuralNetHeight, double minNeuronPotential, int maxNeuronRestTime);
        void SaveNeuralNet(string filename);
        void LoadNeuralNet(string filename);
        Image GetRender();
        DrinksContainer GetDrinksContainer();
        IEnumerable<Drink> FindSimilarDrinks(int drinkID, int quantity);
        Tuple<double, double> GetVectorQuantizationError();
    }
}