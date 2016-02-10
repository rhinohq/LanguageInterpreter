using System;
using System.IO;

namespace LanguageInterpreter
{
    class LuaInterpreter
    {
        public static LuaValue RunFile(string luaFile)
        {
            return Interpreter(File.ReadAllText(luaFile));
        }

        public static LuaValue RunFile(string luaFile, LuaTable enviroment)
        {
            return Interpreter(File.ReadAllText(luaFile), enviroment);
        }

        public static LuaValue Interpreter(string luaCode)
        {
            return Interpreter(luaCode, CreateGlobalEnviroment());
        }

        public static LuaValue Interpreter(string luaCode, LuaTable enviroment)
        {
            Chunk chunk = Parse(luaCode);
            chunk.Enviroment = enviroment;
            return chunk.Execute();
        }

        private static Parser parser = new Parser();

        public static Chunk Parse(string luaCode)
        {
            bool success;
            Chunk chunk = parser.ParseChunk(new TextInput(luaCode), out success);
            if (success)
            {
                return chunk;
            }
            else
            {
                throw new ArgumentException("Code has syntax errors:\r\n" + parser.GetErrorMessages());
            }
        }

        public static LuaTable CreateGlobalEnviroment()
        {
            LuaTable global = new LuaTable();

            BaseLib.RegisterFunctions(global);
            StringLib.RegisterModule(global);
            TableLib.RegisterModule(global);
            IOLib.RegisterModule(global);
            FileLib.RegisterModule(global);
            MathLib.RegisterModule(global);
            OSLib.RegisterModule(global);

            global.SetNameValue("_G", global);

            return global;
        }
    }
}
