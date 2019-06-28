using System;

namespace SpellChecker
{
    /*
     * This class uses dynamic programming to calculate the edit distance between two words.
     */
    public class MinimumEditDistance
    {
        public static int calculate(string word1, string word2)
        {
            word1 = word1.ToLower();
            word2 = word2.ToLower();
            int[,] distance = new int[word1.Length+1, word2.Length+1];
            for (int i = 1; i <= word1.Length; i++)
                distance[i,0] = i;

            for (int i = 1; i <= word2.Length; i++)
                distance[0, i] = i;
                

            for (int i = 1; i <= word1.Length; i++)
            {
                for (int j = 1; j <= word2.Length; j++)
                {
                    int delta = word1[i-1] != word2[j-1] ? 1 : 0;
                    distance[i, j] = Math.Min(distance[i - 1, j - 1] + delta, Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1));
                }
            }
            return distance[word1.Length, word2.Length];
        }
    }
}