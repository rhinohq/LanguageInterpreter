// Program entry point.

using System;
using System.IO;

namespace LanguageInterpreter
{
    class Program
    {
        private static void Main(string[] args)
        {
            // If the expected command line arguments have not been given, it prints the usage guide.
            if (args.Length != 2)
                Usage();

            // Read all the source code from specified file.
            string Code = File.ReadAllText(args[1]);

            // If Lua is picked, it runs the Lua interpreter.
            if (args[0] == "/l")
                InterpretLua(Code);
        }

        // The Usage Guide
        public static void Usage()
        {
            Console.WriteLine("Usage: <Language Choice> <Source Code Filename>");
            Console.WriteLine("Lua: /l");
            Console.WriteLine("Python: /p");
            Environment.Exit(0);
        }

        // Lua Interpreter
        public static void InterpretLua(string Code)
        {
            Lua.LuaLexer Lexer = new Lua.LuaLexer();
            string[,] Tokens = Lexer.LuaLex(Code);

            Lua.LuaParser Parser = new Lua.LuaParser();
        }
    }
}