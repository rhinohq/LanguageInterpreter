using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInterpreter.Lua
{
    class Statement : Equality
    {

    }

    class Aexp : Equality
    {

    }

    class Bexp : Equality
    {

    }

    class AssignStatement : Statement
    {
        public string Name { get; set; }
    }

    class CompoundStatement : Statement
    {

    }

    class IfStatement : Statement
    {

    }

    class WhileStatement : Statement
    {

    }

    class IntAexp : Aexp
    {

    }

    class VarAexp : Aexp
    {

    }

    class BinopAexp : Aexp
    {

    }

    class RelopBexp : Bexp
    {

    }

    class AndBexp : Bexp
    {

    }

    class OrBexp : Bexp
    {

    }

    class NotBexp : Bexp
    {

    }
}
