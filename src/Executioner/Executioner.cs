using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class Executioner
    {
        public const int DEFAULT_GUESS_LIMIT = 8;
        private int mGuessesLeft;
        private string mWord;
        private GameStatus mPrevGameStatus = GameStatus.Playing;
        private CharState[] mCharStates = new CharState[26];

        public Executioner(List<string> wordBank, int guessLimit = DEFAULT_GUESS_LIMIT) {
            mGuessesLeft = guessLimit;
            Random r = new Random();
            int index = r.Next(wordBank.Count);
            mWord = wordBank[index].ToUpper();
        }

        public GameState GetGameState()
        {
            string censoredWord = GetCensoredWord();
            List<char>[] guessCharLists = GetGuessedCharLists();
            return new GameState(mPrevGameStatus, GuessResponse.None, mGuessesLeft, censoredWord, guessCharLists[0], guessCharLists[1]);
        }

        public GameState Guess(char c) {
            if (mPrevGameStatus == GameStatus.Playing)
            {
                GuessResponse guessResponse = ProcessGuess(c);
                if (guessResponse == GuessResponse.Incorrect)
                    --mGuessesLeft;
                List<char>[] guessCharLists = GetGuessedCharLists();
                string censoredWord = GetCensoredWord();
                GameStatus gameStatus = GetGameStatus(censoredWord);
                if (gameStatus != GameStatus.Playing)
                    censoredWord = mWord;
                mPrevGameStatus = gameStatus;
                return new GameState(gameStatus, guessResponse, mGuessesLeft, censoredWord, guessCharLists[0], guessCharLists[1]);
            }
            else
            {
                string censoredWord = GetCensoredWord();
                List<char>[] guessCharLists = GetGuessedCharLists();
                return new GameState(mPrevGameStatus, GuessResponse.Forbiden, mGuessesLeft, censoredWord, guessCharLists[0], guessCharLists[1]);
            }
        }

        private GuessResponse ProcessGuess(char c) {
            byte b = GetCharIndex(c);
            if (b == 0xff)
                return GuessResponse.Invalid;
            switch (mCharStates[b])
            {
                case CharState.CorrectlyGuessed:
                case CharState.IncorrectlyGuessed:
                    return GuessResponse.AlreadyGuessed;
                default:
                    // if char exists in word
                    if (mWord.IndexOf((char)(b + 'A')) != -1)
                    {
                        mCharStates[b] = CharState.CorrectlyGuessed;
                        return GuessResponse.Correct;
                    }
                    else
                    {
                        mCharStates[b] = CharState.IncorrectlyGuessed;
                        return GuessResponse.Incorrect;
                    }
            }
        }

        /// <summary /> Get array of lists of chars in the form List<char>[] { correctlyGuessed, incorrectlyGuessed }
        private List<char>[] GetGuessedCharLists()
        {
            List<char> correctlyGuessed = new List<char>();
            List<char> incorrectlyGuessed = new List<char>();
            for (int i=0; i<mCharStates.Length; i++)
            {
                char c = (char)(i + 'A');
                switch (mCharStates[i])
                {
                    case CharState.IncorrectlyGuessed:
                        incorrectlyGuessed.Add(c);
                        break;
                    case CharState.CorrectlyGuessed:
                        correctlyGuessed.Add(c);
                        break;
                }
            }
            return new List<char>[] { incorrectlyGuessed, correctlyGuessed };
        }

        private string GetCensoredWord()
        {
            StringBuilder bldr = new StringBuilder(mWord.Length);
            foreach (char c in mWord)
            {
                CharState charState = mCharStates[GetCharIndex(c)];
                if (charState == CharState.CorrectlyGuessed)
                    bldr.Append(c);
                else
                    bldr.Append('_');
            }
            return bldr.ToString();
        }

        private GameStatus GetGameStatus(string censoredWord)
        {
            if (mGuessesLeft <= 0)
                return GameStatus.Lost;
            if (censoredWord.IndexOf('_') == -1) // if there are no underscores in the censored word, player wins
                return GameStatus.Won;
            return GameStatus.Playing;
        }

        private static byte GetCharIndex(char c) {
            if (c >= 'A' && c <= 'Z')
                return (byte)(c - 'A');
            else if (c >= 'a' && c <= 'z')
                return (byte)(c - 'a');
            else
                return 0xff;
        }
    }
}
