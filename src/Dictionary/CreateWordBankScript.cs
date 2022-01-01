using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class CreateWordBankScript
    {

        public static List<string> Run(string dictionary, Func<string, bool> wordIsValid)
        {
            string[] array = File.ReadAllLines("data/" + dictionary + ".txt");
            List<string> list = new List<string>();
            foreach (string str in array)
            {
                string word = str.ToUpper();
                if (wordIsValid(word))
                    list.Add(word);
            }
            return list;
        }

        public static List<string> Run(string dictionary)
        {
            return Run(dictionary, AlwaysTrue);
        }

        private static bool AlwaysTrue(string str)
        {
            return true;
        }
    }
}
