using System.Collections.Generic;

namespace Language.Lua
{
    public partial class Access
    {
    }

    public partial class Args
    {
        public List<Expr> ArgList = new List<Expr>();

        public StringLiteral String;

        public TableConstructor Table;
    }

    public partial class Assignment : Statement
    {
        public List<Var> VarList = new List<Var>();

        public List<Expr> ExprList = new List<Expr>();
    }

    public partial class BaseExpr
    {
    }

    public partial class BoolLiteral : Term
    {
        public string Text;
    }

    public partial class BreakStmt : Statement
    {
    }

    public partial class Chunk
    {
        public List<Statement> Statements = new List<Statement>();
    }

    public partial class DoStmt : Statement
    {
        public Chunk Body;
    }

    public partial class ElseifBlock
    {
        public Expr Condition;

        public Chunk ThenBlock;
    }

    public partial class Expr
    {
    }

    public partial class ExprStmt : Statement
    {
        public Expr Expr;
    }

    public partial class Field
    {
        public Expr Value;
    }

    public partial class ForInStmt : Statement
    {
        public List<string> NameList = new List<string>();

        public List<Expr> ExprList = new List<Expr>();

        public Chunk Body;
    }

    public partial class ForStmt : Statement
    {
        public string VarName;

        public Expr Start;

        public Expr End;

        public Expr Step;

        public Chunk Body;
    }

    public partial class Function : Statement
    {
        public FunctionName Name;

        public FunctionBody Body;
    }

    public partial class FunctionBody
    {
        public ParamList ParamList;

        public Chunk Chunk;
    }

    public partial class FunctionCall : Access
    {
        public Args Args;
    }

    public partial class FunctionName
    {
        public List<string> FullName = new List<string>();

        public string MethodName;
    }

    public partial class FunctionValue : Term
    {
        public FunctionBody Body;
    }

    public partial class GroupExpr : BaseExpr
    {
        public Expr Expr;
    }

    public partial class IfStmt : Statement
    {
        public Expr Condition;

        public Chunk ThenBlock;

        public List<ElseifBlock> ElseifBlocks = new List<ElseifBlock>();

        public Chunk ElseBlock;
    }

    public partial class ItemValue : Field
    {
    }

    public partial class KeyAccess : Access
    {
        public Expr Key;
    }

    public partial class KeyValue : Field
    {
        public Expr Key;
    }

    public partial class LocalFunc : Statement
    {
        public string Name;

        public FunctionBody Body;
    }

    public partial class LocalVar : Statement
    {
        public List<string> NameList = new List<string>();

        public List<Expr> ExprList = new List<Expr>();
    }

    public partial class MethodCall : Access
    {
        public string Method;

        public Args Args;
    }

    public partial class NameAccess : Access
    {
        public string Name;
    }

    public partial class NameValue : Field
    {
        public string Name;
    }

    public partial class NilLiteral : Term
    {
    }

    public partial class NumberLiteral : Term
    {
        public string HexicalText;

        public string Text;
    }

    public partial class OperatorExpr : Expr
    {
    }

    public partial class ParamList
    {
        public List<string> NameList = new List<string>();

        public bool HasVarArg;

        public string IsVarArg;
    }

    public partial class PrimaryExpr : Term
    {
        public BaseExpr Base;

        public List<Access> Accesses = new List<Access>();
    }

    public partial class RepeatStmt : Statement
    {
        public Chunk Body;

        public Expr Condition;
    }

    public partial class ReturnStmt : Statement
    {
        public List<Expr> ExprList = new List<Expr>();
    }

    public partial class Statement
    {
    }

    public partial class StringLiteral : Term
    {
        public string Text;
    }

    public partial class TableConstructor : Term
    {
        public List<Field> FieldList = new List<Field>();
    }

    public partial class Term : Expr
    {
    }

    public partial class Var
    {
        public BaseExpr Base;

        public List<Access> Accesses = new List<Access>();
    }

    public partial class VariableArg : Term
    {
        public string Name;
    }

    public partial class VarName : BaseExpr
    {
        public string Name;
    }

    public partial class WhileStmt : Statement
    {
        public Expr Condition;

        public Chunk Body;
    }
}