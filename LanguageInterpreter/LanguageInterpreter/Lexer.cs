using System;
using System.Text.RegularExpressions;
// Thhis is a Lexer. The Lexer converts the source code that the user writes and converts it into tokens that can be read by the parser.

namespace LanguageInterpreter
{
    internal class Lexer
    {
        // Function that converts the source code in to tokens.
        public string[,] Lex(string[] Characters, string[,] TokenExpressions)
        {
            int Pos = 0;
            string[,] Tokens;
            bool Match = false;

            while (Pos < Characters.Length)
            {
                foreach (string TokenExpression in TokenExpressions)
                {
                    Regex Regex = new Regex(TokenExpression, RegexOptions.None);
                    Match = Regex.IsMatch(Characters[]);

                    if (Match)
                    {
                    }
                }

                if (!Match)
                {
                    Console.WriteLine("Illegal character: ");
                    Environment.Exit(0);
                }
                else
                {
                }
            }

            return Tokens;
        }
    }
}