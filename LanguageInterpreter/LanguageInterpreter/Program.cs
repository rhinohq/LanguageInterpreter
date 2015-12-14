using System;
// Program entry point.

namespace LanguageInterpreter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }

        // What is printed when the command-line arguments and not entered correctly.
        public void Usage()
        {
            Console.WriteLine("Usage: <Source Code Filename>");
            Environment.Exit(0);
        }
    }
}