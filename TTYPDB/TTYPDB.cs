using System.Text.RegularExpressions;
using TTYPDB.Basic;
using TTYPDB.Utils;

namespace TTYPDB
{
    public class TTYPDB
    {
        private string _filePath;  // 私有字段存储文件路径
        private string _fileContent; // 私有字段存储文件内容
        private string _database;

        /// <summary>
        /// Set the path where you want to read the file<br/>
        /// 设置希望读取文件的路径
        /// </summary>
        public string FilePath
        {
            get => _filePath; // 获取文件路径
            set
            {
                ValidateFilePath(value); // 在设置值之前进行验证
                _filePath = value; // 只有通过验证后才设置值
            }
        }

        // 验证文件路径的方法
        private static void ValidateFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new FilePathIsNullOrWhiteSpace("文件路径不能为 null 或空白");
            }
        }

        /// <summary>
        /// Read the contents from the file<br/>
        /// 从文件中读取内容
        /// </summary>
        public void ReadingFile()
        {
            _fileContent = ReadFile.ReadTtypdbFile(FilePath);
        }

        /// <summary>
        /// Set the database to load<br/>
        /// 设置要加载的数据库
        /// </summary>
        /// <param name="db">name of the database<br/>数据库名称</param>
        public void LoadDatabase(string db)
        {
            // 使用正则表达式查找最外层的数据库
            string pattern = $@"(?<=^|;)\s*{db}::\{{(.*?)\}};\s*(?=;|$)";
            Match match = Regex.Match(_fileContent, pattern, RegexOptions.Singleline | RegexOptions.Multiline);

            if (match.Success)
            {
                // 如果找到数据库，则赋值
                _database = db;
            }
            else
            {
                throw new DatabaseNotFoundException($"数据库 '{db}' 未找到。");
            }
        }
    }
}
