using System;
using System.Collections.Generic;
using System.Text;

namespace TYLDDB.Utils.DatabaseProcess
{
    internal class Database_V2 : IDatabase
    {
        public string GetDatabaseContent(string input, string databaseName)
        {
            // 定位到指定数据库的开始部分
            string startMarker = $"{databaseName}::{{";
            string endMarker = "};";

            // 找到数据库内容的起始位置和结束位置
            int startIndex = input.IndexOf(startMarker, StringComparison.Ordinal);
            if (startIndex == -1)
            {
                return $"数据库 {databaseName} 未找到。";
            }

            // 在找到的起始位置之后，定位结束标记
            int endIndex = input.IndexOf(endMarker, startIndex, StringComparison.Ordinal);
            if (endIndex == -1)
            {
                return $"数据库 {databaseName} 的结束标记未找到。";
            }

            // 提取数据库内容
            int contentStartIndex = startIndex + startMarker.Length;
            string dbContent = input[contentStartIndex..endIndex].Trim();

            // 使用 StringBuilder 高效地处理每行开头的空格
            var result = new StringBuilder();

            // 将数据库内容按行分割
            string[] lines = dbContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                // 使用 TrimStart() 去掉每行开头的空格
                string trimmedLine = line.TrimStart();

                // 添加处理后的行到结果中
                result.AppendLine(trimmedLine);
            }

            // 返回处理后的字符串
            return result.ToString().TrimEnd(); // 移除结尾的多余换行符
        }

        public List<string> GetDatabaseList(string fileContent) => throw new NotImplementedException();
    }
}
