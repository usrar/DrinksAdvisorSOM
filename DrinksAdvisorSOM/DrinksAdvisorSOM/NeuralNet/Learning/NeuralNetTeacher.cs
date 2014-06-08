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
                    neuralMapHeight;
        private double initialLearningRate;
        private float distanceBetweenNeurons;
        private Drink[] drinksArray;
        private Random randomizer;
        protected int processorsCount;

        private const double RANDOM_WEIGHTS_SCALE_FACTOR = 1;


        public NeuralNetTeacher(Drink[] drinksArray, int epochsCount, double initialLearningRate,
            float distanceBetweenNeurons, int neuralMapWidth, int neuralMapHeight)
        {
            this.drinksArray = drinksArray;
            this.epochsCount = epochsCount;
            this.initialLearningRate = initialLearningRate;
            this.neuralMapWidth = neuralMapWidth;
            this.neuralMapHeight = neuralMapHeight;
            this.distanceBetweenNeurons = distanceBetweenNeurons;

            randomizer = new Random();
            processorsCount = Environment.ProcessorCount;
        }
        
        public DrinksSelfOrganizingMap GetLearnedNeuralNet()
        {
            //Node[] neuralNet = InitializeNodes(drinksArray);
            Node[] neuralNet = InitializeNodes(drinksArray[0].FeaturesArray.Length);
            List<Drink> trainingData = drinksArray.ToList();

            for (int i = 0; i < epochsCount; i++)
            {
                if (trainingData.Count == 0)
                    trainingData = drinksArray.ToList();

                int inputVectorIndex = randomizer.Next(trainingData.Count);

                neuralNet = Epoch(i, trainingData[inputVectorIndex], neuralNet);
                trainingData.RemoveAt(inputVectorIndex);

            }

            return new DrinksSelfOrganizingMap(neuralNet, neuralMapWidth, neuralMapHeight, distanceBetweenNeurons);

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
            Node winningNode = bestMatchingNodeMultiThreadedSearcher.GetBestMatchingNode();

            double neighbourhoodRadius = Radius(t);
            double learningRate = LearningRate(t);


            
            System.Threading.Tasks.Parallel.ForEach(nodesArray, node =>
                {
                    double theta = Theta(t, winningNode, node, neighbourhoodRadius);

                    if (theta > 0)
                    {
                        node.AdjustWeights(theta, learningRate, inputDrink.FeaturesArray);
                        node.DrinkID = inputDrink.ID;
                    }  
                });
            

            return nodesArray;
        }



        private class BestMatchingNodeMultiThreadedSearcher
        {
            private Tuple<Node, double>[] bestMatchingNodes;
            private int processorsCount;

            public BestMatchingNodeMultiThreadedSearcher(Node[] neuralNet, int processorsCount, double[] inputVector)
            {
                this.processorsCount = processorsCount;
                bestMatchingNodes = new Tuple<Node,double>[processorsCount];
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
                Node bestMatchingNode = null;
                double bestDistance = double.MaxValue;


                for(int i = startIndex; i < endIndex; i++)
                {
                    double distance = GetDistance(neuralNet[i].Weights, inputVector);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestMatchingNode = neuralNet[i];
                    }
                }

                bestMatchingNodes[threadIndex] = new Tuple<Node, double>(bestMatchingNode, bestDistance);
            }

            private double GetDistance(double[] weights, double[] inputVector)
            {
                double distance = 0;

                for (int i = 0; i < weights.Length; i++)
                {
                    distance += (inputVector[i] - weights[i]) * (inputVector[i] - weights[i]);
                }

                return Math.Sqrt(distance);
            }


            public Node GetBestMatchingNode()
            {
                Node bestMatchingNode = null;
                double bestDistance = double.MaxValue;

                for (int i = 0; i < bestMatchingNodes.Length; i++)
                {
                    if (bestMatchingNodes[i].Item2 < bestDistance)
                    {
                        bestMatchingNode = bestMatchingNodes[i].Item1;
                        bestDistance = bestMatchingNodes[i].Item2;
                    }
                }

                return bestMatchingNode;
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
    }
}
