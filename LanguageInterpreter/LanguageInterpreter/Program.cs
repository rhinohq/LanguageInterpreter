using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public void Usage()
        {
            Console.WriteLine("Usage: <Source Code Filename>");
            Environment.Exit(0);
        }
    }
}
