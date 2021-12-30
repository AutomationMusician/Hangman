using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class GameState {
        public GameStatus Status { get; }
        public GuessResponse _GuessResponse { get; }
        public int GuessesLeft { get; }
        public string CensoredWord { get; }
        public List<char> CorrectlyGuessed { get; }
        public List<char> IncorrectlyGuessed { get; }

        public GameState(GameStatus status, GuessResponse guessResponse, int guessesLeft, string censoredWord, List<char> incorrectlyGuessed, List<char> correctlyGuessed)
        {
            Status = status;
            _GuessResponse = guessResponse;
            GuessesLeft = guessesLeft;
            CensoredWord = censoredWord;
            IncorrectlyGuessed = incorrectlyGuessed;
            CorrectlyGuessed = correctlyGuessed;
        }
    }
}
