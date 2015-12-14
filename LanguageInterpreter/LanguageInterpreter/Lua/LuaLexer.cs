namespace LanguageInterpreter.Lua
{
    internal class LuaLexer
    {
        public const string RESERVED = "RESERVED";
        public const string INT = "INT";
        public const string ID = "ID";

        private string[,] TokenExpressions = new string[,] {
            { @"and", null },
            { @"break", null },
            { @"do", RESERVED },
            { @"else", RESERVED },
            { @"elseif", RESERVED },
            { @"end", RESERVED },
            { @"false", RESERVED },
            { @"for", RESERVED },
            { @"function", RESERVED },
            { @"goto", RESERVED },
            { @"if", RESERVED },
            { @"in", RESERVED },
            { @"local", RESERVED },
            { @"nil", RESERVED },
            { @"not", RESERVED },
            { @"or", RESERVED },
            { @"repeat", RESERVED },
            { @"return", RESERVED },
            { @"then", RESERVED },
            { @"true", RESERVED },
            { @"until", RESERVED },
            { @"while", RESERVED },
            { @"+", RESERVED },
            { @"-", RESERVED },
            { @"*", RESERVED },
            { @"/", RESERVED },
            { @"%", RESERVED },
            { @"^", RESERVED },
            { @"#", RESERVED },
            { @"&", RESERVED },
            { @"~", RESERVED },
            { @"|", RESERVED },
            { @"<<", RESERVED },
            { @">>", RESERVED },
            { @"//", RESERVED },
            { @" ", RESERVED },
            { @"(", RESERVED },
            { @")", RESERVED },
            { @"{", RESERVED },
            { @"}", RESERVED },
            { @"[", RESERVED },
            { @"]", RESERVED },
            { @"::", RESERVED },
            { @";", RESERVED },
            { @":", RESERVED },
            { @",", RESERVED },
            { @".", RESERVED },
            { @"..", RESERVED },
            { @"...", RESERVED },
            { @"[0-9]+", INT },
            { @"[A-Za-z][A-Za-z0-9_]*", ID }
        };

        public string[,] LuaLex(string[] Characters)
        {
            Lexer Lexer = new Lexer();

            return Lexer.Lex(Characters, TokenExpressions);
        }
    }
}