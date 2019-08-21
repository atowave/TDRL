using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ied.Extentions
{
    static public class INIParser
    {
        public static Dictionary<string, Dictionary<string, ValueType>> DefaultValues = new Dictionary<string, Dictionary<string, ValueType>>();
        static Dictionary<string, Dictionary<string, ValueType>> Array = new Dictionary<string, Dictionary<string, ValueType>>()
        {
            {"b", new Dictionary<string, ValueType>() {
                    {"true", true},
                    {"false", false }
                }
            }
        };
        static string array;
        static Dictionary<string, ValueType> currentDic;
        static public Dictionary<string, Dictionary<string, ValueType>> Load(string file)
        {
            Dictionary<string, Dictionary<string, ValueType>> ini = new Dictionary<string, Dictionary<string, ValueType>>();
            foreach(string line in File.ReadAllLines(file))
            {
                if (line != "")
                {
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        array = line.Substring(1, line.Length - 2);
                        ini.Add(array, new Dictionary<string, ValueType>());
                        currentDic = ini[array];
                    } else
                    {
                        string[] value = line.Split('=');
                        if (value[0].StartsWith("b"))
                        {
                            currentDic.Add(value[0].Substring(1), Boolean.Parse(value[1]));
                        }
                        if (value[0].StartsWith("i"))
                        {
                            currentDic.Add(value[0].Substring(1), Int32.Parse(value[1]));
                        } 
                        if (value[0].StartsWith("d"))
                        {
                            currentDic.Add(value[0].Substring(1), Double.Parse(value[1]));
                        }
                    }
                }
            }
            return ini;
        }

        static Dictionary<string, string> encode_keys = new Dictionary<string, string>()
        {
            {"System.Boolean", "b" },
            {"System.Double", "d" },
            {"System.Int32", "i" },
            {"System.String", "s" }
        };
        static public string Encode(Dictionary<string, Dictionary<string, ValueType>> array)
        {
            string txt = "";
            foreach (KeyValuePair<string, Dictionary<string, ValueType>> pair in array)
            {
                txt += "["+pair.Key+"]"+ Environment.NewLine;
                foreach (KeyValuePair<string, ValueType> pair1 in pair.Value)
                {
                    txt += encode_keys[pair1.Value.GetType().ToString()] + pair1.Key + "="+pair1.Value.ToString() + Environment.NewLine;
                }
                txt += Environment.NewLine;
            }
            return txt;
        }
    }
}
