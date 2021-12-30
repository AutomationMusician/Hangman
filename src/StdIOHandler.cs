using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class StdIOHandler
    {
        private Executioner mExecutioner;
        public StdIOHandler(Executioner executioner)
        {
            this.mExecutioner = executioner;
        }

        public void Play()
        {
            GameState state = mExecutioner.GetGameState();
            Console.WriteLine(FormatState(state));
            while (state.Status == GameStatus.Playing)
            {
                char c = PromptForChar();
                state = mExecutioner.Guess(c);
                Console.WriteLine();
                switch (state._GuessResponse)
                {
                    case GuessResponse.Forbiden:
                        Console.WriteLine("Guessing is forbidden in the current game state.");
                        break;
                    case GuessResponse.Invalid:
                        Console.WriteLine("Invalid character. Try again.");
                        break;
                    case GuessResponse.AlreadyGuessed:
                        Console.WriteLine("'" + c + "' was already guessed. Try again.");
                        break;
                    case GuessResponse.Correct:
                        Console.WriteLine("'" + c + "' appears in the word!");
                        break;
                    case GuessResponse.Incorrect:
                        Console.WriteLine("'" + c + "' is not in the word.");
                        break;
                }
                Console.WriteLine(FormatState(state));
            }
            if (state.Status == GameStatus.Won)
                Console.WriteLine("YOU WIN!");
            else
                Console.WriteLine("YOU LOSE.");
        }

        private char PromptForChar()
        {
            Console.Write("Make a guess: ");
            string? line = Console.ReadLine();
            if (line != null && line.Length > 0)
                return line[0];
            else
                return '\0';
        }

        private string FormatState(GameState state)
        {
            StringBuilder bldr = new StringBuilder();
            bldr.AppendLine(state.CensoredWord);
            bldr.AppendLine();
            bldr.AppendLine("Guesses Left: " + state.GuessesLeft);
            if (state.IncorrectlyGuessed.Count > 0)
                bldr.AppendLine("Guessed Incorrectly: " + CommaDelimetedChars(state.IncorrectlyGuessed));
            if (state.CorrectlyGuessed.Count > 0)
                bldr.AppendLine("Guessed Correctly:   " + CommaDelimetedChars(state.CorrectlyGuessed));
            bldr.AppendLine();
            return bldr.ToString();
        }

        private static string CommaDelimetedChars(List<char> chars) {
            StringBuilder bldr = new StringBuilder();
            for (int i=0; i<chars.Count - 1; i++)
            {
                bldr.Append(chars[i] + ", ");
            }
            if (chars.Count > 0)
                bldr.Append(chars[chars.Count - 1]);
            return bldr.ToString();
        }
    }
}
