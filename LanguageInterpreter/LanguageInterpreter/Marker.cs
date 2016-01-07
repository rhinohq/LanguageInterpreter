using System;
using System.Text;
using System.Collections.Generic;

namespace LanguageInterpreter
{
    public class Marker
    {
        public ExMarkScheme MarkScheme { get; set; }
        public UserCode Code { get; set; }
        
        public bool MarkOutput()
        {
            if (MarkScheme.Output == Code.Output)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool MarkVars()
        {
            foreach (UserCode.Variable Var in Code.AssignedVariables)
            {
                if (MarkScheme.Contains(Var))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public bool MarkExprs()
        {
            foreach (UserCode.Expression Expr in Code.Expressions)
            {
                if (MarkScheme.Contains(Expr))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public bool MarkControlStructs()
        {
            foreach (UserCode.ControlStructure ConStruct in Code.ControlStructures)
            {
                if (MarkScheme.Contains(ConStruct))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}