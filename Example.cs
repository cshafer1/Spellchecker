using System;
using System.Runtime.InteropServices;

namespace SpellChecker
{
    class Example
    {
        static void Main(string[] args)
        {
            string[] words = {"test", "dark", "programming"};
            string query = "brogming";

            Console.WriteLine(SpellChecker.SpellCheck(query, words));
        }
    }
}