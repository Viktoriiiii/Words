using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Words
{
    public static class WordCounter
    {
        static Dictionary<string, int> wordsCount;


        /// <summary>
        /// Count the number of words in all files and output to json file/files
        /// </summary>
        /// <param name="flagOtherFiles">Flag to split output by files.
        /// true - separating output on files</param>
        /// <param name="flagForPreposition">Flag for preposition.
        /// true - count prepositions as words</param>
        public static void CountWords(bool flagOtherFiles, bool flagForPreposition)
        {
            Console.WriteLine("Программа для подсчета слов во всех файлах и вывов в json файл.");
            Console.WriteLine("Выполняется подсчет слов...");
            DirectoryInfo directoryInfo = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\Words");

            wordsCount = new Dictionary<string, int>();

            if (flagOtherFiles)
            {
                foreach (var file in directoryInfo.GetFiles())
                {
                    wordsCount.Clear();
                    string fileContent = File.ReadAllText(file.FullName);
                    wordsCount = flagForPreposition ?
                        GetDictionaryWithPreposition(fileContent) : GetDictionaryWithoutPreposition(fileContent);
                    WriteWordsInFile(wordsCount, file.Name);
                }
            }
            else
            {
                wordsCount.Clear();
                foreach (var file in directoryInfo.GetFiles())
                {
                    string fileContent = File.ReadAllText(file.FullName);
                    wordsCount = flagForPreposition ?
                        GetDictionaryWithPreposition(fileContent) : GetDictionaryWithoutPreposition(fileContent);
                }
                WriteWordsInFile(wordsCount, "result");
            }

            Console.WriteLine($"Подсчет слов завершен. Файл находится по адресу: {AppDomain.CurrentDomain.BaseDirectory}");
        }

        private static Dictionary<string, int> GetDictionaryWithPreposition(string fileContent)
        {
            string AbsentSignsInWords = "1234567890[](){}<>.,:;?!-*@#$%^&=+`~/|№'\"";
            StringBuilder sb = new StringBuilder(fileContent);
            foreach (char c in AbsentSignsInWords)
                sb = sb.Replace(c, ' ');
            sb = sb.Replace(Environment.NewLine, " ");
            string[] words = sb.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string w in words)
                if (wordsCount.TryGetValue(w, out int c))
                    wordsCount[w] = c + 1;
                else
                    wordsCount.Add(w, 1);
            wordsCount = wordsCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return wordsCount;
        }

        private static Dictionary<string, int> GetDictionaryWithoutPreposition(string fileContent)
        {
            string AbsentSignsInWords = "1234567890[](){}<>.,:;?!-*@#$%^&=+`~/|№'\"";
            StringBuilder sb = new StringBuilder(fileContent);
            foreach (char c in AbsentSignsInWords)
                sb = sb.Replace(c, ' ');
            sb = sb.Replace(Environment.NewLine, " ");

            string[] prepositions = { "без", "безо", "близ", "в", "во", "вместо", "вне", "для", "до", "за", "из", 
                "изо", "из-за", "из-под", "к", "ко", "кроме", "между", "меж", "на", "над", "о", "об", "обо", "от", 
                "ото", "перед", "передо", "пред", "пред", "пo", "под", "подо", "при", "про", "ради", "с", "со", 
                "сквозь", "среди", "у", "через", "чрез" };





            foreach (string c in prepositions)
                sb = sb.Replace(c, " ");
            sb = sb.Replace(Environment.NewLine, " ");

            string[] words = sb.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string w in words)
                if (wordsCount.TryGetValue(w, out int c))
                    wordsCount[w] = c + 1;
                else
                    wordsCount.Add(w, 1);
            wordsCount = wordsCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return wordsCount;
        }
        
        private static void WriteWordsInFile(Dictionary<string, int> dict, string nameFile)
        {
            string[] content = new string[dict.Count];
            int c = 0;
            foreach (var d in dict)
            {
                content[c] = d.Key + " " + d.Value;
                c++;
            }
            File.WriteAllLines("result-" + nameFile, content);
        }       

    }
}
