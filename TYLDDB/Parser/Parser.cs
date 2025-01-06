using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TYLDDB.Parser
{
    /// <summary>
    /// Data type parser.<br />
    /// 数据类型解析器。
    /// </summary>
    public class DataParser
    {
        // 定义正则表达式来匹配键值对（类型::"key"="value"）
        private readonly static string pattern = @"(\w+)\s*::\s*""([^""]+)""\s*=\s*""([^""]+)"";";

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, string> ParseString(string content)
        {
            string type = "string";

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, string>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;
                string matchedKey = match.Groups[2].Value;
                string matchedValue = match.Groups[3].Value;

                // 如果提供的类型与键的类型匹配，加入到结果字典
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 将匹配的键值对添加到字典中
                    result[matchedKey] = matchedValue;
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, int> ParseInt(string content)
        {
            string type = "int"; // 目标类型是 int

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, int>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;  // 这是类型，比如 int, short, long
                string matchedKey = match.Groups[2].Value;   // 这是键，比如 int_value, string_value
                string matchedValue = match.Groups[3].Value; // 这是值的字符串部分，比如 "123", "32767"

                // 如果提供的类型与键的类型匹配
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 尝试将值转换为 int 类型
                    if (int.TryParse(matchedValue, out int intValue))
                    {
                        // 将匹配的键值对添加到字典中
                        result[matchedKey] = intValue;
                    }
                    else
                    {
                        throw new Exception($"无法将值 \"{matchedValue}\" 转换为 int。");
                    }
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, short> ParseShort(string content)
        {
            string type = "short"; // 目标类型是 short

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, short>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;
                string matchedKey = match.Groups[2].Value;
                string matchedValue = match.Groups[3].Value;

                // 如果提供的类型与键的类型匹配
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 尝试将值转换为 short 类型
                    if (short.TryParse(matchedValue, out short shortValue))
                    {
                        // 将匹配的键值对添加到字典中
                        result[matchedKey] = shortValue;
                    }
                    else
                    {
                        throw new Exception($"无法将值 \"{matchedValue}\" 转换为 short。");
                    }
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, long> ParseLong(string content)
        {
            string type = "long"; // 目标类型是 long

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, long>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;
                string matchedKey = match.Groups[2].Value;
                string matchedValue = match.Groups[3].Value;

                // 如果提供的类型与键的类型匹配
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 尝试将值转换为 long 类型
                    if (long.TryParse(matchedValue, out long longValue))
                    {
                        // 将匹配的键值对添加到字典中
                        result[matchedKey] = longValue;
                    }
                    else
                    {
                        throw new Exception($"无法将值 \"{matchedValue}\" 转换为 long。");
                    }
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, float> ParseFloat(string content)
        {
            string type = "float"; // 目标类型是 float

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, float>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;
                string matchedKey = match.Groups[2].Value;
                string matchedValue = match.Groups[3].Value;

                // 如果提供的类型与键的类型匹配
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 尝试将值转换为 short 类型
                    if (float.TryParse(matchedValue, out float floatValue))
                    {
                        // 将匹配的键值对添加到字典中
                        result[matchedKey] = floatValue;
                    }
                    else
                    {
                        throw new Exception($"无法将值 \"{matchedValue}\" 转换为 float。");
                    }
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, double> ParseDouble(string content)
        {
            string type = "double"; // 目标类型是 double

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, double>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;
                string matchedKey = match.Groups[2].Value;
                string matchedValue = match.Groups[3].Value;

                // 如果提供的类型与键的类型匹配
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 尝试将值转换为 short 类型
                    if (double.TryParse(matchedValue, out double Value))
                    {
                        // 将匹配的键值对添加到字典中
                        result[matchedKey] = Value;
                    }
                    else
                    {
                        throw new Exception($"无法将值 \"{matchedValue}\" 转换为 double。");
                    }
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, bool> ParseBoolean(string content)
        {
            string type = "boolean"; // 目标类型是 float

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, bool>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;
                string matchedKey = match.Groups[2].Value;
                string matchedValue = match.Groups[3].Value;

                // 如果提供的类型与键的类型匹配
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 尝试将值转换为 short 类型
                    if (bool.TryParse(matchedValue, out bool Value))
                    {
                        // 将匹配的键值对添加到字典中
                        result[matchedKey] = Value;
                    }
                    else
                    {
                        throw new Exception($"无法将值 \"{matchedValue}\" 转换为 bool。");
                    }
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, char> ParseChar(string content)
        {
            string type = "char"; // 目标类型是 float

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, char>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;
                string matchedKey = match.Groups[2].Value;
                string matchedValue = match.Groups[3].Value;

                // 如果提供的类型与键的类型匹配
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 尝试将值转换为 short 类型
                    if (char.TryParse(matchedValue, out char Value))
                    {
                        // 将匹配的键值对添加到字典中
                        result[matchedKey] = Value;
                    }
                    else
                    {
                        throw new Exception($"无法将值 \"{matchedValue}\" 转换为 char。");
                    }
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, decimal> ParseDecimal(string content)
        {
            string type = "decimal"; // 目标类型是 float

            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, decimal>();

            // 匹配整个内容中的所有键值对
            var matches = Regex.Matches(content, pattern);

            // 遍历所有的匹配项
            foreach (Match match in matches)
            {
                string matchedType = match.Groups[1].Value;
                string matchedKey = match.Groups[2].Value;
                string matchedValue = match.Groups[3].Value;

                // 如果提供的类型与键的类型匹配
                if (string.Equals(type, matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    // 尝试将值转换为 short 类型
                    if (decimal.TryParse(matchedValue, out decimal Value))
                    {
                        // 将匹配的键值对添加到字典中
                        result[matchedKey] = Value;
                    }
                    else
                    {
                        throw new Exception($"无法将值 \"{matchedValue}\" 转换为 decimal。");
                    }
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }
    }
}
