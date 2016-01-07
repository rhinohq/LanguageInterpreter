using System;
using System.Collections.Generic;

namespace LanguageInterpreter
{
    public class ExMarkScheme
    {
        public class Variable : IEquatable<Variable>
        {
            public string VarName { get; set; }
            public string VarType { get; set; }
            public string VarValue { get; set; }

            public bool Equals(Variable other)
            {
                throw new NotImplementedException();
            }
        }
        
        public class Expression : IEquatable<Expression>
        {
            public string ExpressionType { get; set; }
            public string ExpressionStr { get; set; }

            public bool Equals(Expression other)
            {
                throw new NotImplementedException();
            }
        }
        
        public class ControlStructure : IEquatable<ControlStructure>
        {
            public string StructureType { get; set; }
            public string StrutureCondition {get; set; }

            public bool Equals(ControlStructure other)
            {
                throw new NotImplementedException();
            }
        }
        
        public string Output { get; set; }
        
        public bool CheckOutput { get; set; }
        public bool CheckVars { get; set; }
        public bool CheckExprs { get; set; }
        public bool CheckConStruct { get; set; }
        
        public List<Variable> AssignedVariables = new List<Variable>();
        public List<Expression> Expressions = new List<Expression>();
        public List<ControlStructure> ControlStructures = new List<ControlStructure>();
    }
    
    public class UserCode
    {
        public class Variable : IEquatable<Variable>
        {
            public string VarName { get; set; }
            public string VarType { get; set; }
            public string VarValue { get; set; }

            public bool Equals(Variable other)
            {
                throw new NotImplementedException();
            }
        }
        
        public class Expression : IEquatable<Expression>
        {
            public string ExpressionType { get; set; }
            public string ExpressionStr { get; set; }

            public bool Equals(Expression other)
            {
                throw new NotImplementedException();
            }
        }
        
        public class ControlStructure : IEquatable<ControlStructure>
        {
            public string StructureType { get; set; }
            public string StrutureCondition {get; set; }

            public bool Equals(ControlStructure other)
            {
                throw new NotImplementedException();
            }
        }
        
        public string Output { get; set; }
        
        public List<Variable> AssignedVariables = new List<Variable>();
        public List<Expression> Expressions = new List<Expression>();
        public List<ControlStructure> ControlStructures = new List<ControlStructure>();
    }
}