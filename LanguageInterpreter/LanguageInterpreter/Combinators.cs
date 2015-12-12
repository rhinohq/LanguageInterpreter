using System;

namespace LanguageInterpreter
{
    class Result
    {
        public string Repr(string Token, int Pos)
        {
            return "Result(" + Token + ", " + Pos.ToString() + ")";
        }
    }

    class Parser
    {
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

    class Tag : Parser
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

    class Reserved : Parser
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

    class Concat : Parser
    {

    }

    class Exp : Parser
    {

    }

    class Alternate : Parser
    {

    }

    class Opt : Parser
    {

    }

    class Rep : Parser
    {

    }

    class Process : Parser
    {

    }

    class Lazy : Parser
    {

    }

    class Phase : Parser
    {

    }
}