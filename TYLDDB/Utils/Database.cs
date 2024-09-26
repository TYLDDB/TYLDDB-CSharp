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
    }
}
