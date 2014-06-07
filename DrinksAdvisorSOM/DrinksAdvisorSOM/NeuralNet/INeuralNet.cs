using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DrinksAdvisorSOM.Models;

namespace DrinksAdvisorSOM.NeuralNet
{
    interface INeuralNet
    {
        void LearnNeuralNet(int epochsCount, double initialLearningRate, float distanceBetweenNeurons, int neuralMapWidth, int neuralNetHeight);
        void SaveNeuralNet(string filename);
        void LoadNeuralNet(string filename);
        Image GetRender();
        DrinksContainer GetDrinksContainer();
    }
}