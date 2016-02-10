using System;
using System.Collections.Generic;

namespace Language.Lua
{
    public class LuaTable : LuaValue
    {
        private List<LuaValue> list;

        private Dictionary<LuaValue, LuaValue> dict;

        public LuaTable()
        {
        }

        public LuaTable(LuaTable parent)
        {
            MetaTable = new LuaTable();
            MetaTable.SetNameValue("__index", parent);
            MetaTable.SetNameValue("__newindex", parent);
        }

        public LuaTable MetaTable { get; set; }

        public override object Value
        {
            get { return this; }
        }

        public override string GetTypeCode()
        {
            return "table";
        }

        public int Length
        {
            get
            {
                if (list == null)
                {
                    return 0;
                }
                else
                {
                    return list.Count;
                }
            }
        }

        public int Count
        {
            get
            {
                if (dict == null)
                {
                    return 0;
                }
                else
                {
                    return dict.Count;
                }
            }
        }

        public override string ToString()
        {
            if (MetaTable != null)
            {
                LuaFunction function = MetaTable.GetValue("__tostring") as LuaFunction;
                if (function != null)
                {
                    return function.Invoke(new LuaValue[] { this }).ToString();
                }
            }

            return "Table " + GetHashCode();
        }

        public IEnumerable<LuaValue> ListValues
        {
            get { return list; }
        }

        public IEnumerable<LuaValue> Keys
        {
            get
            {
                if (Length > 0)
                {
                    for (int index = 1; index <= list.Count; index++)
                    {
                        yield return new LuaNumber(index);
                    }
                }

                if (Count > 0)
                {
                    foreach (LuaValue key in dict.Keys)
                    {
                        yield return key;
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<LuaValue, LuaValue>> KeyValuePairs
        {
            get { return dict; }
        }

        public bool ContainsKey(LuaValue key)
        {
            if (dict != null)
            {
                if (dict.ContainsKey(key))
                {
                    return true;
                }
            }

            if (list != null)
            {
                LuaNumber index = key as LuaNumber;
                if (index != null && index.Number == (int)index.Number)
                {
                    return index.Number >= 1 && index.Number <= list.Count;
                }
            }

            return false;
        }

        public void AddValue(LuaValue value)
        {
            if (list == null)
            {
                list = new List<LuaValue>();
            }

            list.Add(value);
        }

        public void InsertValue(int index, LuaValue value)
        {
            if (index > 0 && index <= Length + 1)
            {
                list.Insert(index - 1, value);
            }
            else
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }

        public bool Remove(LuaValue item)
        {
            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index - 1);
        }

        public void Sort()
        {
            list.Sort((a, b) =>
            {
                LuaNumber n = a as LuaNumber;
                LuaNumber m = b as LuaNumber;
                if (n != null && m != null)
                {
                    return n.Number.CompareTo(m.Number);
                }

                LuaString s = a as LuaString;
                LuaString t = b as LuaString;
                if (s != null && t != null)
                {
                    return s.Text.CompareTo(t.Text);
                }

                return 0;
            });
        }

        public void Sort(LuaFunction compare)
        {
            list.Sort((a, b) =>
                {
                    LuaValue result = compare.Invoke(new LuaValue[] { a, b });
                    LuaBoolean boolValue = result as LuaBoolean;
                    if (boolValue != null && boolValue.BoolValue == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                });
        }

        public LuaValue GetValue(int index)
        {
            if (index > 0 && index <= Length)
            {
                return list[index - 1];
            }

            return LuaNil.Nil;
        }

        public LuaValue GetValue(string name)
        {
            LuaValue key = GetKey(name);

            if (key == LuaNil.Nil)
            {
                if (MetaTable != null)
                {
                    return GetValueFromMetaTable(name);
                }

                return LuaNil.Nil;
            }
            else
            {
                return dict[key];
            }
        }

        public LuaValue GetKey(string key)
        {
            if (dict == null) return LuaNil.Nil;

            foreach (LuaValue value in dict.Keys)
            {
                LuaString str = value as LuaString;

                if (str != null && string.Equals(str.Text, key, StringComparison.Ordinal))
                {
                    return value;
                }
            }

            return LuaNil.Nil;
        }

        public void SetNameValue(string name, LuaValue value)
        {
            if (value == LuaNil.Nil)
            {
                RemoveKey(name);
            }
            else
            {
                RawSetValue(name, value);
            }
        }

        private void RemoveKey(string name)
        {
            LuaValue key = GetKey(name);

            if (key != LuaNil.Nil)
            {
                dict.Remove(key);
            }
        }

        public void SetKeyValue(LuaValue key, LuaValue value)
        {
            LuaNumber number = key as LuaNumber;

            if (number != null && number.Number == (int)number.Number)
            {
                int index = (int)number.Number;

                if (index == Length + 1)
                {
                    AddValue(value);
                    return;
                }

                if (index > 0 && index <= Length)
                {
                    list[index - 1] = value;
                    return;
                }
            }

            if (value == LuaNil.Nil)
            {
                RemoveKey(key);
                return;
            }

            if (dict == null)
            {
                dict = new Dictionary<LuaValue, LuaValue>();
            }

            dict[key] = value;
        }

        private void RemoveKey(LuaValue key)
        {
            if (key != LuaNil.Nil && dict != null && dict.ContainsKey(key))
            {
                dict.Remove(key);
            }
        }

        public LuaValue GetValue(LuaValue key)
        {
            if (key == LuaNil.Nil)
            {
                return LuaNil.Nil;
            }
            else
            {
                LuaNumber number = key as LuaNumber;

                if (number != null && number.Number == (int)number.Number)
                {
                    int index = (int)number.Number;

                    if (index > 0 && index <= Length)
                    {
                        return list[index - 1];
                    }
                }

                if (dict != null && dict.ContainsKey(key))
                {
                    return dict[key];
                }
                else if (MetaTable != null)
                {
                    return GetValueFromMetaTable(key);
                }

                return LuaNil.Nil;
            }
        }

        private LuaValue GetValueFromMetaTable(string name)
        {
            LuaValue indexer = MetaTable.GetValue("__index");

            LuaTable table = indexer as LuaTable;

            if (table != null)
            {
                return table.GetValue(name);
            }

            LuaFunction function = indexer as LuaFunction;

            if (function != null)
            {
                return function.Function.Invoke(new LuaValue[] { new LuaString(name) });
            }

            return LuaNil.Nil;
        }

        private LuaValue GetValueFromMetaTable(LuaValue key)
        {
            LuaValue indexer = MetaTable.GetValue("__index");

            LuaTable table = indexer as LuaTable;

            if (table != null)
            {
                return table.GetValue(key);
            }

            LuaFunction function = indexer as LuaFunction;

            if (function != null)
            {
                return function.Function.Invoke(new LuaValue[] { key });
            }

            return LuaNil.Nil;
        }

        public LuaFunction Register(string name, LuaFunc function)
        {
            LuaFunction luaFunc = new LuaFunction(function);
            SetNameValue(name, luaFunc);
            return luaFunc;
        }

        public LuaValue RawGetValue(LuaValue key)
        {
            if (dict != null && dict.ContainsKey(key))
            {
                return dict[key];
            }

            return LuaNil.Nil;
        }

        public void RawSetValue(string name, LuaValue value)
        {
            LuaValue key = GetKey(name);

            if (key == LuaNil.Nil)
            {
                key = new LuaString(name);
            }

            if (dict == null)
            {
                dict = new Dictionary<LuaValue, LuaValue>();
            }

            dict[key] = value;
        }
    }
}