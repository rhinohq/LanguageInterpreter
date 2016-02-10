using System;

namespace LanguageInterpreter.LuaValue
{
    public class LuaError : Exception
    {
        public LuaError(string message)
            : base(message)
        {
        }

        public LuaError(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public LuaError(string messageformat, params object[] args)
            : base(string.Format(messageformat, args))
        {
        }
    }
}