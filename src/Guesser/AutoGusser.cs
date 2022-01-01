using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class AutoGuesser : Guesser
    {
        private List<string> mWordBank;
        public AutoGuesser(Executioner executioner, List<string> wordBank) : base(executioner)
        {
            mWordBank = wordBank;
        }

        protected override char GuessCharacter(GameState state)
        {
            FilterWordBank(state);
            Console.WriteLine("Number of matches: " + mWordBank.Count);
            return MostCommonChar(state, mWordBank);
        }

        private void FilterWordBank(GameState state)
        {
            List<string> wordBank = new List<string>();
            foreach (string word in mWordBank)
            {
                if (IsWordPossible(state, word))
                {
                    wordBank.Add(word);
                }
            }
            mWordBank.Clear();
            mWordBank = wordBank;
        }

        private static bool IsWordPossible(GameState state, string word)
        {
            string censoredWord = state.CensoredWord;
            // check word length
            if (censoredWord.Length != word.Length)
                return false;
            
            // loop through characters
            for (char c = 'A'; c <= 'Z'; c++)
            {
                // if the character is incorrect but exists in the word, it's not valid
                if (state.IncorrectlyGuessed.Contains(c) && word.Contains(c))
                    return false;
            }

            for (int i=0; i<censoredWord.Length; i++)
            {
                // if the censored char is revealed and it does not equal the word char, it's not valid
                if (censoredWord[i] != '_' && censoredWord[i] != word[i])
                    return false;

                // if the word char has been correctly guessed, but the censored char has not been revealed, it's not valid
                if (censoredWord[i] == '_' && state.CorrectlyGuessed.Contains(word[i]))
                    return false; 
            }
            return true;
        }

        private static char MostCommonChar(GameState state, List<string> wordBank)
        {
            char mostCommon = '\0';
            int maxNumOccurences = 0;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (!state.IncorrectlyGuessed.Contains(c) && !state.CorrectlyGuessed.Contains(c))
                {
                    // calculate occurences
                    int occurences = 0;
                    foreach (string word in wordBank)
                    {
                        if (word.Contains(c))
                            ++occurences;
                    }
                    
                    // check if it is the maximum
                    if (occurences >= maxNumOccurences)
                    {
                        mostCommon = c;
                        maxNumOccurences = occurences;
                    }
                }
            }
            return mostCommon;
        }
    }
}
