using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Words
{
    public static class WordCounter
    {
        private static Dictionary<string, int> wordsCount;

        private static string[] prepositions = {"без","безо","близ","в","во","вместо","вне","для","до","за","из",
                       "изо","из-за","из-под","к","ко","кроме","между","меж","на","над","о","об","обо","от",
                       "ото","перед","передо","пред","пред","пo","под","подо","при","про","ради","с","со",
                       "сквозь","среди","у","через","чрез","aboard","about","above","absent","across","afore",
                       "after","against","along","amid","amidst","among","amongst","around","as","aside","aslant",
                       "astride","at","athwart","atop","before","behind","below","beneath","beside","besides",
                       "between","betwixt","beyond","but","by","circa","despite","down","except","for","from",
                       "given","in","inside","into","like","mid","minus","near","neath","next","of","on",
                       "opposite","out","outside","over","per","plus","pro","round","since","than","through",
                       "till","to","toward","towards","under","underneath","unlike","until","up","via","with",
                       "without"};

        private static string AbsentSignsInWords = "1234567890[](){}♦•<>«».,:;?!-_*@#$%^&=+`~/|№'\"\\";

        /// <summary>
        /// Count the number of words in all files and output to json file/files
        /// </summary>
        /// <param name="flagOtherFiles">Flag to split output by files.
        /// true - separating output on files</param>
        /// <param name="flagForPreposition">Flag for preposition.
        /// true - count prepositions as words</param>
        public static void CountWords(bool flagOtherFiles, bool flagForPreposition)
        {
            Console.WriteLine("Программа для подсчета слов во всех файлах и вывода в json файл.");
            Console.WriteLine("Выполняется подсчет слов...");
            DirectoryInfo directoryInfo = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\Words");

            wordsCount = new Dictionary<string, int>();

            FileInfo[] files = directoryInfo.GetFiles("*.txt", SearchOption.AllDirectories);
            int count = 1;
            foreach (var file in files)
            {
                if (flagOtherFiles)
                    wordsCount.Clear();
                string fileContent = File.ReadAllText(file.FullName);
                WriteInDictionary(fileContent, flagForPreposition, flagOtherFiles);
                if (flagOtherFiles)
                {
                    var filesWithWords = new Dictionary<string, Dictionary<string, int>>
                    {
                        { file.Name, wordsCount }
                    };
                    WriteWordsInFile(filesWithWords, $"result-{count}-{file.Name}");
                    count++;
                }
            }            

            if (!flagOtherFiles)                
                WriteWordsInFile(wordsCount, "result.txt");

            Console.WriteLine($"Подсчет слов завершен. Расположение вывода: {AppDomain.CurrentDomain.BaseDirectory}");
        }

        /// <summary>
        /// Getting a dictionary indicating the number of words used in the text
        /// </summary>
        /// <param name="fileContent">Text to parse</param>
        /// <param name="flagForPreposition">Flag for preposition.
        /// true - count prepositions as words</param>
        /// <param name="flagOtherFiles">Flag for preposition.
        /// true - count prepositions as words</param>
        private static void WriteInDictionary(string fileContent, bool flagForPreposition, bool flagOtherFiles)
        {
            StringBuilder sb = new StringBuilder(fileContent);
            foreach (char c in AbsentSignsInWords)
                sb = sb.Replace(c, ' ');
            sb = sb.Replace(Environment.NewLine, " ");            

            if (flagOtherFiles)
                wordsCount = new Dictionary<string, int>();

            string[] words = sb.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string w in words)
            {
                if (!flagForPreposition)
                {                    
                    if (prepositions.Contains(w) || prepositions.Contains(w.ToLower()))
                        continue;
                }
                if (wordsCount.TryGetValue(w, out int c))
                    wordsCount[w] = c + 1;
                else
                    wordsCount.Add(w, 1);
            }                
            wordsCount = wordsCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Write dictianary to file with format json
        /// </summary>
        /// <param name="dict">Dictianary</param>
        /// <param name="nameFile">File name</param>
        private static void WriteWordsInFile(Dictionary<string, int> dict, string nameFile)
        {
            File.WriteAllText(nameFile, JsonConvert.SerializeObject(dict));
        }

        /// <summary>
        /// Write dictianary to file with format json
        /// </summary>
        /// <param name="dict">Dictianary</param>
        /// <param name="nameFile">File name</param>
        private static void WriteWordsInFile(Dictionary<string, Dictionary<string, int>> dict, string nameFile)
        {
            File.WriteAllText(nameFile, JsonConvert.SerializeObject(dict));
        }
    }
}
