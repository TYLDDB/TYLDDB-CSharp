using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TYLDDB.Basic;
using TYLDDB.Parser;
using TYLDDB.Utils;
using TYLDDB.Utils.FastCache.ConcurrentDictionary;
using TYLDDB.Utils.FastCache.SemaphoreThreadLock;

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
            database = new Database(); // 实例化数据库操作类

            // 实例化并发词典
            cdStringDictionary = new CdStringDictionary();
            cdShortDictionary = new CdShortDictionary();
            cdLongDictionary = new CdLongDictionary();
            cdIntegerDictionary = new CdIntegerDictionary();
            cdFloatDictionary = new CdFloatDictionary();
            cdDoubleDictionary = new CdDoubleDictionary();
            cdDecimalDictionary = new CdDecimalDictionary();
            cdCharDictionary = new CdCharDictionary();
            cdBooleanDictionary = new CdBooleanDictionary();

            // 实例化信号词典
            stlStringDictionary = new StlStringDictionary();
            stlShortDictionary = new StlShortDictionary();
            stlLongDictionary = new StlLongDictionary();
            stlIntegerDictionary = new StlIntegerDictionary();
            stlFloatDictionary = new StlFloatDictionary();
            stlDoubleDictionary = new StlDoubleDictionary();
            stlDecimalDictionary = new StlDecimalDictionary();
            stlCharDictionary = new StlCharDictionary();
            stlBooleanDictionary = new StlBooleanDictionary();
        }

        ///////////////////////////////////////////////////// 私有字段
        private string _filePath;  // 存储文件路径
        private string _fileContent; // 存储文件内容
        private string _database; // 存储正在访问的数据库
        private string _databaseContent; // 存储数据库内容
        private bool _isRead = false; // 是否已调用读取文件
        private Database database;
        private CdStringDictionary cdStringDictionary;
        private CdShortDictionary cdShortDictionary;
        private CdLongDictionary cdLongDictionary;
        private CdIntegerDictionary cdIntegerDictionary;
        private CdFloatDictionary cdFloatDictionary;
        private CdDoubleDictionary cdDoubleDictionary;
        private CdDecimalDictionary cdDecimalDictionary;
        private CdCharDictionary cdCharDictionary;
        private CdBooleanDictionary cdBooleanDictionary;
        private StlStringDictionary stlStringDictionary;
        private StlShortDictionary stlShortDictionary;
        private StlLongDictionary stlLongDictionary;
        private StlIntegerDictionary stlIntegerDictionary;
        private StlFloatDictionary stlFloatDictionary;
        private StlDoubleDictionary stlDoubleDictionary;
        private StlDecimalDictionary stlDecimalDictionary;
        private StlCharDictionary stlCharDictionary;
        private StlBooleanDictionary stlBooleanDictionary;

        ///////////////////////////////////////////////////// 公开字段
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
                throw new FilePathIsNullOrWhiteSpace("文件路径不能为 null 或空白");
            }
            return true;
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
        public async void LoadDatabase(string db)
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
            await ParseAsync();
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

        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        public async Task ParseAsync()
        {
            // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行

            // ConcurrentDictionary
            Task cdStringCacheTask = Task.Factory.StartNew(() => CdString(), TaskCreationOptions.LongRunning);
            Task cdIntCacheTask = Task.Factory.StartNew(() => CdInt(), TaskCreationOptions.LongRunning);
            Task cdShortCacheTask = Task.Factory.StartNew(() => CdShort(), TaskCreationOptions.LongRunning);
            Task cdLongCacheTask = Task.Factory.StartNew(() => CdLong(), TaskCreationOptions.LongRunning);
            Task cdFloatCacheTask = Task.Factory.StartNew(() => CdFloat(), TaskCreationOptions.LongRunning);
            Task cdDoubleCacheTask = Task.Factory.StartNew(() => CdDouble(), TaskCreationOptions.LongRunning);
            Task cdDecimalCacheTask = Task.Factory.StartNew(() => CdDecimal(), TaskCreationOptions.LongRunning);
            Task cdCharCacheTask = Task.Factory.StartNew(() => CdChar(), TaskCreationOptions.LongRunning);
            Task cdBoolCacheTask = Task.Factory.StartNew(() => CdBool(), TaskCreationOptions.LongRunning);

            // SemaphoreThreadLock
            Task stlStringCacheTask = Task.Factory.StartNew(() => StlString(), TaskCreationOptions.LongRunning);
            Task stlIntCacheTask = Task.Factory.StartNew(() => StlInt(), TaskCreationOptions.LongRunning);
            Task stlShortCacheTask = Task.Factory.StartNew(() => StlShort(), TaskCreationOptions.LongRunning);
            Task stlLongCacheTask = Task.Factory.StartNew(() => StlLong(), TaskCreationOptions.LongRunning);
            Task stlFloatCacheTask = Task.Factory.StartNew(() => StlFloat(), TaskCreationOptions.LongRunning);
            Task stlDoubleCacheTask = Task.Factory.StartNew(() => StlDouble(), TaskCreationOptions.LongRunning);
            Task stlDecimalCacheTask = Task.Factory.StartNew(() => StlDecimal(), TaskCreationOptions.LongRunning);
            Task stlCharCacheTask = Task.Factory.StartNew(() => StlChar(), TaskCreationOptions.LongRunning);
            Task stlBoolCacheTask = Task.Factory.StartNew(() => StlBool(), TaskCreationOptions.LongRunning);

            // 等待所有任务完成
            await Task.WhenAll(cdStringCacheTask,
                               cdIntCacheTask,
                               cdShortCacheTask,
                               cdLongCacheTask,
                               cdFloatCacheTask,
                               cdDoubleCacheTask,
                               cdDecimalCacheTask,
                               cdCharCacheTask,
                               cdBoolCacheTask,
                               stlStringCacheTask,
                               stlIntCacheTask,
                               stlShortCacheTask,
                               stlLongCacheTask,
                               stlFloatCacheTask,
                               stlDoubleCacheTask,
                               stlDecimalCacheTask,
                               stlCharCacheTask,
                               stlBoolCacheTask);

            // ConcurrentDictionary
            async void CdString()
            {
                var dict = DataParser.ParseString(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdStringDictionary.SetAsync(key, value);
                }
            }
            async void CdShort()
            {
                var dict = DataParser.ParseShort(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdShortDictionary.SetAsync(key, value);
                }
            }
            async void CdLong()
            {
                var dict = DataParser.ParseLong(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdLongDictionary.SetAsync(key, value);
                }
            }
            async void CdInt()
            {
                var dict = DataParser.ParseInt(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdIntegerDictionary.SetAsync(key, value);
                }
            }
            async void CdFloat()
            {
                var dict = DataParser.ParseFloat(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdFloatDictionary.SetAsync(key, value);
                }
            }
            async void CdDouble()
            {
                var dict = DataParser.ParseDouble(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdDoubleDictionary.SetAsync(key, value);
                }
            }
            async void CdDecimal()
            {
                var dict = DataParser.ParseDecimal(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdDecimalDictionary.SetAsync(key, value);
                }
            }
            async void CdChar()
            {
                var dict = DataParser.ParseChar(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdCharDictionary.SetAsync(key, value);
                }
            }
            async void CdBool()
            {
                var dict = DataParser.ParseBoolean(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await cdBooleanDictionary.SetAsync(key, value);
                }
            }

            // SemaphoreThreadLock
            async void StlString()
            {
                var dict = DataParser.ParseString(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlStringDictionary.SetAsync(key, value);
                }
            }
            async void StlShort()
            {
                var dict = DataParser.ParseShort(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlShortDictionary.SetAsync(key, value);
                }
            }
            async void StlLong()
            {
                var dict = DataParser.ParseLong(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlLongDictionary.SetAsync(key, value);
                }
            }
            async void StlInt()
            {
                var dict = DataParser.ParseInt(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlIntegerDictionary.SetAsync(key, value);
                }
            }
            async void StlFloat()
            {
                var dict = DataParser.ParseFloat(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlFloatDictionary.SetAsync(key, value);
                }
            }
            async void StlDouble()
            {
                var dict = DataParser.ParseDouble(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlDoubleDictionary.SetAsync(key, value);
                }
            }
            async void StlDecimal()
            {
                var dict = DataParser.ParseDecimal(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlDecimalDictionary.SetAsync(key, value);
                }
            }
            async void StlChar()
            {
                var dict = DataParser.ParseChar(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlCharDictionary.SetAsync(key, value);
                }
            }
            async void StlBool()
            {
                var dict = DataParser.ParseBoolean(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    await stlBooleanDictionary.SetAsync(key, value);
                }
            }
        }

        public string[] AllTypeSearch(string key)
        {
            string cdString = cdStringDictionary.GetByKey(key);
            string cdShort = cdShortDictionary.GetByKey(key).ToString();
            string cdLong = cdLongDictionary.GetByKey(key).ToString();
            string cdInt = cdIntegerDictionary.GetByKey(key).ToString();
            string cdFloat = cdFloatDictionary.GetByKey(key).ToString();
            string cdDouble = cdDoubleDictionary.GetByKey(key).ToString();
            string cdDecimal = cdDecimalDictionary.GetByKey(key).ToString();
            string cdChar = cdCharDictionary.GetByKey(key).ToString();
            string cdBool = cdBooleanDictionary.GetByKey(key).ToString();

            // 使用 LINQ 来过滤非 null 的项并将其转换为数组
            string[] resultArray = new[] { cdString, cdShort, cdLong, cdInt, cdFloat, cdDouble, cdDecimal, cdChar, cdBool }
                                     .Where(s => s != null)  // 只保留非 null 的字符串
                                     .ToArray();
            return resultArray;
        }
    }
}
