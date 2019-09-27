using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InterviewQuestions
{
    class Program
    {

        public static Dictionary<string, string> Dictionary;

        static void Main(string[] args)
        {
            Console.WriteLine();
            Dictionary = new Dictionary<string, string>();

            var path = Directory.GetCurrentDirectory() + "\\Inputs\\words.txt";

            string[] lines = File.ReadAllLines(path);

            List<string> linesDesc = lines
                .ToList()
                .Where(line => lines.Contains(line))
                .OrderByDescending(line => line.Length)
                .ToList();

            linesDesc.ForEach(w =>
            {
                var cur = Dictionary;
                foreach (char c in w)
                {
                    cur.Add(c.ToString(), "");
                }
            });

            List<string> winners = new List<string>();

            foreach (string line in linesDesc)
            {
                if(IsComposedOfOtherWords(Dictionary, Dictionary, line))
                {
                    winners.Add(line);
                    if(winners.Count() == 2)
                    {
                        break;
                    }
                }
            }

            #region Initial Approach
            //List<string> linesAsc = lines
            //    .ToList()
            //    .Where(line => lines.Contains(line))
            //    .OrderBy(line => line.Length)
            //    .ToList();


            //// Start finding the biggest 2 words (asc -> desc)
            //// that can be built using other words, starting
            //// with the smallest words (desc -> asc)
            //foreach (string desc in linesDesc)
            //{
            //    string descLine = desc;

            //    foreach(string asc in linesAsc)
            //    {
            //        if(descLine.Length == 0)
            //        {
            //            winners.Add(desc);
            //            break;
            //        }

            //        int index;
            //        while((index = descLine.IndexOf(asc)) >= 0)
            //        {
            //            // Small line exists in big line
            //            if (index >= 0)
            //            {
            //                descLine = descLine.Remove(index, asc.Length);
            //            }
            //        }
            //    }

            //    if(winners.Count() == 2)
            //    {
            //        break;
            //    }
            //}

            #endregion

            //winners.ForEach(w => Console.WriteLine(w));
            Console.ReadKey();
        }

        public static bool IsComposedOfOtherWords(Dictionary<string, string> baseDict, Dictionary<string, string> curDict, string word, int level = 0)
        {
            string first = word[0].ToString();
            if(!curDict.TryGetValue(first, out string found))
            {
                return false;
            }

            if(word.Length == 1 && found == first)
            {
                return level > 0;
            }

            if (found == first && IsComposedOfOtherWords(baseDict, baseDict, word.Substring(1), level + 1))
                return true;


            return false;
        }
    }
}
