// This is the AST (Abstract Syntax Tree).

namespace LanguageInterpreter.Lua
{
    internal class Statement : Equality
    {
    }

    internal class Aexp : Equality
    {
    }

    internal class Bexp : Equality
    {
    }

    internal class AssignStatement : Statement
    {
        public string Name { get; set; }
    }

    internal class CompoundStatement : Statement
    {
    }

    internal class IfStatement : Statement
    {
    }

    internal class WhileStatement : Statement
    {
    }

    internal class IntAexp : Aexp
    {
    }

    internal class VarAexp : Aexp
    {
    }

    internal class BinopAexp : Aexp
    {
    }

    internal class RelopBexp : Bexp
    {
    }

    internal class AndBexp : Bexp
    {
    }

    internal class OrBexp : Bexp
    {
    }

    internal class NotBexp : Bexp
    {
    }
}