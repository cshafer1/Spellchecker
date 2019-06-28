using System;
using System.Linq;

namespace SpellChecker
{
    /*
     * This class implements the Lipschitz Embeddings Algorithm to return the closest matching string to the query from
     * a provided dictionary
     *
     * Read more about finding similar string matches from the following link
     * https://pdfs.semanticscholar.org/570d/c4934c770cc8cee2571c8b1527971156ad64.pdf
     */
    public class SpellChecker
    {
        public static string SpellCheck(string query, string[] dictionary)
        {
            int length = dictionary.Length;
            int[] editDistancesQuery = new int[length];
            int[] editDistancesReference = new int[length];
            string[] shuffledDictionary = dictionary;
            int maxIndex = 0;
            int maxValue = int.MinValue;
            Shuffle(ref shuffledDictionary);
            for (int i = 0; i < length; i++)
            {
                editDistancesQuery[i] = MinimumEditDistance.calculate(query, shuffledDictionary[i]);
            }
            for (int i = 0; i < length; i++)
            {
                editDistancesReference[i] = MinimumEditDistance.calculate(dictionary[i], shuffledDictionary[i]);
                if (editDistancesReference[i] <= maxValue) continue;
                maxValue = editDistancesReference[i];
                maxIndex = i;
            }

            double[] embeddingSpaceDistance = new double[length];
            double p = 1.0 / length;
            double result = 0;
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    int differenceEdit = Math.Abs(MinimumEditDistance.calculate(dictionary[j], shuffledDictionary[i]) -
                                                  editDistancesQuery[i]);
                    result += Math.Pow(Math.Pow(differenceEdit / Math.Pow(length, 1 / p), p), 1 / p);
                }

                embeddingSpaceDistance[j] = result;
            }
            
            var dictQueueEditDistance = MinimumEditDistance.calculate(dictionary[maxIndex], query);
            int editDistance = -1;
            string nearest = "";

            for (int i = 0; i < length; i++)
            {
                int editDistanceLocal = MinimumEditDistance.calculate(dictionary[i], query);
                if (embeddingSpaceDistance[i] > dictQueueEditDistance)
                {
                    editDistance = dictQueueEditDistance;
                    nearest = dictionary[maxIndex];
                } else if (editDistanceLocal < dictQueueEditDistance)
                {
                    editDistance = editDistanceLocal;
                    nearest = dictionary[i];
                }
            }
            return nearest;
        }
        
         
        private static void Shuffle(ref string[] words)
        {
            Random r = new Random();
            int length = words.Length;
            for (int i = length - 1; i > 0; i--)
            {
                int j = r.Next(0, i + 1);
                string temp = words[i];
                words[i] = words[j];
                words[j] = temp;
            }
        }
    }
}