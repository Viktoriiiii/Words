using System;

namespace Words
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WordCounter.CountWords(false, true);
            Console.ReadKey();
        }        
    }
}
