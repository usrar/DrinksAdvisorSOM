using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrinksAdvisorSOM.Models;
using System.Threading;
using System.Diagnostics;
using DrinksAdvisorSOM.NeuralNet.Structure;

namespace DrinksAdvisorSOM.NeuralNet.Learning
{
    class NeuralNetTeacher
    {
        private int epochsCount,
                    neuralMapWidth,
                    neuralMapHeight,
                    featuresCount;

        private double initialLearningRate,
                       maxNeuronRestTimeInv;

        private static double _minNeuronPotential;

        private float distanceBetweenNeurons;
        private Dictionary<int,Drink> drinksDictionary;
        private Drink[] drinksArray;
        private Random randomizer;
        protected int processorsCount;

        protected static double[] potentialsArray;

        private const double RANDOM_WEIGHTS_SCALE_FACTOR = 1;


        public NeuralNetTeacher(Dictionary<int,Drink> drinksDictionary, int epochsCount, double initialLearningRate,
            float distanceBetweenNeurons, int neuralMapWidth, int neuralMapHeight, int featuresCount, double minNeuronPotential, int maxNeuronRestTime)
        {
            this.drinksDictionary = drinksDictionary;
            this.drinksArray = drinksDictionary.Values.ToArray();
            this.epochsCount = epochsCount;
            this.initialLearningRate = initialLearningRate;
            this.neuralMapWidth = neuralMapWidth;
            this.neuralMapHeight = neuralMapHeight;
            this.distanceBetweenNeurons = distanceBetweenNeurons;
            this.featuresCount = featuresCount;
            _minNeuronPotential = minNeuronPotential;
            this.maxNeuronRestTimeInv = 1 / (double)maxNeuronRestTime;

            randomizer = new Random();
            processorsCount = Environment.ProcessorCount;
        }
        
        public DrinksSelfOrganizingMap GetLearnedNeuralNet()
        {
            Node[] neuralNet = InitializeNodes(featuresCount);
            potentialsArray = InitializePotentialsArray(neuralNet.Length);

            List<Drink> trainingData = drinksArray.ToList();

            for (int i = 0; i < epochsCount; i++)
            {
                if (trainingData.Count == 0)
                    trainingData = drinksArray.ToList();

                int inputVectorIndex = randomizer.Next(trainingData.Count);

                neuralNet = Epoch(i, trainingData[inputVectorIndex], neuralNet);
                trainingData.RemoveAt(inputVectorIndex);

            }

            neuralNet = ComputeNearestDrinks(neuralNet);
            Tuple<double, double> vectorQuantizationError = GetVectorQuantizationError(neuralNet);
            return new DrinksSelfOrganizingMap(neuralNet, neuralMapWidth, neuralMapHeight, distanceBetweenNeurons, vectorQuantizationError.Item1, vectorQuantizationError.Item2);
        }


        private double[] InitializePotentialsArray(int size)
        {
            double[] potentialsArray = new double[size];
            for (int i = 0; i < size; i++)
            {
                potentialsArray[i] = _minNeuronPotential;
            }

            return potentialsArray;
        }

        private Node[] ComputeNearestDrinks(Node[] neuralNet)
        {
            int counter = 0;

            foreach (Node node in neuralNet)
            {
                int nearestDrinkID = GetNearestDrink(node.Weights);

                if (nearestDrinkID != node.DrinkID)
                {
                    node.DrinkID = nearestDrinkID;
                    counter++;
                }
            }
            return neuralNet;
        }

        private int GetNearestDrink(double[] nodeWeights)
        {
            int drinkID = 0;
            double bestDistance = double.MaxValue;

            for (int i = 0; i < drinksArray.Length; i++)
            {
                double tempDistance = nodeWeights.GetDistance(drinksArray[i].FeaturesArray);
                if (tempDistance < bestDistance)
                {
                    bestDistance = tempDistance;
                    drinkID = drinksArray[i].ID;
                }
            }

            return drinkID;
        }


        private Node[] InitializeNodes(Drink[] trainingDataBaseArray)
        {
            List<Drink> trainingData = trainingDataBaseArray.ToList();
            int nodesNumber = neuralMapWidth * neuralMapHeight;
            Node[] nodesArray = new Node[nodesNumber];
            
            
            for (int i = 0; i < nodesNumber; i++)
            {
                if( trainingData.Count == 0)
                    trainingData = trainingDataBaseArray.ToList();

			        int index = randomizer.Next(trainingData.Count);
                    Drink drink = trainingData[index];

                    nodesArray[i] = new Node(trainingData[index].FeaturesArray,(i % neuralMapWidth) * distanceBetweenNeurons, (i / neuralMapWidth) * distanceBetweenNeurons, drink.ID);
                    trainingData.RemoveAt(index);
			}

            return nodesArray; 
        }

        private Node[] InitializeNodes(int weightsCount)
        {
            int nodesNumber = neuralMapWidth * neuralMapHeight;
            Node[] nodesArray = new Node[nodesNumber];


            for (int i = 0; i < nodesNumber; i++)
            {
                nodesArray[i] = new Node(GetRandomWeights(weightsCount), (i % neuralMapWidth) * distanceBetweenNeurons, (i / neuralMapWidth) * distanceBetweenNeurons, -1);
            }

            return nodesArray;
        }

        private double[] GetRandomWeights(int weightsCount)
        {
            double[] weights = new double[weightsCount];

            for (int i = 0; i < weightsCount; i++)
            {
                weights[i] = randomizer.NextDouble() * RANDOM_WEIGHTS_SCALE_FACTOR;

            }

            return weights;
        }

        private Node[] Epoch(int t, Drink inputDrink, Node[] nodesArray)
        {
            BestMatchingNodeMultiThreadedSearcher bestMatchingNodeMultiThreadedSearcher = new BestMatchingNodeMultiThreadedSearcher(nodesArray, processorsCount, inputDrink.FeaturesArray);
            int winningNodeIndex = bestMatchingNodeMultiThreadedSearcher.GetBestMatchingNodeIndex();

            double neighbourhoodRadius = Radius(t);
            double learningRate = LearningRate(t);

            potentialsArray[winningNodeIndex] -= (_minNeuronPotential + maxNeuronRestTimeInv);
            UpdatePotentialsArray(maxNeuronRestTimeInv);
            
            System.Threading.Tasks.Parallel.ForEach(nodesArray, node =>
                {
                    double theta = Theta(t, nodesArray[winningNodeIndex], node, neighbourhoodRadius);

                    if (theta > 0)
                    {
                        node.AdjustWeights(theta, learningRate, inputDrink.FeaturesArray);
                        node.DrinkID = inputDrink.ID;
                    }  
                });
            

            return nodesArray;
        }


        private void UpdatePotentialsArray(double value)
        {
            for (int i = 0; i < potentialsArray.Length; i++)
            {
                potentialsArray[i] += value;
                if (potentialsArray[i] > 1)
                    potentialsArray[i] = 1;
            }
        }



        private class BestMatchingNodeMultiThreadedSearcher
        {
            private Tuple<int, double>[] bestMatchingNodes;
            private int processorsCount;

            public BestMatchingNodeMultiThreadedSearcher(Node[] neuralNet, int processorsCount, double[] inputVector)
            {
                this.processorsCount = processorsCount;
                bestMatchingNodes = new Tuple<int,double>[processorsCount];
                FindBestMatchingNodes(neuralNet, inputVector);
            }



            private void FindBestMatchingNodes(Node[] neuralNet, double[] inputVector)
            {
                List<Thread> list_threads = new List<Thread>();
                int chunkFactor = processorsCount;

                int chunkLength = ((neuralNet.Length % chunkFactor) == 0)
                         ? neuralNet.Length / chunkFactor
                         : ((int)((neuralNet.Length) / ((float)chunkFactor)) + 1);

                int remaining = neuralNet.Length;
                int currentRow = 0;
                int threadIndex = 0;

                while (remaining > 0)
                {
                    if (remaining < chunkLength)
                        chunkLength = remaining;

                    remaining -= chunkLength;

                    int t_currentRow = currentRow,
                        t_chunkLength = chunkLength,
                        t_boundaryRow = t_currentRow + t_chunkLength,
                        t_threadIndex = threadIndex;

                    Thread thread = new Thread(() => FindBestMatchingNodeThread(neuralNet, t_currentRow, t_boundaryRow, t_threadIndex, inputVector));
                    list_threads.Add(thread);
                    thread.Start();

                    currentRow += chunkLength;
                    threadIndex ++;
                }


                list_threads.ForEach(t => t.Join());
            }

            

            private void FindBestMatchingNodeThread(Node[] neuralNet, int startIndex, int endIndex, int threadIndex, double[] inputVector)
            {
                int bestMatchingNodeIndex = 0;
                double bestDistance = double.MaxValue;


                for(int i = startIndex; i < endIndex; i++)
                {
                    double distance = neuralNet[i].Weights.GetDistance(inputVector);
                    if (distance < bestDistance && potentialsArray[i] > _minNeuronPotential)
                    {
                        bestDistance = distance;
                        bestMatchingNodeIndex = i;
                    }
                }

                bestMatchingNodes[threadIndex] = new Tuple<int, double>(bestMatchingNodeIndex, bestDistance);
            }

            public int GetBestMatchingNodeIndex()
            {
                int bestMatchingNodeIndex = 0;
                double bestDistance = double.MaxValue;

                for (int i = 0; i < bestMatchingNodes.Length; i++)
                {
                    if (bestMatchingNodes[i].Item2 < bestDistance)
                    {
                        bestMatchingNodeIndex = bestMatchingNodes[i].Item1;
                        bestDistance = bestMatchingNodes[i].Item2;
                    }
                }

                return bestMatchingNodeIndex;
            }

        }
  

        private double Radius(int t)
        {
            double mapRadius = Math.Max(neuralMapWidth, neuralMapHeight) / 2,
                   timeConstant = epochsCount / Math.Log(mapRadius);

            return mapRadius * Math.Exp(-t / timeConstant);
        }

        private double LearningRate(int t)
        {
            return initialLearningRate * Math.Exp(-t / (double)epochsCount);
        }

        private double Theta(int t, Node BestMatchingNode, Node node, double radius)
        {
            double distSqr = (BestMatchingNode.X - node.X) * (BestMatchingNode.X - node.X)  + (BestMatchingNode.Y - node.Y) * (BestMatchingNode.Y - node.Y);

            if (distSqr > radius * radius)
                return 0;

            return Math.Exp(-distSqr / (2 * radius * radius));
        }


        // item1 - error average, item 2 - stddev
        private Tuple<double, double> GetVectorQuantizationError(Node[] neuralNet)
        {   
            double[] nodeErrorArray = new double[neuralNet.Length];
            for (int i = 0; i < nodeErrorArray.Length; i++)
            {
                nodeErrorArray[i] = neuralNet[i].Weights.GetSquareDistance(drinksDictionary[neuralNet[i].DrinkID].FeaturesArray);
            }

            return new Tuple<double,double>(nodeErrorArray.Average(), nodeErrorArray.StandardDeviation());
        }
    }
}
