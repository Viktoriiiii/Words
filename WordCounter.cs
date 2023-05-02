using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words
{
    public static class WordCounter
    {   
        /// <summary>
        /// Count the number of words in all files and output to json file/files
        /// </summary>
        /// <param name="flagOtherFiles">Flag to split output by files.
        /// true - separating output on files</param>
        /// <param name="flagForPreposition">Flag for preposition.
        /// true - doesn't count prepositions as words</param>
        public static void CountWords(bool flagOtherFiles, bool flagForPreposition)
        {
            Console.WriteLine("Программа для подсчета слов во всех файлах и вывов в json файл.");
            Console.WriteLine("Выполняется подсчет слов...");
            DirectoryInfo directoryInfo = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\Words");
            
            
            Dictionary<string, int> wordsCount = flagForPreposition ? 
                CountWithoutPreposition() : CountWithPreposition();

            if (flagOtherFiles)
            {
                foreach (var file in directoryInfo.GetFiles())
                {                    
                    string fileContent = File.ReadAllText(file.FullName);
                    WriteWordsInFile(fileContent);
                }
            }
            else
            {




            }















            Console.WriteLine($"Подсчет слов завершен. Файл находится по адресу: {AppDomain.CurrentDomain.BaseDirectory}");
        }

        private static Dictionary<string, int> CountWithPreposition()
        {

            return new Dictionary<string, int>();
        }
        
        private static Dictionary<string, int> CountWithoutPreposition()
        {

            return new Dictionary<string, int>();
        }

        private static void WriteWordsInFile(string fileContent)
        {
            Console.WriteLine(fileContent);
        }       

    }
}
