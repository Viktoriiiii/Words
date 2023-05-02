using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Words
{
    internal class Program
    {
        static void Main(string[] args)
        {

            WordCounter.CountWords(true, true);
            Console.ReadKey();
        }        
    }
}
