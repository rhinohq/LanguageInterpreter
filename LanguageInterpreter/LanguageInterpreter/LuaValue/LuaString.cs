namespace LanguageInterpreter.LuaValue
{
    public class LuaString : LuaValue
    {
        public LuaString(string text)
        {
            Text = text;
        }

        public static readonly LuaString Empty = new LuaString(string.Empty);

        public string Text { get; set; }

        public override object Value
        {
            get { return Text; }
        }

        public override string GetTypeCode()
        {
            return "string";
        }

        public override string ToString()
        {
            return Text;
        }
    }
}