namespace Language.Lua
{
    public class LuaNumber : LuaValue
    {
        public LuaNumber(double number)
        {
            Number = number;
        }

        public double Number { get; set; }

        public override object Value
        {
            get { return Number; }
        }

        public override string GetTypeCode()
        {
            return "number";
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}