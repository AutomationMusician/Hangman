using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    abstract class Guesser
    {
        protected Executioner mExecutioner;
        public Guesser(Executioner executioner)
        {
            this.mExecutioner = executioner;
        }

        abstract protected char GuessCharacter(GameState state);

        public void Play()
        {
            GameState state = mExecutioner.GetGameState();
            Console.WriteLine(FormatState(state));
            while (state.Status == GameStatus.Playing)
            {
                char c = GuessCharacter(state);
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
                if (state.Status == GameStatus.Playing)
                    Console.WriteLine(FormatState(state));
                else
                    Console.WriteLine();
            }
            if (state.Status == GameStatus.Won)
            {
                Console.WriteLine("YOU WIN!");
                Console.WriteLine("The word was '" + state.CensoredWord + "'");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("YOU LOSE.");
                Console.WriteLine("The word was '" + state.CensoredWord + "'");
                Console.WriteLine();
            }
        }

        private string FormatState(GameState state)
        {
            StringBuilder bldr = new StringBuilder();
            foreach (char c in state.CensoredWord)
            {
                bldr.Append(c);
                bldr.Append(' ');
            }
            bldr.Remove(bldr.Length - 1, 1); // Remove final space
            bldr.AppendLine();
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
