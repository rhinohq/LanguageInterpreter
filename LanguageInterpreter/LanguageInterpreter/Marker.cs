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
            
        }
        
        public bool MarkExprs()
        {
            
        }
        
        public bool MarkControlStructs()
        {
            foreach (ControlStructure ConStruct in Code.ControlStructures)
            {
                if (MarkScheme.Contains(ConStruct))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}