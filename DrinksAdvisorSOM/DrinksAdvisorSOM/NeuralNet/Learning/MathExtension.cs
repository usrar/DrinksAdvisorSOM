using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrinksAdvisorSOM.NeuralNet.Learning
{
    public static class MathExtension
    {

        public static double GetDistance(this double[] weights, double[] inputVector)
        {
            double distance = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                distance += (inputVector[i] - weights[i]) * (inputVector[i] - weights[i]);
            }

            return Math.Sqrt(distance);
        }

        public static double GetSquareDistance(this double[] weights, double[] inputVector)
        {
            double distance = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                distance += (inputVector[i] - weights[i]) * (inputVector[i] - weights[i]);
            }

            return distance;
        }


        public static double StandardDeviation(this IEnumerable<double> values)
        {
            double result = 0;
            int count = values.Count();
            if (count > 1)
            {
                //Compute the Average
                double avg = values.Average();

                //Perform the Sum of (value-avg)^2
                double sum = values.Sum(d => (d - avg) * (d - avg));

                //Put it all together
                result = Math.Sqrt(sum / count);
            }
            return result;
        }
    }
}
