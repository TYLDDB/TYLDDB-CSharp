using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TYLDDB.Parser
{
    /// <summary>
    /// 解析器
    /// </summary>
    public class Parser
    {
        private string _dbContent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContent">Database content<br />数据库内容</param>
        public Parser(string dbContent)
        {
            _dbContent = dbContent;
        }

        // 正则表达式，用于匹配不同类型的条目
        private readonly string pattern = @"(\w+)::""([^""]+)""=""([^""]+)"";";

        // 解析输入字符串，提取不同类型的条目
        private Dictionary<string, Type> Parse(string input)
        {
            // 字典存储最终的解析结果
            Dictionary<string, Type> parsedData = new Dictionary<string, Type>();

            // 使用正则表达式查找所有匹配项
            MatchCollection matches = Regex.Matches(input, pattern);

            // 遍历所有匹配项，添加到字典
            foreach (Match match in matches)
            {
                string typeName = match.Groups[1].Value;  // 类型名称（如string, int）
                string varName = match.Groups[2].Value;   // 变量名称（如str_name）
                string value = match.Groups[3].Value;     // 变量值（如name1）

                Type type = GetTypeFromString(typeName);  // 获取对应的类型

                // 将名称和类型添加到字典
                if (type != null)
                {
                    parsedData.Add(varName, type);
                }
            }

            return parsedData;
        }

        // 根据类型名称返回对应的 .NET 类型
        private static Type GetTypeFromString(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "string":
                    return typeof(string);
                case "int":
                    return typeof(int);
                case "short":
                    return typeof(short);
                case "long":
                    return typeof(long);
                case "float":
                    return typeof(float);
                case "double":
                    return typeof(double);
                case "boolean":
                    return typeof(bool);
                case "char":
                    return typeof(char);
                case "decimal":
                    return typeof(decimal);
                default:
                    return typeof(string); // 如果未识别的类型，返回 string
            }
        }

        public void Strings()
        {
        }

        public void Integers()
        {
        }
    }
}
