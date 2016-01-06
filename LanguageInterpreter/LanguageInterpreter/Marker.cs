using System;
using System.Text;
using System.Collections.Generic;

namespace LanguageInterpreter
{
    public class Marker
    {
        public MarkerLists.ExMarkScheme MarkScheme { get; set; }
        public MarkerLists.UserCode Code { get; set; }
        
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
            foreach (Variable Var in Code.AssignedVariables)
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
            foreach (Expression Expr in Code.Expressions)
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
            foreach (ControlStructure ConStruct in Code.ControlStructures)
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