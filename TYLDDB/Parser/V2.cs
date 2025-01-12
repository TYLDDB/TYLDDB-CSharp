using System.Collections.Generic;
using System;

namespace TYLDDB.Parser
{
    /// <summary>
    /// Data type parser.<br />
    /// 数据类型解析器。
    /// </summary>
    public class DataParser_V2
    {
        // TODO: 需要处理if中的continue，if (typeEndIndex == -1 || equalSignIndex == -1)判断不建议直接continue！可以尝试抛出一个错误，在外部做错误处理！

        // TODO：添加用于实例化的方法

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs. This method is resolved by calling other methods, such as <see cref="ParseString(string)"/>.<br />
        /// 解析给定的内容并查找所有匹配的键值对。该方法为调用其它方法进行解析，如<see cref="ParseString(string)"/>。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <param name="type">Data types thet need to be resolved.<br />需要解析的数据类型。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, T> Parse<T>(string content, DataParser_Type type)
        {
            switch (type)
            {
                case DataParser_Type.String:
                    return ParseString(content) as Dictionary<string, T>;
                case DataParser_Type.Int:
                    return ParseInt(content) as Dictionary<string, T>;
                case DataParser_Type.Short:
                    return ParseShort(content) as Dictionary<string, T>;
                case DataParser_Type.Long:
                    return ParseLong(content) as Dictionary<string, T>;
                case DataParser_Type.Float:
                    return ParseFloat(content) as Dictionary<string, T>;
                case DataParser_Type.Double:
                    return ParseDouble(content) as Dictionary<string, T>;
                case DataParser_Type.Bool:
                    return ParseBoolean(content) as Dictionary<string, T>;
                case DataParser_Type.Char:
                    return ParseChar(content) as Dictionary<string, T>;
                case DataParser_Type.Decimal:
                    return ParseDecimal(content) as Dictionary<string, T>;
                default:
                    var result = new Dictionary<string, T>();
                    return result;
            }
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs. This method uses built-in integrated parsing rather than invoking other method parsing.<br />
        /// 解析给定的内容并查找所有匹配的键值对。该方法采用内置集成式解析而非调用其它方法解析。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <param name="type">Data types thet need to be resolved.<br />需要解析的数据类型。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, TValue> IntegratedParse<TValue>(string content, DataParser_Type type)
        {
            var result = new Dictionary<string, TValue>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                switch (type)
                {
                    case DataParser_Type.String:
                        {
                            var resultTemp = new Dictionary<string, string>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');

                            // 如果类型匹配，添加到字典
                            if (string.Equals("string", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = matchedValue;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    case DataParser_Type.Int:
                        {
                            var resultTemp = new Dictionary<string, int>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                            int value = int.Parse(matchedValue);

                            // 如果类型匹配，添加到字典
                            if (string.Equals("int", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = value;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    case DataParser_Type.Short:
                        {
                            var resultTemp = new Dictionary<string, short>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                            short value = short.Parse(matchedValue);

                            // 如果类型匹配，添加到字典
                            if (string.Equals("short", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = value;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    case DataParser_Type.Long:
                        {
                            var resultTemp = new Dictionary<string, long>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                            long value = long.Parse(matchedValue);

                            // 如果类型匹配，添加到字典
                            if (string.Equals("long", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = value;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    case DataParser_Type.Float:
                        {
                            var resultTemp = new Dictionary<string, float>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                            float value = float.Parse(matchedValue);

                            // 如果类型匹配，添加到字典
                            if (string.Equals("float", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = value;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    case DataParser_Type.Double:
                        {
                            var resultTemp = new Dictionary<string, double>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                            double value = double.Parse(matchedValue);

                            // 如果类型匹配，添加到字典
                            if (string.Equals("double", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = value;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    case DataParser_Type.Bool:
                        {
                            var resultTemp = new Dictionary<string, bool>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                            bool value = bool.Parse(matchedValue);

                            // 如果类型匹配，添加到字典
                            if (string.Equals("boolean", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = value;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    case DataParser_Type.Char:
                        {
                            var resultTemp = new Dictionary<string, char>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                            char value = char.Parse(matchedValue);

                            // 如果类型匹配，添加到字典
                            if (string.Equals("char", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = value;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    case DataParser_Type.Decimal:
                        {
                            var resultTemp = new Dictionary<string, decimal>();

                            string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                            decimal value = decimal.Parse(matchedValue);

                            // 如果类型匹配，添加到字典
                            if (string.Equals("decimal", matchedType, StringComparison.OrdinalIgnoreCase))
                            {
                                resultTemp[matchedKey] = value;
                                result = resultTemp as Dictionary<string, TValue>;
                            }

                            break;
                        }

                    default:
                        return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Parses the given content and finds all matching key-value pairs.<br />
        /// 解析给定的内容并查找所有匹配的键值对。
        /// </summary>
        /// <param name="content">The content string to be parsed, containing key-value pairs.<br />要解析的内容字符串，包含键值对。</param>
        /// <returns>A dictionary containing all matched key-value pairs. If no matching key-value pair is found, an empty dictionary is returned.<br />包含所有匹配的键值对的字典。如果没有找到匹配的键值对，返回空字典。</returns>
        public static Dictionary<string, string> ParseString(string content)
        {
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, string>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');

                // 如果类型匹配，添加到字典
                if (string.Equals("string", matchedType, StringComparison.OrdinalIgnoreCase))
                {
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
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, int>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                int value = int.Parse(matchedValue);

                // 如果类型匹配，添加到字典
                if (string.Equals("int", matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    result[matchedKey] = value;
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
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, short>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                short value = short.Parse(matchedValue);

                // 如果类型匹配，添加到字典
                if (string.Equals("short", matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    result[matchedKey] = value;
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
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, long>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                long value = long.Parse(matchedValue);

                // 如果类型匹配，添加到字典
                if (string.Equals("long", matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    result[matchedKey] = value;
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
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, float>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                float value = float.Parse(matchedValue);

                // 如果类型匹配，添加到字典
                if (string.Equals("float", matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    result[matchedKey] = value;
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
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, double>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                double value = double.Parse(matchedValue);

                // 如果类型匹配，添加到字典
                if (string.Equals("double", matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    result[matchedKey] = value;
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
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, bool>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                bool value = bool.Parse(matchedValue);

                // 如果类型匹配，添加到字典
                if (string.Equals("boolean", matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    result[matchedKey] = value;
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
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, char>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                char value = char.Parse(matchedValue);

                // 如果类型匹配，添加到字典
                if (string.Equals("char", matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    result[matchedKey] = value;
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
            // 创建一个字典来存储找到的所有键值对
            var result = new Dictionary<string, decimal>();

            // 分割内容为行，逐行分析
            var lines = content.Split(';');

            foreach (var line in lines)
            {
                // 去除空格并忽略空行
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                // 查找 :: 和 = 的位置
                int typeEndIndex = trimmedLine.IndexOf("::");
                int equalSignIndex = trimmedLine.IndexOf("=");

                if (typeEndIndex == -1 || equalSignIndex == -1)
                {
                    continue;
                }
                // 提取类型、键和值
                string matchedType = trimmedLine.Substring(0, typeEndIndex).Trim();
                string matchedKey = trimmedLine.Substring(typeEndIndex + 2, equalSignIndex - typeEndIndex - 2).Trim().Trim('"');
                string matchedValue = trimmedLine.Substring(equalSignIndex + 1).Trim().Trim('"');
                decimal value = decimal.Parse(matchedValue);

                // 如果类型匹配，添加到字典
                if (string.Equals("decimal", matchedType, StringComparison.OrdinalIgnoreCase))
                {
                    result[matchedKey] = value;
                }
            }

            // 返回结果字典，若没有匹配项则返回空字典
            return result;
        }
    }

    /// <summary>
    /// Parse type Settings for data type parser V2.<br />
    /// 数据类型解析器V2的解析类型设置。
    /// </summary>
    public enum DataParser_Type
    {
        /// <summary>
        /// String.<br />
        /// 字符串。
        /// </summary>
        String,
        /// <summary>
        /// Integer.<br />
        /// 整数类型。
        /// </summary>
        Int,
        /// <summary>
        /// Short.<br />
        /// 短整数类型。
        /// </summary>
        Short,
        /// <summary>
        /// Long.<br />
        /// 长整数类型。
        /// </summary>
        Long,
        /// <summary>
        /// Float.<br />
        /// 浮点数。
        /// </summary>
        Float,
        /// <summary>
        /// Double.<br />
        /// 双精度浮点数。
        /// </summary>
        Double,
        /// <summary>
        /// Boolean.<br />
        /// 布尔值。
        /// </summary>
        Bool,
        /// <summary>
        /// Char.
        /// </summary>
        Char,
        /// <summary>
        /// Decimal.<br />
        /// 高精度十进制数。
        /// </summary>
        Decimal,
    }
}
