namespace LanguageInterpreter.Python
{
    internal class PythonLexer
    {
        private const string RESERVED = "RESERVED";
        private const string INT = "INT";
        private const string ID = "ID";

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

        public string[,] PythonLex(string[] Characters)
        {
            Lexer Lexer = new Lexer();

            return Lexer.Lex(Characters, TokenExpressions);
        }
    }
}