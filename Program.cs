﻿using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> wordBank = CreateWordBankScript.Run("words_alpha", WordIsValid);
            Executioner executioner = new Executioner(wordBank);
            AutoGuesser guesser = new AutoGuesser(executioner, wordBank);
            guesser.Play();
        }

        static bool WordIsValid(string word)
        {
            return (word.Length >= 6);
        }
    }
}
