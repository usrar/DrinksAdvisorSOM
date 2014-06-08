﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrinksAdvisorSOM.NeuralNet.FileIO;
using DrinksAdvisorSOM.Models;
using System.Drawing;
using DrinksAdvisorSOM.NeuralNet.Rendering;
using DrinksAdvisorSOM.NeuralNet.Structure;
using DrinksAdvisorSOM.NeuralNet.Learning;

namespace DrinksAdvisorSOM.NeuralNet
{
    class DrinksSelfOrganizingMapController : IDrinksSelfOrganizingMapController
    {
        
        private DrinksContainer DrinksContainer { get; set; }
        private NeuralNetRenderer neuralNetRenderer;
        private DrinksSelfOrganizingMap drinksMap;
        


        public DrinksSelfOrganizingMapController()
        {
            DrinksContainer = LoadTrainingSet(Properties.Resources.DrinksFeatures);
            neuralNetRenderer = new NeuralNetRenderer();
        }


        public void LoadNeuralNet(string filename)
        {
            NeuralNetReader reader = new NeuralNetReader();
            drinksMap = reader.LoadNeuralNet(filename); 
        }

        public void SaveNeuralNet(string filename)
        {
            EnsureDrinksMapIsNotNull();
            NeuralNetWriter writer = new NeuralNetWriter();
            writer.SaveNeuralNet(drinksMap, filename);
        }

        public DrinksContainer GetDrinksContainer()
        {
            return DrinksContainer;
        }

        public void LearnNeuralNet(int epochsCount, double initialLearningRate, float distanceBetweenNeurons, int neuralMapWidth, int neuralMapHeight)
        {
            NeuralNetTeacher learner = new NeuralNetTeacher(DrinksContainer.DrinksDictionary.Values.ToArray(), epochsCount, initialLearningRate, distanceBetweenNeurons, neuralMapWidth, neuralMapHeight);
            drinksMap = learner.GetLearnedNeuralNet();
        }

        public Image GetRender()
        {
            EnsureDrinksMapIsNotNull();
            return neuralNetRenderer.GetRender(drinksMap);
        }


        public IEnumerable<Drink> FindSimilarDrinks(int drinkID, int quantity)
        {
            List<int> similarDrinksID = new List<int>(quantity);

            int radius = 1;
            while (similarDrinksID.Count < quantity)
            {
                List<PointF> coordinationsList = drinksMap.GetCoordinatesListByDrinkID(drinkID);


                foreach (PointF pointF in coordinationsList)
                {
                    float offset = radius * drinksMap.DistanceBetweenNeurons,
                          startX = pointF.X - offset,
                          endX = pointF.X + offset,
                          startY = pointF.Y - offset,
                          endY = pointF.Y + offset;

                    for (float i = startX; i <= endX; i += drinksMap.DistanceBetweenNeurons)
                    {
                        int? sample1 = drinksMap.GetDrinkIDByCoordinates(new PointF(i, startY)),
                             sample2 = drinksMap.GetDrinkIDByCoordinates(new PointF(i, endY));

                        if (sample1.HasValue &&  sample1.Value != drinkID && !similarDrinksID.Contains(sample1.Value))
                        {
                            similarDrinksID.Add(sample1.Value);
                        }
                        if (sample2.HasValue && sample2.Value != drinkID && !similarDrinksID.Contains(sample2.Value))
                        {
                            similarDrinksID.Add(sample2.Value);
                        }
                    }

                    for (float i = startY; i < endY; i += drinksMap.DistanceBetweenNeurons)
                    {
                        int? sample1 = drinksMap.GetDrinkIDByCoordinates(new PointF(startX, i)),
                             sample2 = drinksMap.GetDrinkIDByCoordinates(new PointF(endX, i));

                        if (sample1.HasValue && sample1.Value != drinkID && !similarDrinksID.Contains(sample1.Value))
                        {
                            similarDrinksID.Add(sample1.Value);
                        }
                        if (sample2.HasValue && sample2.Value != drinkID && !similarDrinksID.Contains(sample2.Value))
                        {
                            similarDrinksID.Add(sample2.Value);
                        }
                    }
                }

                radius++;
            }

            List<Drink> similarDrinks = new List<Drink>();
            similarDrinksID.ForEach(id => similarDrinks.Add(DrinksContainer.DrinksDictionary[id]));

            return similarDrinks.Take(quantity);
        }


        private DrinksContainer LoadTrainingSet(string filename)
        {
            DrinksReader reader = new DrinksReader();
            return reader.LoadDrinks(filename);
        }

        

        private void EnsureDrinksMapIsNotNull()
        {
            if (drinksMap == null)
                throw new Exception("There's no drinks map.");
        }
    }
}
