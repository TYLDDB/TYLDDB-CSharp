using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TYLDDB.Basic.Exception;
using TYLDDB.Parser;
using TYLDDB.Utils;

namespace TYLDDB
{
    /// <summary>
    /// The core class of the database.<br />
    /// 数据库的核心类。
    /// </summary>
    public partial class LDDB
    {
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

        ///////////////////////////////////////////////////// 方法

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
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        public async Task Parse()
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
            void CdString()
            {
                var dict = DataParser.ParseString(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdStringDictionary.Set(key, value);
                }
            }
            void CdShort()
            {
                var dict = DataParser.ParseShort(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdShortDictionary.Set(key, value);
                }
            }
            void CdLong()
            {
                var dict = DataParser.ParseLong(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdLongDictionary.Set(key, value);
                }
            }
            void CdInt()
            {
                var dict = DataParser.ParseInt(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdIntegerDictionary.Set(key, value);
                }
            }
            void CdFloat()
            {
                var dict = DataParser.ParseFloat(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdFloatDictionary.Set(key, value);
                }
            }
            void CdDouble()
            {
                var dict = DataParser.ParseDouble(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdDoubleDictionary.Set(key, value);
                }
            }
            void CdDecimal()
            {
                var dict = DataParser.ParseDecimal(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdDecimalDictionary.Set(key, value);
                }
            }
            void CdChar()
            {
                var dict = DataParser.ParseChar(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdCharDictionary.Set(key, value);
                }
            }
            void CdBool()
            {
                var dict = DataParser.ParseBoolean(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    cdBooleanDictionary.Set(key, value);
                }
            }

            // SemaphoreThreadLock
            void StlString()
            {
                var dict = DataParser.ParseString(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlStringDictionary.Set(key, value);
                }
            }
            void StlShort()
            {
                var dict = DataParser.ParseShort(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlShortDictionary.Set(key, value);
                }
            }
            void StlLong()
            {
                var dict = DataParser.ParseLong(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlLongDictionary.Set(key, value);
                }
            }
            void StlInt()
            {
                var dict = DataParser.ParseInt(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlIntegerDictionary.Set(key, value);
                }
            }
            void StlFloat()
            {
                var dict = DataParser.ParseFloat(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlFloatDictionary.Set(key, value);
                }
            }
            void StlDouble()
            {
                var dict = DataParser.ParseDouble(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlDoubleDictionary.Set(key, value);
                }
            }
            void StlDecimal()
            {
                var dict = DataParser.ParseDecimal(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlDecimalDictionary.Set(key, value);
                }
            }
            void StlChar()
            {
                var dict = DataParser.ParseChar(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlCharDictionary.Set(key, value);
                }
            }
            void StlBool()
            {
                var dict = DataParser.ParseBoolean(_databaseContent);

                // 遍历 dict 中的每一项
                foreach (var kvp in dict)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    stlBooleanDictionary.Set(key, value);
                }
            }
        }

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
    }
}
