using System;

namespace LanguageInterpreter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }

        public void Usage()
        {
            Console.WriteLine("Usage: <Source Code Filename>");
            Environment.Exit(0);
        }
    }
}