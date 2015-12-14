// This is a combinator. A combinator is a function which produces a parser as its output, usually after taking one or more parsers as input.

namespace LanguageInterpreter
{
    internal class Result
    {
        public string Repr(string Token, int Pos)
        {
            return "Result(" + Token + ", " + Pos.ToString() + ")";
        }
    }

    // The class that defines the parsers. Parsers are used to create the AST.
    internal class Parser
    {
        public string Call(string[,] Tokens, int Pos)
        {
            return null;
        }

        public Concat Add()
        {
            Concat concat = new Concat();

            return concat;
        }

        public Exp Mul()
        {
            Exp exp = new Exp();

            return exp;
        }

        public Alternate Or()
        {
            Alternate alternate = new Alternate();

            return alternate;
        }

        public Process Xor()
        {
            Process process = new Process();

            return process;
        }
    }

    internal class Tag : Parser
    {
        public string TokenTag { get; set; }

        public string Call(string[,] Tokens, int Pos)
        {
            if (Pos < Tokens.Length && Tokens[Pos, 0] == TokenTag)
            {
                Result result = new Result();

                return result.Repr(Tokens[Pos, 0], Pos++);
            }
            else
                return null;
        }
    }

    // Reserved will be used to parse reserved words and operators; it will accept tokens with a specific value and tag.
    internal class Reserved : Parser
    {
        public string Value { get; set; }
        public string Tag { get; set; }

        public string Call(string[,] Tokens, int Pos)
        {
            if (Pos < Tokens.Length && Tokens[Pos, 0] == Value && Tokens[Pos, 1] == Tag)
            {
                Result result = new Result();

                return result.Repr(Tokens[Pos, 0], Pos++);
            }
            else
                return null;
        }
    }

    internal class Concat : Parser
    {
        public Parser Left { get; set; }
        public Parser Right { get; set; }
    }

    internal class Exp : Parser
    {
    }

    internal class Alternate : Parser
    {
    }

    internal class Opt : Parser
    {
    }

    internal class Rep : Parser
    {
    }

    internal class Process : Parser
    {
    }

    internal class Lazy : Parser
    {
    }

    internal class Phase : Parser
    {
    }
}