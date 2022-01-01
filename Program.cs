using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> wordBank = CreateWordBankScript.Run("1-1000", WordIsValid);
            Executioner executioner = new Executioner(wordBank);
            StdIOHandler handler = new StdIOHandler(executioner);
            handler.Play();
        }

        static bool WordIsValid(string word)
        {
            return (word.Length >= 6);
        }
    }
}
