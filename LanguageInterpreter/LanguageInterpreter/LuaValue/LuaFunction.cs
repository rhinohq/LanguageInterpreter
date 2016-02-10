namespace LanguageInterpreter.LuaValue
{
    public delegate LuaValue LuaFunc(LuaValue[] args);

    public class LuaFunction : LuaValue
    {
        public LuaFunction(LuaFunc function)
        {
            Function = function;
        }

        public LuaFunc Function { get; set; }

        public override object Value
        {
            get { return Function; }
        }

        public override string GetTypeCode()
        {
            return "function";
        }

        public LuaValue Invoke(LuaValue[] args)
        {
            return Function.Invoke(args);
        }
    }
}