// This is a Lexer. The Lexer converts the source code that the user writes and converts it into tokens that can be read by the parser.

using System;
using System.Text.RegularExpressions;

namespace LanguageInterpreter
{
    internal class Lexer
    {
        // Function that converts the source code in to tokens.
        public string[,] Lex(string Characters, string[,] TokenExpressions)
        {
            int Pos = 0;
            int Count = 0;
            int TokenIndex = 0;
            string[,] Tokens = new string[,] { };
            string Text = "";
            bool Match = false;

            while (Pos < Characters.Length)
            {
                foreach (string TokenExpression in TokenExpressions)
                {
                    Regex Regex = new Regex(TokenExpression, RegexOptions.None);
                    Match = Regex.IsMatch(Characters);

                    if (Match)
                    {
                        Text = Convert.ToString(Regex.Match(Characters, Pos));

                        Tokens[TokenIndex, 0] = Text;
                        Tokens[0, TokenIndex] = TokenExpressions[0, Count];

                        TokenIndex++;

                        break;
                    }

                    Count++;
                }

                if (!Match)
                {
                    Console.WriteLine("Illegal character: ");
                    Environment.Exit(0);
                }
                else
                {
                    Pos = Characters.IndexOf(Text, Pos);
                }
            }

            return Tokens;
        }
    }
}