using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class ManualGuesser : Guesser
    {
        public ManualGuesser(Executioner executioner) : base(executioner) { }

        protected override char GuessCharacter(GameState state)
        {
            Console.WriteLine();
            Console.Write("Make a guess: ");
            string? line = Console.ReadLine();
            if (line != null && line.Length > 0)
            {
                char c = line[0];
                if (c >= 'a' && c <= 'z')
                    return (char)(c - 'a' + 'A');
                return c;
            }
            else
                return '\0';
        }
    }
}
