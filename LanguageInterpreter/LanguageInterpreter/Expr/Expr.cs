using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageInterpreter.Expr
{
    public abstract partial class Access
    {
        public abstract LuaValue Evaluate(LuaValue baseValue, LuaTable enviroment);
    }

    public abstract partial class BaseExpr : Term
    {
    }

    public partial class BoolLiteral : Term
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            return LuaBoolean.From(bool.Parse(Text));
        }
    }

    public abstract partial class Expr
    {
        public abstract LuaValue Evaluate(LuaTable enviroment);

        public abstract Term Simplify();
    }

    public partial class FunctionBody
    {
        public LuaValue Evaluate(LuaTable enviroment)
        {
            return new LuaFunction(
                new LuaFunc(delegate (LuaValue[] args)
                {
                    var table = new LuaTable(enviroment);

                    List<string> names = ParamList.NameList;

                    if (names.Count > 0)
                    {
                        int argCount = Math.Min(names.Count, args.Length);

                        for (int i = 0; i < argCount; i++)
                        {
                            table.SetNameValue(names[i], args[i]);
                        }

                        if (ParamList.HasVarArg)
                        {
                            if (argCount < args.Length)
                            {
                                LuaValue[] remainedArgs = new LuaValue[args.Length - argCount];
                                for (int i = 0; i < remainedArgs.Length; i++)
                                {
                                    remainedArgs[i] = args[argCount + i];
                                }
                                table.SetNameValue("...", new LuaMultiValue(remainedArgs));
                            }
                        }
                    }
                    else if (ParamList.IsVarArg != null)
                    {
                        table.SetNameValue("...", new LuaMultiValue(args));
                    }

                    Chunk.Enviroment = table;

                    return Chunk.Execute();
                })
            );
        }
    }

    public partial class FunctionCall : Access
    {
        public override LuaValue Evaluate(LuaValue baseValue, LuaTable enviroment)
        {
            LuaFunction function = baseValue as LuaFunction;

            if (function != null)
            {
                if (function.Function.Method.DeclaringType.FullName == "Language.Lua.Library.BaseLib" &&
                    (function.Function.Method.Name == "loadstring" || function.Function.Method.Name == "dofile"))
                {
                    if (Args.String != null)
                    {
                        return function.Function.Invoke(new LuaValue[] { Args.String.Evaluate(enviroment), enviroment });
                    }
                    else
                    {
                        return function.Function.Invoke(new LuaValue[] { Args.ArgList[0].Evaluate(enviroment), enviroment });
                    }
                }

                if (Args.Table != null)
                {
                    return function.Function.Invoke(new LuaValue[] { Args.Table.Evaluate(enviroment) });
                }
                else if (Args.String != null)
                {
                    return function.Function.Invoke(new LuaValue[] { Args.String.Evaluate(enviroment) });
                }
                else
                {
                    List<LuaValue> args = Args.ArgList.ConvertAll(arg => arg.Evaluate(enviroment));

                    return function.Function.Invoke(LuaMultiValue.UnWrapLuaValues(args.ToArray()));
                }
            }
            else
            {
                throw new Exception("Invoke function call on non function value.");
            }
        }
    }

    public partial class FunctionValue : Term
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            return Body.Evaluate(enviroment);
        }
    }

    public partial class GroupExpr : BaseExpr
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            return Expr.Evaluate(enviroment);
        }

        public override Term Simplify()
        {
            return Expr.Simplify();
        }
    }

    public partial class KeyAccess : Access
    {
        public override LuaValue Evaluate(LuaValue baseValue, LuaTable enviroment)
        {
            LuaValue key = Key.Evaluate(enviroment);
            return LuaValue.GetKeyValue(baseValue, key);
        }
    }

    public partial class MethodCall : Access
    {
        public override LuaValue Evaluate(LuaValue baseValue, LuaTable enviroment)
        {
            LuaValue value = LuaValue.GetKeyValue(baseValue, new LuaString(Method));
            LuaFunction function = value as LuaFunction;

            if (function != null)
            {
                if (Args.Table != null)
                {
                    return function.Function.Invoke(new LuaValue[] { baseValue, Args.Table.Evaluate(enviroment) });
                }
                else if (Args.String != null)
                {
                    return function.Function.Invoke(new LuaValue[] { baseValue, Args.String.Evaluate(enviroment) });
                }
                else
                {
                    List<LuaValue> args = Args.ArgList.ConvertAll(arg => arg.Evaluate(enviroment));
                    args.Insert(0, baseValue);
                    return function.Function.Invoke(args.ToArray());
                }
            }
            else
            {
                throw new Exception("Invoke method call on non function value.");
            }
        }
    }

    public partial class NameAccess : Access
    {
        public override LuaValue Evaluate(LuaValue baseValue, LuaTable enviroment)
        {
            LuaValue key = new LuaString(Name);
            return LuaValue.GetKeyValue(baseValue, key);
        }
    }

    public partial class NilLiteral : Term
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            return LuaNil.Nil;
        }
    }

    public partial class NumberLiteral : Term
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            double number;

            if (string.IsNullOrEmpty(HexicalText))
            {
                number = double.Parse(Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign);
            }
            else
            {
                number = int.Parse(HexicalText, NumberStyles.HexNumber);
            }

            return new LuaNumber(number);
        }
    }

    /// <summary>
    /// Represent Unary or Binary Operation, for Unary Operation the LeftOperand is not used.
    /// </summary>
    public partial class Operation : Term
    {
        public string Operator;

        public Term LeftOperand;

        public Term RightOperand;

        public Operation(string oper)
        {
            Operator = oper;
        }

        public Operation(string oper, Term left, Term right)
        {
            Operator = oper;
            LeftOperand = left == null ? null : left.Simplify();
            RightOperand = right == null ? null : right.Simplify();
        }

        public override LuaValue Evaluate(LuaTable enviroment)
        {
            if (LeftOperand == null)
            {
                return PrefixUnaryOperation(Operator, RightOperand, enviroment);
            }
            else if (RightOperand == null)
            {
                return LeftOperand.Evaluate(enviroment);
            }
            else
            {
                return InfixBinaryOperation(LeftOperand, Operator, RightOperand, enviroment);
            }
        }

        private LuaValue PrefixUnaryOperation(string Operator, Term RightOperand, LuaTable enviroment)
        {
            LuaValue rightValue = RightOperand.Evaluate(enviroment);

            switch (Operator)
            {
                case "-":
                    var number = rightValue as LuaNumber;
                    if (number != null)
                    {
                        return new LuaNumber(-number.Number);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__unm", rightValue, null);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { rightValue });
                        }
                    }
                    break;

                case "#":
                    var table = rightValue as LuaTable;
                    if (table != null)
                    {
                        return new LuaNumber(table.Length);
                    }
                    var str = rightValue as LuaString;
                    if (str != null)
                    {
                        return new LuaNumber(str.Text.Length);
                    }
                    break;

                case "not":
                    var rightBool = rightValue as LuaBoolean;
                    if (rightBool != null)
                    {
                        return LuaBoolean.From(!rightBool.BoolValue);
                    }
                    break;
            }

            return LuaNil.Nil;
        }

        private LuaValue InfixBinaryOperation(Term LeftOperand, string Operator, Term RightOperand, LuaTable enviroment)
        {
            LuaValue leftValue = LeftOperand.Evaluate(enviroment);
            LuaValue rightValue = RightOperand.Evaluate(enviroment);

            switch (Operator)
            {
                case "+":
                    var left = leftValue as LuaNumber;
                    var right = rightValue as LuaNumber;
                    if (left != null && right != null)
                    {
                        return new LuaNumber(left.Number + right.Number);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__add", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "-":
                    left = leftValue as LuaNumber;
                    right = rightValue as LuaNumber;
                    if (left != null && right != null)
                    {
                        return new LuaNumber(left.Number - right.Number);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__sub", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "*":
                    left = leftValue as LuaNumber;
                    right = rightValue as LuaNumber;
                    if (left != null && right != null)
                    {
                        return new LuaNumber(left.Number * right.Number);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__mul", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "/":
                    left = leftValue as LuaNumber;
                    right = rightValue as LuaNumber;
                    if (left != null && right != null)
                    {
                        return new LuaNumber(left.Number / right.Number);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__div", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "%":
                    left = leftValue as LuaNumber;
                    right = rightValue as LuaNumber;
                    if (left != null && right != null)
                    {
                        return new LuaNumber(left.Number % right.Number);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__mod", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "^":
                    left = leftValue as LuaNumber;
                    right = rightValue as LuaNumber;
                    if (left != null && right != null)
                    {
                        return new LuaNumber(Math.Pow(left.Number, right.Number));
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__pow", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "==":
                    return LuaBoolean.From(leftValue.Equals(rightValue));

                case "~=":
                    return LuaBoolean.From(leftValue.Equals(rightValue) == false);

                case "<":
                    int? compare = Compare(leftValue, rightValue);
                    if (compare != null)
                    {
                        return LuaBoolean.From(compare < 0);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__lt", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case ">":
                    compare = Compare(leftValue, rightValue);
                    if (compare != null)
                    {
                        return LuaBoolean.From(compare > 0);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__gt", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "<=":
                    compare = Compare(leftValue, rightValue);
                    if (compare != null)
                    {
                        return LuaBoolean.From(compare <= 0);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__le", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case ">=":
                    compare = Compare(leftValue, rightValue);
                    if (compare != null)
                    {
                        return LuaBoolean.From(compare >= 0);
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__ge", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "..":
                    if ((leftValue is LuaString || leftValue is LuaNumber) &&
                        (rightValue is LuaString || rightValue is LuaNumber))
                    {
                        return new LuaString(string.Concat(leftValue, rightValue));
                    }
                    else
                    {
                        LuaFunction func = GetMetaFunction("__concat", leftValue, rightValue);
                        if (func != null)
                        {
                            return func.Invoke(new LuaValue[] { leftValue, rightValue });
                        }
                    }
                    break;

                case "and":
                    bool leftBool = leftValue.GetBooleanValue();
                    bool rightBool = rightValue.GetBooleanValue();
                    if (leftBool == false)
                    {
                        return leftValue;
                    }
                    else
                    {
                        return rightValue;
                    }
                case "or":
                    leftBool = leftValue.GetBooleanValue();
                    rightBool = rightValue.GetBooleanValue();
                    if (leftBool == true)
                    {
                        return leftValue;
                    }
                    else
                    {
                        return rightValue;
                    }
            }

            return null;
        }

        private static int? Compare(LuaValue leftValue, LuaValue rightValue)
        {
            LuaNumber left = leftValue as LuaNumber;
            LuaNumber right = rightValue as LuaNumber;
            if (left != null && right != null)
            {
                return left.Number.CompareTo(right.Number);
            }

            LuaString leftString = leftValue as LuaString;
            LuaString rightString = rightValue as LuaString;
            if (leftString != null && rightString != null)
            {
                return StringComparer.Ordinal.Compare(leftString.Text, rightString.Text);
            }

            return null;
        }

        private static LuaFunction GetMetaFunction(string name, LuaValue leftValue, LuaValue rightValue)
        {
            LuaTable left = leftValue as LuaTable;

            if (left != null)
            {
                LuaFunction func = left.GetValue(name) as LuaFunction;

                if (func != null)
                {
                    return func;
                }
            }

            LuaTable right = rightValue as LuaTable;

            if (right != null)
            {
                return right.GetValue(name) as LuaFunction;
            }

            return null;
        }
    }

    public partial class OperatorExpr : Expr
    {
        public LinkedList<object> Terms = new LinkedList<object>();

        public void Add(string oper)
        {
            Terms.AddLast(oper);
        }

        public void Add(Term term)
        {
            Terms.AddLast(term);
        }

        public Term BuildExpressionTree()
        {
            var node = Terms.First;
            Term term = node.Value as Term;

            if (Terms.Count == 1)
            {
                return term;
            }
            else
            {
                if (term != null)
                {
                    return BuildExpressionTree(node.Value as Term, node.Next);
                }

                string oper = node.Value as string;

                if (oper != null)
                {
                    return BuildExpressionTree(null, node);
                }

                return null;
            }
        }

        // Operator-precedence parsing algorithm
        private static Term BuildExpressionTree(Term leftTerm, LinkedListNode<object> node)
        {
            string oper = node.Value as string;
            var rightNode = node.Next;
            Term rightTerm = rightNode.Value as Term;

            if (rightNode.Next == null) // last node
            {
                return new Operation(oper, leftTerm, rightTerm);
            }
            else
            {
                string nextOper = rightNode.Next.Value as string;

                if (OperTable.IsPrior(oper, nextOper))
                {
                    return BuildExpressionTree(new Operation(oper, leftTerm, rightTerm), rightNode.Next);
                }
                else
                {
                    return new Operation(oper, leftTerm, BuildExpressionTree(rightTerm, rightNode.Next));
                }
            }
        }

        public override LuaValue Evaluate(LuaTable enviroment)
        {
            Term term = BuildExpressionTree();
            return term.Evaluate(enviroment);
        }

        public override Term Simplify()
        {
            return BuildExpressionTree().Simplify();
        }
    }

    public enum Associativity
    {
        NonAssociative,
        LeftAssociative,
        RightAssociative
    }

    public class OperTable
    {
        private static Dictionary<string, int> precedence = new Dictionary<string, int>();
        private static Associativity[] associativity;

        static OperTable()
        {
            List<string[]> operators = new List<string[]>();
            operators.Add(new string[] { "or" });
            operators.Add(new string[] { "and" });
            operators.Add(new string[] { "==", "~=" });
            operators.Add(new string[] { ">", ">=", "<", "<=" });
            operators.Add(new string[] { ".." });
            operators.Add(new string[] { "+", "-" });
            operators.Add(new string[] { "*", "/", "%" });
            operators.Add(new string[] { "#", "not" });
            operators.Add(new string[] { "^" });

            for (int index = 0; index < operators.Count; index++)
            {
                foreach (string oper in operators[index])
                {
                    precedence.Add(oper, index);
                }
            }

            associativity = new Associativity[operators.Count];
            associativity[0] = Associativity.LeftAssociative;
            associativity[1] = Associativity.LeftAssociative;
            associativity[2] = Associativity.NonAssociative;
            associativity[3] = Associativity.LeftAssociative;
            associativity[4] = Associativity.LeftAssociative;
            associativity[5] = Associativity.LeftAssociative;
            associativity[6] = Associativity.LeftAssociative;
            associativity[7] = Associativity.NonAssociative;
            associativity[8] = Associativity.RightAssociative;
        }

        /// <summary>
        /// Whether the input text is an operator or not
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public static bool Contains(string oper)
        {
            return precedence.ContainsKey(oper);
        }

        /// <summary>
        /// whether operLeft has higher precedence than operRight
        /// </summary>
        /// <param name="operLeft"></param>
        /// <param name="operRight"></param>
        /// <returns></returns>
        public static bool IsPrior(string operLeft, string operRight)
        {
            if (operLeft == null) return false;
            if (operRight == null) return true;

            int priLeft = precedence[operLeft];
            int priRight = precedence[operRight];
            if (priLeft > priRight)
            {
                return true;
            }
            else if (priLeft < priRight)
            {
                return false;
            }
            else
            {
                switch (associativity[priLeft])
                {
                    case Associativity.LeftAssociative:
                        return true;

                    case Associativity.RightAssociative:
                        return false;

                    default:
                        return true;
                }
            }
        }
    }

    public partial class PrimaryExpr : Term
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            LuaValue baseValue = Base.Evaluate(enviroment);

            foreach (Access access in Accesses)
            {
                baseValue = access.Evaluate(baseValue, enviroment);
            }

            return baseValue;
        }

        public override Term Simplify()
        {
            if (Accesses.Count == 0)
            {
                return Base.Simplify();
            }
            else
            {
                return this;
            }
        }
    }

    public partial class StringLiteral : Term
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            return new LuaString(Text);
        }
    }

    public partial class TableConstructor : Term
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            LuaTable table = new LuaTable();

            foreach (Field field in FieldList)
            {
                NameValue nameValue = field as NameValue;
                if (nameValue != null)
                {
                    table.SetNameValue(nameValue.Name, nameValue.Value.Evaluate(enviroment));
                    continue;
                }

                KeyValue keyValue = field as KeyValue;
                if (keyValue != null)
                {
                    table.SetKeyValue(
                        keyValue.Key.Evaluate(enviroment),
                        keyValue.Value.Evaluate(enviroment));
                    continue;
                }

                ItemValue itemValue = field as ItemValue;
                if (itemValue != null)
                {
                    table.AddValue(itemValue.Value.Evaluate(enviroment));
                    continue;
                }
            }

            return table;
        }
    }

    public partial class Term : Expr
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            throw new NotImplementedException();
        }

        public override Term Simplify()
        {
            return this;
        }
    }

    public partial class VariableArg : Term
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            return enviroment.GetValue(Name);
        }
    }

    public partial class VarName : BaseExpr
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            return enviroment.GetValue(Name);
        }

        public override Term Simplify()
        {
            return this;
        }
    }
}