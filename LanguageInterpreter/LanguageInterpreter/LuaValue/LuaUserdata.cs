namespace LanguageInterpreter.LuaValue
{
    public class LuaUserdata : LuaValue
    {
        private object Object;

        public LuaUserdata(object obj)
        {
            Object = obj;
        }

        public LuaUserdata(object obj, LuaTable metatable)
        {
            Object = obj;
            MetaTable = metatable;
        }

        public override object Value
        {
            get { return Object; }
        }

        public LuaTable MetaTable { get; set; }

        public override string GetTypeCode()
        {
            return "userdata";
        }

        public override string ToString()
        {
            return "userdata";
        }
    }
}