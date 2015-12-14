namespace LanguageInterpreter.Lua
{
    internal class LuaParser
    {
        private Tag INT = new Tag();
        private Tag ID = new Tag();

        public Reserved Keyword(string KW)
        {
            Reserved KeywordTag = new Reserved();

            KeywordTag.Value = KW;
            KeywordTag.Tag = LuaLexer.RESERVED;

            return KeywordTag;
        }

        public Phase Parser()
        {
            Phase phrase = new Phase();

            return phrase;
        }
    }
}