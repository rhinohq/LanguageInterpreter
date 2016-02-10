using System;
using System.Collections.Generic;
using System.IO;

namespace Language.Lua.Library
{
    public static class IOLib
    {
        public static void RegisterModule(LuaTable enviroment)
        {
            LuaTable module = new LuaTable();
            RegisterFunctions(module);
            enviroment.SetNameValue("io", module);
        }

        public static void RegisterFunctions(LuaTable module)
        {
            module.Register("input", input);
            module.Register("output", output);
            module.Register("tmpfile", tmpfile);
        }

        private static TextReader DefaultInput = Console.In;
        private static TextWriter DefaultOutput = Console.Out;

        public static LuaValue input(LuaValue[] values)
        {
            if (values == null || values.Length == 0)
            {
                return new LuaUserdata(DefaultInput);
            }
            else
            {
                LuaString file = values[0] as LuaString;
                if (file != null)
                {
                    DefaultInput = File.OpenText(file.Text);
                    return null;
                }

                LuaUserdata data = values[0] as LuaUserdata;
                if (data != null && data.Value is TextReader)
                {
                    DefaultInput = data.Value as TextReader;
                }
                return null;
            }
        }

        public static LuaValue output(LuaValue[] values)
        {
            if (values == null || values.Length == 0)
            {
                return new LuaUserdata(DefaultOutput);
            }
            else
            {
                LuaString file = values[0] as LuaString;
                if (file != null)
                {
                    DefaultOutput = File.CreateText(file.Text);
                    return null;
                }

                LuaUserdata data = values[0] as LuaUserdata;
                if (data != null && data.Value is TextWriter)
                {
                    DefaultOutput = data.Value as TextWriter;
                }
                return null;
            }
        }

        public static LuaValue tmpfile(LuaValue[] values)
        {
            StreamWriter writer = File.CreateText(Path.GetTempFileName());
            return new LuaUserdata(writer);
        }
    }
}