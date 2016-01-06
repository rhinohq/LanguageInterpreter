// Setup for Lexer for the language Lua.

namespace LanguageInterpreter.Lua
{
    internal class LuaLexer
    {
        // Constant variables for storing the tag types in an easily accessible and readable way.
        public const string RESERVED = "RESERVED";
        public const string INT = "INT";
        public const string ID = "ID";

        // Expressions that are used for the token creation for Lua. These are the operators and keywords used by Lua.
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

        // Function for returning the tokens from the source code.
        public string[,] LuaLex(string Characters)
        {
            Lexer Lexer = new Lexer();

            return Lexer.Lex(Characters, TokenExpressions);
        }
    }
}