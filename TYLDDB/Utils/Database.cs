using System.Collections.Generic;
using System.Text.RegularExpressions;
using TYLDDB.Basic;

namespace TYLDDB.Utils
{
    internal class Database
    {
#pragma warning disable CA1822 // 忽略静态提示
        public List<string> GetDatabaseList(string fileContent)
        {
            // 正则表达式：排除 internaldb 和 distributeddb，同时匹配符合规则的数据库名称
            string pattern = @"(?!internaldb$|distributeddb$)[a-zA-Z0-9_]+(?=::\{)";

            // 使用正则表达式提取所有数据库名称
            MatchCollection matches = Regex.Matches(fileContent, pattern);

            // 创建一个List来存储数据库名称
            List<string> databaseNames = new List<string>();

            // 遍历匹配结果并加入到List中
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取数据库名称
                    string databaseName = match.Value;
                    databaseNames.Add(databaseName);
                }
            }

            return databaseNames;
        }

        public string GetDatabaseContent(string content, string databaseName)
        {
            // 更新的正则表达式，用于匹配指定数据库的内容，包括处理大括号的嵌套结构
            string pattern = $@"{databaseName}\s*::\s*\{{([^{{}}]*(?:{{[^{{}}]*}}[^{{}}]*)*)\}};";

            // 执行正则匹配
            Match match = Regex.Match(content, pattern, RegexOptions.Singleline);

            if (match.Success)
            {
                // 获取匹配的数据库内容
                string databaseContent = match.Groups[1].Value;

                // 替换多余的空格和换行符，保持引号内的空格
                string cleanedContent = Regex.Replace(databaseContent, @"\s*(?=\S)", "");

                return databaseContent;
            }
            else
            {
                // 如果未找到匹配项，抛出自定义异常
                throw new GetDatabaseContentErrorException($"未找到数据库 '{databaseName}' 的内容。");
            }
        }
    }
}
