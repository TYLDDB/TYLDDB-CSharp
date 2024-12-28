using System;
using System.Collections.Generic;
using TYLDDB.Basic;
using TYLDDB.Utils;
using TYLDDB.Utils.FastCache;

namespace TYLDDB
{
    /// <summary>
    /// The core class of the database.<br />
    /// 数据库的核心类。
    /// </summary>
    public class LDDB
    {
        /// <summary>
        /// Instantiate the LDDB class<br />
        /// 实例化LDDB类
        /// </summary>
        public LDDB()
        {
            // NOTHING HERE!
        }

        ///////////////////////////////////////////////////// 私有字段
        private string _filePath;  // 存储文件路径
        private string _fileContent; // 存储文件内容
        private string _database; // 存储正在访问的数据库
        private string _databaseContent; // 存储数据库内容
        private bool _isRead = false; // 是否已调用读取文件
        private event Action OnFileReadComplete;
        private Database database = new Database();
        private Cache cdCache = new ConcurrentDictionary();
        private Cache stlCache = new SemaphoreThreadLock();

        ///////////////////////////////////////////////////// 公开字段
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
        /// <summary>
        /// Names of all databases in the current file<br />
        /// 当前文件内所有数据库的名称
        /// </summary>
        public List<string> AllDatabaseName;

        /// <summary>
        /// 验证文件路径是否为null或空
        /// </summary>
        /// <param name="path">路径</param>
        /// <exception cref="FilePathIsNullOrWhiteSpace"></exception>
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
            _fileContent = Reader.ReadFile(FilePath);
            _isRead = true;
        }

        /// <summary>
        /// Set the database to load<br/>
        /// 设置要加载的数据库
        /// </summary>
        /// <param name="db">name of the database<br/>数据库名称</param>
        public void LoadDatabase(string db)
        {
            switch (_isRead)
            {
                case true:
                    _databaseContent = database.GetDatabaseContent(_fileContent, db);
                    break;
                default:
                    ReadingFile();
                    _databaseContent = database.GetDatabaseContent(_fileContent, db);
                    break;
            }
        }

        /// <summary>
        /// Gets the contents of the database being loaded<br/>
        /// 获取正在加载的数据库内容
        /// </summary>
        public string GetLoadingDatabaseContent() => _databaseContent;

        /// <summary>
        /// Read the names of all databases<br />
        /// 读取全部数据库的名称
        /// </summary>
        public void ReadAllDatabaseName() => AllDatabaseName = database.GetDatabaseList(_fileContent);
    }
}
