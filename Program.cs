using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Executioner executioner = new Executioner("1-1000");
            StdIOHandler handler = new StdIOHandler(executioner);
            handler.Play();
        }
    }
}
