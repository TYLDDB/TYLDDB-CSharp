using System.Text.RegularExpressions;
using TYLDDB.Basic;

namespace TYLDDB.Utils
{
    internal class Database
    {
#pragma warning disable CA1822 // 忽略静态提示
        public string LoadDatabase(string db, string fileContent)
        {
            // 使用正则表达式查找最外层的数据库
            string pattern = $@"(?<=^|;)\s*{db}::\{{(.*?)\}};\s*(?=;|$)";
            Match match = Regex.Match(fileContent, pattern, RegexOptions.Singleline | RegexOptions.Multiline);

            if (match.Success)
            {
                // 如果找到数据库，则赋值
                return db;
            }
            else
            {
                throw new DatabaseNotFoundException($"数据库 '{db}' 未找到。");
            }
        }

        public string GetDatabaseContent(string content, string databaseName)
        {
            // 正则表达式模式，匹配指定数据库的内容（支持多行）
            string pattern = $@"{databaseName}::\s*{{(.*?)}}";
            Match match = Regex.Match(content, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (match.Success)
            {
                // 返回捕获组的内容，去掉数据库名称和外部大括号
                return match.Groups[1].Value.Trim();
            }

            // 如果未找到匹配项，抛出自定义异常
            throw new GetDatabaseContentErrorException($"未找到数据库 '{databaseName}' 的内容。");
        }
    }
}
