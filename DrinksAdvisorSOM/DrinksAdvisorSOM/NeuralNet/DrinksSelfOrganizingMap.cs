using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrinksAdvisorSOM.NeuralNet.FileIO;
using DrinksAdvisorSOM.Models;
using System.Drawing;

namespace DrinksAdvisorSOM.NeuralNet
{
    class DrinksSelfOrganizingMap : INeuralNet
    {
        private Node[] NeuralNet { get; set; }
        private DrinksContainer DrinksContainer { get; set; }
        private NeuralNetRenderer neuralNetRenderer;
        private int neuralMapWidth, neuralMapHeight;
        

        public DrinksSelfOrganizingMap()
        {
            DrinksContainer = LoadTrainingSet(@"C:\Users\Fin\Dropbox\Studia\Sieci neuronowe\Projekt\SOM\app\DrinksAdvisorSOM\DrinksAdvisorSOM\features.csv");
            neuralNetRenderer = new NeuralNetRenderer();
        }



        private DrinksContainer LoadTrainingSet(string filename)
        {
            DrinksReader reader = new DrinksReader();
            return reader.LoadDrinks(filename);
        }

        public void LoadNeuralNet(string filename)
        {
            NeuralNetReader reader = new NeuralNetReader();
            NeuralNet = reader.LoadNodes(filename).ToArray();
        }

        public void SaveNeuralNet(string filename)
        {
            NeuralNetWriter writer = new NeuralNetWriter();
            writer.SaveNodes(NeuralNet, filename, 2);
        }

        public DrinksContainer GetDrinksContainer()
        {
            return DrinksContainer;
        }


        public void LearnNeuralNet(int epochsCount, double initialLearningRate, float distanceBetweenNeurons, int neuralMapWidth, int neuralMapHeight)
        {
            this.neuralMapWidth = neuralMapWidth;
            this.neuralMapHeight = neuralMapHeight;
            NeuralNetTeacher learner = new NeuralNetTeacher(DrinksContainer.DrinksDictionary.Values.ToArray(), epochsCount, initialLearningRate, distanceBetweenNeurons, neuralMapWidth, neuralMapHeight);
            NeuralNet = learner.GetLearnedNeuralNet();
        }

        public Image GetRender()
        {
            return neuralNetRenderer.GetRender(NeuralNet, neuralMapWidth, neuralMapHeight);
        }
    }
}
