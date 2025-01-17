using System.Collections.Generic;
using System.Linq;
using TYLDDB.Basic.Exception;
using TYLDDB.Utils;

namespace TYLDDB
{
    /// <summary>
    /// The core class of the database.<br />
    /// 数据库的核心类。
    /// </summary>
    public partial class LDDB
    {
        #region 公开字段
        /// <summary>
        /// Set the path where you want to read the file<br/>
        /// 设置希望读取文件的路径
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filePath; // 获取文件路径
            }
            set
            {
                if (ValidateFilePath(value)) // 在设置值之前进行验证
                {
                    _filePath = value;
                }
            }
        }

        /// <summary>
        /// Names of all databases in the current file<br />
        /// 当前文件内所有数据库的名称
        /// </summary>
        public List<string> AllDatabaseName;
        #endregion

        #region 方法

        /// <summary>
        /// 验证文件路径是否为null或空
        /// </summary>
        /// <param name="path">路径</param>
        /// <exception cref="FilePathIsNullOrWhiteSpace"></exception>
        /// <returns>If <c>true</c>, it can be used, if <c>false</c>, it cannot be used.<br />如果为<c>true</c>则可以使用，若为<c>false</c>则不可使用。</returns>
        private static bool ValidateFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
                throw new FilePathIsNullOrWhiteSpace("The file path cannot be null or blank.");
            }
            return true;
        }

        /// <summary>
        /// Read the contents from the file<br/>
        /// 从文件中读取内容
        /// </summary>
        public void ReadingFile() => _fileContent = Reader.ReadFile(FilePath);

        /// <summary>
        /// Set the database to load<br/>
        /// 设置要加载的数据库
        /// </summary>
        /// <param name="db">name of the database<br/>数据库名称</param>
        public void LoadDatabase(string db)
        {
            ReadingFile();
            _databaseContent = database_v1.GetDatabaseContent(_fileContent, db);
        }

        /// <summary>
        /// Set the database to load<br/>
        /// 设置要加载的数据库
        /// </summary>
        /// <param name="db">name of the database<br/>数据库名称</param>
        public void LoadDatabase_V2(string db)
        {
            ReadingFile();
            _databaseContent = database_v2.GetDatabaseContent(_fileContent, db);
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
        public void ReadAllDatabaseName() => AllDatabaseName = database_v1.GetDatabaseList(_fileContent);

        /// <summary>
        /// Finds the value associated with a given key from multiple types of concurrent dictionaries and returns all non-empty values as an array of strings.<br />
        /// 从多个类型的并发词典中查找与给定键相关的值，并将所有非空的值以字符串数组的形式返回。
        /// </summary>
        /// <param name="key">The key used to find. Method looks up the corresponding value from multiple dictionaries based on this key.<br />用于查找的键。方法将根据这个键从多个字典中查找对应的值。</param>
        /// <returns>Returns an array of strings containing all non-empty values associated with the given key. If no matching value is found, an empty array is returned.<br />返回一个字符串数组，其中包含所有非空的、与给定键相关的值。如果没有找到匹配的值，则返回空数组。</returns>
        public string[] AllTypeSearchFromConcurrentDictionary(string key)
        {
            // 安全地从字典获取并转换为字符串（对于可能为 null 的值使用 ?.ToString()）
            string cdString = cdStringDictionary.GetByKey(key)?.ToString();
            string cdShort = cdShortDictionary.GetByKey(key)?.ToString();
            string cdLong = cdLongDictionary.GetByKey(key)?.ToString();
            string cdInt = cdIntegerDictionary.GetByKey(key)?.ToString();
            string cdFloat = cdFloatDictionary.GetByKey(key)?.ToString();
            string cdDouble = cdDoubleDictionary.GetByKey(key)?.ToString();
            string cdDecimal = cdDecimalDictionary.GetByKey(key)?.ToString();
            string cdChar = cdCharDictionary.GetByKey(key)?.ToString();
            string cdBool = cdBooleanDictionary.GetByKey(key)?.ToString();

            // 使用 LINQ 来过滤非 null 且非空的字符串，并将其转换为数组
            string[] resultArray = new[] { cdString, cdShort, cdLong, cdInt, cdFloat, cdDouble, cdDecimal, cdChar, cdBool }
                                     .Where(s => !string.IsNullOrEmpty(s))  // 只保留非 null 且非空字符串
                                     .ToArray();

            return resultArray;
        }

        /// <summary>
        /// Finds the value associated with the given key from the semaphore thread-lock dictionary and returns all non-empty values as an array of strings.<br />
        /// 从信号量线程锁字典中查找与给定键相关的值，并将所有非空的值以字符串数组的形式返回。
        /// </summary>
        /// <param name="key">The key used to find. Method looks up the corresponding value from multiple dictionaries based on this key.<br />用于查找的键。方法将根据这个键从多个字典中查找对应的值。</param>
        /// <returns>Returns an array of strings containing all non-empty values associated with the given key. If no matching value is found, an empty array is returned.<br />返回一个字符串数组，其中包含所有非空的、与给定键相关的值。如果没有找到匹配的值，则返回空数组。</returns>
        public string[] AllTypeSearchFromSemaphoreThreadLock(string key)
        {
            // 安全地从字典获取并转换为字符串（对于可能为 null 的值使用 ?.ToString()）
            string stlString = stlStringDictionary.GetByKey(key)?.ToString();
            string stlShort = stlShortDictionary.GetByKey(key)?.ToString();
            string stlLong = stlLongDictionary.GetByKey(key)?.ToString();
            string stlInt = stlIntegerDictionary.GetByKey(key)?.ToString();
            string stlFloat = stlFloatDictionary.GetByKey(key)?.ToString();
            string stlDouble = stlDoubleDictionary.GetByKey(key)?.ToString();
            string stlDecimal = stlDecimalDictionary.GetByKey(key)?.ToString();
            string stlChar = stlCharDictionary.GetByKey(key)?.ToString();
            string stlBool = stlBooleanDictionary.GetByKey(key)?.ToString();

            // 使用 LINQ 来过滤非 null 且非空的字符串，并将其转换为数组
            string[] resultArray = new[] { stlString, stlShort, stlLong, stlInt, stlFloat, stlDouble, stlDecimal, stlChar, stlBool }
                                     .Where(s => !string.IsNullOrEmpty(s))  // 只保留非 null 且非空字符串
                                     .ToArray();

            return resultArray;
        }
        #endregion
    }
}
