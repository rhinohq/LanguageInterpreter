// Deprecated class. This is the Lexer for Python, the language this interpreter was originally for.

namespace LanguageInterpreter.Python
{
    internal class PythonLexer
    {
        public const string RESERVED = "RESERVED";
        public const string INT = "INT";
        public const string ID = "ID";

        // 2D Array storing the keywords, operators and syntax of Python for the Lexer.
        private string[,] TokenExpressions = new string[,] {
            { @"[ \n\t]+", null },
            { @"#[^\n]*", null },
            { @"\=", RESERVED },
            { @"\(", RESERVED },
            { @"\)", RESERVED },
            { @":", RESERVED },
            { @".", RESERVED },
            { @",", RESERVED },
            { @"\+", RESERVED },
            { @"-", RESERVED },
            { @"\*", RESERVED },
            { @"\**", RESERVED },
            { @"/", RESERVED },
            { @"//", RESERVED },
            { @"%", RESERVED },
            { @"<=", RESERVED },
            { @">=", RESERVED },
            { @"<", RESERVED },
            { @">", RESERVED },
            { @"==", RESERVED },
            { @"!=", RESERVED },
            { @"<>", RESERVED },
            { @"+=", RESERVED },
            { @"-=", RESERVED },
            { @"*=", RESERVED },
            { @"/=", RESERVED },
            { @"%=", RESERVED },
            { @"**=", RESERVED },
            { @"//=", RESERVED },
            { @"and", RESERVED },
            { @"or", RESERVED },
            { @"not", RESERVED },
            { @"in", RESERVED },
            { @"not in", RESERVED },
            { @"is", RESERVED },
            { @"is not", RESERVED },
            { @"if", RESERVED },
            { @"elif", RESERVED },
            { @"else", RESERVED },
            { @"while", RESERVED },
            { @"for", RESERVED },
            { @"print", RESERVED },
            { @"import", RESERVED },
            { @"[0-9]+", INT },
            { @"[A-Za-z][A-Za-z0-9_]*", ID }
        };

        public string[,] PythonLex(string Characters)
        {
            Lexer Lexer = new Lexer();

            return Lexer.Lex(Characters, TokenExpressions);
        }
    }
}