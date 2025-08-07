using System;
using System.Collections.Generic;
using System.Linq;
using TYLDDB.Basic.Exception;
using TYLDDB.Utils.FastCache.ConcurrentDictionary;
using TYLDDB.Utils.FastCache.SemaphoreThreadLock;
using TYLDDB.Utils.Read;
using System.Threading;
using TYLDDB.Parser;
using System.Threading.Tasks;
using TYLDDB.Utils.DatabaseProcess;
using TYLDDB.Utils.DatabaseCache;


#if NET8_0_OR_GREATER
using TYLDDB.Utils.FastCache.TDCache;
#endif

namespace TYLDDB
{
    /// <summary>
    /// The core class of the database.<br />
    /// 数据库的核心类。
    /// </summary>
    public partial class LDDB
    {
        #region 私有字段
        private string _filePath;  // 存储文件路径
        private string _fileContent; // 存储文件内容
        private string _databaseContent; // 存储数据库内容
#pragma warning disable CS0618 // 类型或成员已过时
        private readonly Database_V1 database_v1;
#pragma warning restore CS0618 // 类型或成员已过时
        private readonly Database_V2 database_v2;
        private readonly CacheMode _cacheMode;

        #region Concurrent Dictionary Cache
        private readonly ConcurrentDictionaryCache<String> _stringConcurrentDictionaryCache;
        private readonly ConcurrentDictionaryCache<Int16> _int16ConcurrentDictionaryCache; // short
        private readonly ConcurrentDictionaryCache<Int32> _int32ConcurrentDictionaryCache; // int
        private readonly ConcurrentDictionaryCache<Int64> _int64ConcurrentDictionaryCache; // long
#if NET7_0_OR_GREATER
        private readonly ConcurrentDictionaryCache<Int128> _int128ConcurrentDictionaryCache;
#endif
        private readonly ConcurrentDictionaryCache<float> _floatConcurrentDictionaryCache;
        private readonly ConcurrentDictionaryCache<Double> _doubleConcurrentDictionaryCache;
        private readonly ConcurrentDictionaryCache<Decimal> _decimalConcurrentDictionaryCache;
        private readonly ConcurrentDictionaryCache<Char> _charConcurrentDictionaryCache;
        private readonly ConcurrentDictionaryCache<Boolean> _boolConcurrentDictionaryCache;
        #endregion

        private readonly CdStringDictionary cdStringDictionary;
        private readonly CdShortDictionary cdShortDictionary;
        private readonly CdLongDictionary cdLongDictionary;
        private readonly CdIntegerDictionary cdIntegerDictionary;
        private readonly CdFloatDictionary cdFloatDictionary;
        private readonly CdDoubleDictionary cdDoubleDictionary;
        private readonly CdDecimalDictionary cdDecimalDictionary;
        private readonly CdCharDictionary cdCharDictionary;
        private readonly CdBooleanDictionary cdBooleanDictionary;
        private readonly StlStringDictionary stlStringDictionary;
        private readonly StlShortDictionary stlShortDictionary;
        private readonly StlLongDictionary stlLongDictionary;
        private readonly StlIntegerDictionary stlIntegerDictionary;
        private readonly StlFloatDictionary stlFloatDictionary;
        private readonly StlDoubleDictionary stlDoubleDictionary;
        private readonly StlDecimalDictionary stlDecimalDictionary;
        private readonly StlCharDictionary stlCharDictionary;
        private readonly StlBooleanDictionary stlBooleanDictionary;

#if NET8_0_OR_GREATER
        private readonly TripleDictionaryCache tripleDictionaryCache;
#endif
        #endregion

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

        #region Instantiate
        /// <summary>
        /// Instantiate the LDDB class<br />
        /// 实例化LDDB类
        /// </summary>
        public LDDB()
        {
            #region 实例化数据库操作类
            database_v1 = new Database_V1();
            database_v2 = new Database_V2();
            #endregion

            #region 实例化并发词典
            cdStringDictionary = new CdStringDictionary();
            cdShortDictionary = new CdShortDictionary();
            cdLongDictionary = new CdLongDictionary();
            cdIntegerDictionary = new CdIntegerDictionary();
            cdFloatDictionary = new CdFloatDictionary();
            cdDoubleDictionary = new CdDoubleDictionary();
            cdDecimalDictionary = new CdDecimalDictionary();
            cdCharDictionary = new CdCharDictionary();
            cdBooleanDictionary = new CdBooleanDictionary();
            #endregion

            #region 实例化信号词典
            stlStringDictionary = new StlStringDictionary();
            stlShortDictionary = new StlShortDictionary();
            stlLongDictionary = new StlLongDictionary();
            stlIntegerDictionary = new StlIntegerDictionary();
            stlFloatDictionary = new StlFloatDictionary();
            stlDoubleDictionary = new StlDoubleDictionary();
            stlDecimalDictionary = new StlDecimalDictionary();
            stlCharDictionary = new StlCharDictionary();
            stlBooleanDictionary = new StlBooleanDictionary();
            #endregion

            _cacheMode = CacheMode.CDAndSTL;

#if NET8_0_OR_GREATER
            tripleDictionaryCache = new TripleDictionaryCache();
#endif
        }

        /// <summary>
        /// Instantiate the LDDB class<br />
        /// 实例化LDDB类
        /// </summary>
        public LDDB(CacheMode mode)
        {
            #region 实例化数据库操作类
            database_v1 = new Database_V1();
            database_v2 = new Database_V2();
            #endregion

            switch (mode)
            {
                case CacheMode.ConcurrentDictionary:
                    #region 实例化并发词典
                    cdStringDictionary = new CdStringDictionary();
                    cdShortDictionary = new CdShortDictionary();
                    cdLongDictionary = new CdLongDictionary();
                    cdIntegerDictionary = new CdIntegerDictionary();
                    cdFloatDictionary = new CdFloatDictionary();
                    cdDoubleDictionary = new CdDoubleDictionary();
                    cdDecimalDictionary = new CdDecimalDictionary();
                    cdCharDictionary = new CdCharDictionary();
                    cdBooleanDictionary = new CdBooleanDictionary();
                    #endregion
                    break;
                case CacheMode.SemaphoreThreadLock:
                    #region 实例化信号词典
                    stlStringDictionary = new StlStringDictionary();
                    stlShortDictionary = new StlShortDictionary();
                    stlLongDictionary = new StlLongDictionary();
                    stlIntegerDictionary = new StlIntegerDictionary();
                    stlFloatDictionary = new StlFloatDictionary();
                    stlDoubleDictionary = new StlDoubleDictionary();
                    stlDecimalDictionary = new StlDecimalDictionary();
                    stlCharDictionary = new StlCharDictionary();
                    stlBooleanDictionary = new StlBooleanDictionary();
                    #endregion
                    break;
                case CacheMode.CDAndSTL:
                    #region 实例化并发词典
                    cdStringDictionary = new CdStringDictionary();
                    cdShortDictionary = new CdShortDictionary();
                    cdLongDictionary = new CdLongDictionary();
                    cdIntegerDictionary = new CdIntegerDictionary();
                    cdFloatDictionary = new CdFloatDictionary();
                    cdDoubleDictionary = new CdDoubleDictionary();
                    cdDecimalDictionary = new CdDecimalDictionary();
                    cdCharDictionary = new CdCharDictionary();
                    cdBooleanDictionary = new CdBooleanDictionary();
                    #endregion

                    #region 实例化信号词典
                    stlStringDictionary = new StlStringDictionary();
                    stlShortDictionary = new StlShortDictionary();
                    stlLongDictionary = new StlLongDictionary();
                    stlIntegerDictionary = new StlIntegerDictionary();
                    stlFloatDictionary = new StlFloatDictionary();
                    stlDoubleDictionary = new StlDoubleDictionary();
                    stlDecimalDictionary = new StlDecimalDictionary();
                    stlCharDictionary = new StlCharDictionary();
                    stlBooleanDictionary = new StlBooleanDictionary();
                    #endregion
                    break;
#if NET8_0_OR_GREATER
                case CacheMode.TripleDictionaryCache:
                    tripleDictionaryCache = new TripleDictionaryCache();
                    break;
#endif
            }
        }

        /// <summary>
        /// Instantiate the LDDB class<br />
        /// 实例化LDDB类
        /// </summary>
        public LDDB(DatabaseProcess process)
        {
            #region 实例化并发词典
            cdStringDictionary = new CdStringDictionary();
            cdShortDictionary = new CdShortDictionary();
            cdLongDictionary = new CdLongDictionary();
            cdIntegerDictionary = new CdIntegerDictionary();
            cdFloatDictionary = new CdFloatDictionary();
            cdDoubleDictionary = new CdDoubleDictionary();
            cdDecimalDictionary = new CdDecimalDictionary();
            cdCharDictionary = new CdCharDictionary();
            cdBooleanDictionary = new CdBooleanDictionary();
            #endregion

            #region 实例化信号词典
            stlStringDictionary = new StlStringDictionary();
            stlShortDictionary = new StlShortDictionary();
            stlLongDictionary = new StlLongDictionary();
            stlIntegerDictionary = new StlIntegerDictionary();
            stlFloatDictionary = new StlFloatDictionary();
            stlDoubleDictionary = new StlDoubleDictionary();
            stlDecimalDictionary = new StlDecimalDictionary();
            stlCharDictionary = new StlCharDictionary();
            stlBooleanDictionary = new StlBooleanDictionary();
            #endregion

            _cacheMode = CacheMode.CDAndSTL;

#if NET8_0_OR_GREATER
            tripleDictionaryCache = new TripleDictionaryCache();
#endif

            switch (process)
            {
                case DatabaseProcess.V1:
                    database_v1 = new Database_V1();
                    break;
                case DatabaseProcess.V2:
                    database_v2 = new Database_V2();
                    break;
            }
        }

        /// <summary>
        /// Instantiate the LDDB class<br />
        /// 实例化LDDB类
        /// </summary>
        public LDDB(CacheMode mode, DatabaseProcess process)
        {
            switch (mode)
            {
                case CacheMode.ConcurrentDictionary:
                    #region 实例化并发词典
                    cdStringDictionary = new CdStringDictionary();
                    cdShortDictionary = new CdShortDictionary();
                    cdLongDictionary = new CdLongDictionary();
                    cdIntegerDictionary = new CdIntegerDictionary();
                    cdFloatDictionary = new CdFloatDictionary();
                    cdDoubleDictionary = new CdDoubleDictionary();
                    cdDecimalDictionary = new CdDecimalDictionary();
                    cdCharDictionary = new CdCharDictionary();
                    cdBooleanDictionary = new CdBooleanDictionary();
                    #endregion
                    _cacheMode = mode;
                    break;
                case CacheMode.SemaphoreThreadLock:
                    #region 实例化信号词典
                    stlStringDictionary = new StlStringDictionary();
                    stlShortDictionary = new StlShortDictionary();
                    stlLongDictionary = new StlLongDictionary();
                    stlIntegerDictionary = new StlIntegerDictionary();
                    stlFloatDictionary = new StlFloatDictionary();
                    stlDoubleDictionary = new StlDoubleDictionary();
                    stlDecimalDictionary = new StlDecimalDictionary();
                    stlCharDictionary = new StlCharDictionary();
                    stlBooleanDictionary = new StlBooleanDictionary();
                    #endregion

                    _cacheMode = mode;
                    break;
                case CacheMode.CDAndSTL:
                    #region 实例化并发词典
                    cdStringDictionary = new CdStringDictionary();
                    cdShortDictionary = new CdShortDictionary();
                    cdLongDictionary = new CdLongDictionary();
                    cdIntegerDictionary = new CdIntegerDictionary();
                    cdFloatDictionary = new CdFloatDictionary();
                    cdDoubleDictionary = new CdDoubleDictionary();
                    cdDecimalDictionary = new CdDecimalDictionary();
                    cdCharDictionary = new CdCharDictionary();
                    cdBooleanDictionary = new CdBooleanDictionary();
                    #endregion

                    #region 实例化信号词典
                    stlStringDictionary = new StlStringDictionary();
                    stlShortDictionary = new StlShortDictionary();
                    stlLongDictionary = new StlLongDictionary();
                    stlIntegerDictionary = new StlIntegerDictionary();
                    stlFloatDictionary = new StlFloatDictionary();
                    stlDoubleDictionary = new StlDoubleDictionary();
                    stlDecimalDictionary = new StlDecimalDictionary();
                    stlCharDictionary = new StlCharDictionary();
                    stlBooleanDictionary = new StlBooleanDictionary();
                    #endregion

                    _cacheMode = mode;
                    break;
#if NET8_0_OR_GREATER
                case CacheMode.TripleDictionaryCache:
                    tripleDictionaryCache = new TripleDictionaryCache();
                    _cacheMode = mode;
                    break;
#endif
            }

            switch (process)
            {
                case DatabaseProcess.V1:
                    database_v1 = new Database_V1();
                    break;
                case DatabaseProcess.V2:
                    database_v2 = new Database_V2();
                    break;
            }
        }
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

        #region Reading file
        /// <summary>
        /// Read the contents from the file<br/>
        /// 从文件中读取内容
        /// </summary>
        public void ReadingFile() => _fileContent = Reader.ReadFile(FilePath);

        /*
        /// <summary>
        /// <strong>由于内存问题一直未处理完毕，暂时弃用</strong>
        /// Read the contents from the file<br/>
        /// 从文件中读取内容
        /// </summary>
        public void ReadingFile_C_MinGW_Asm() => _fileContent = Reader.ReadFile_C_MinGW_Asm(FilePath); */

        /*
        /// <summary>
        /// <strong>由于内存问题一直未处理完毕，暂时弃用</strong>
        /// Read the contents from the file<br/>
        /// 从文件中读取内容
        /// </summary>
        public void ReadingFile_Cpp_MinGW() => _fileContent = Reader.ReadFile_Cpp_MinGW(FilePath); */

#if NET8_0_OR_GREATER
        /// <summary>
        /// Read the contents from the file<br/>
        /// 从文件中读取内容
        /// </summary>
        public void ReadingFile_C_MinGW() => _fileContent = Reader.ReadFile_C_MinGW(FilePath);

        /// <summary>
        /// Read the contents from the file<br/>
        /// 从文件中读取内容
        /// </summary>
        public void ReadingFile_C_VisualStudio() => _fileContent = Reader.ReadFile_C_VS(FilePath);
#endif
        #endregion

        #region Database process
        /// <summary>
        /// Set the database to load<br/>
        /// 设置要加载的数据库
        /// </summary>
        /// <param name="db">name of the database<br/>数据库名称</param>
        public void LoadDatabase(string db) => LoadDatabase_V2(db);

        /// <summary>
        /// Set the database to load<br/>
        /// 设置要加载的数据库
        /// </summary>
        /// <param name="db">name of the database<br/>数据库名称</param>
        [Obsolete("Too slow.")]
        public void LoadDatabase_V1(string db)
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
            if (_fileContent == null)
            {
                ReadingFile();
            }
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

        #region Parser
        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        [Obsolete]
        public void Parse_V1()
        {
            switch (_cacheMode)
            {
                case CacheMode.CDAndSTL:
                    {
                        #region method
                        void String()
                        {
                            var dict = DataParser_V1.ParseString(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlStringDictionary.Set(key, value);
                                cdStringDictionary.Set(key, value);
                            }
                        }
                        void Short()
                        {
                            var dict = DataParser_V1.ParseShort(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlShortDictionary.Set(key, value);
                                cdShortDictionary.Set(key, value);
                            }
                        }
                        void Long()
                        {
                            var dict = DataParser_V1.ParseLong(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlLongDictionary.Set(key, value);
                                cdLongDictionary.Set(key, value);
                            }
                        }
                        void Int()
                        {
                            var dict = DataParser_V1.ParseInt(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlIntegerDictionary.Set(key, value);
                                cdIntegerDictionary.Set(key, value);
                            }
                        }
                        void Float()
                        {
                            var dict = DataParser_V1.ParseFloat(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlFloatDictionary.Set(key, value);
                                cdFloatDictionary.Set(key, value);
                            }
                        }
                        void Double()
                        {
                            var dict = DataParser_V1.ParseDouble(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlDoubleDictionary.Set(key, value);
                                cdDoubleDictionary.Set(key, value);
                            }
                        }
                        void Decimal()
                        {
                            var dict = DataParser_V1.ParseDecimal(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlDecimalDictionary.Set(key, value);
                                cdDecimalDictionary.Set(key, value);
                            }
                        }
                        void Char()
                        {
                            var dict = DataParser_V1.ParseChar(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlCharDictionary.Set(key, value);
                                cdCharDictionary.Set(key, value);
                            }
                        }
                        void Bool()
                        {
                            var dict = DataParser_V1.ParseBoolean(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlBooleanDictionary.Set(key, value);
                                cdBooleanDictionary.Set(key, value);
                            }
                        }
                        #endregion

                        #region thread
                        Thread _str = new Thread(String);
                        Thread _short = new Thread(Short);
                        Thread _long = new Thread(Long);
                        Thread _float = new Thread(Float);
                        Thread _double = new Thread(Double);
                        Thread _decimal = new Thread(Decimal);
                        Thread _char = new Thread(Char);
                        Thread _int = new Thread(Int);
                        Thread _bool = new Thread(Bool);

                        _str.Start();
                        _short.Start();
                        _long.Start();
                        _float.Start();
                        _double.Start();
                        _decimal.Start();
                        _char.Start();
                        _int.Start();
                        _bool.Start();

                        _str.Join();
                        _short.Join();
                        _long.Join();
                        _float.Join();
                        _double.Join();
                        _decimal.Join();
                        _char.Join();
                        _int.Join();
                        _bool.Join();
                        break;
                        #endregion
                    }

                case CacheMode.SemaphoreThreadLock:
                    {
                        #region method
                        void String()
                        {
                            var dict = DataParser_V1.ParseString(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlStringDictionary.Set(key, value);
                            }
                        }
                        void Short()
                        {
                            var dict = DataParser_V1.ParseShort(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlShortDictionary.Set(key, value);
                            }
                        }
                        void Long()
                        {
                            var dict = DataParser_V1.ParseLong(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlLongDictionary.Set(key, value);
                            }
                        }
                        void Int()
                        {
                            var dict = DataParser_V1.ParseInt(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlIntegerDictionary.Set(key, value);
                            }
                        }
                        void Float()
                        {
                            var dict = DataParser_V1.ParseFloat(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlFloatDictionary.Set(key, value);
                            }
                        }
                        void Double()
                        {
                            var dict = DataParser_V1.ParseDouble(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlDoubleDictionary.Set(key, value);
                            }
                        }
                        void Decimal()
                        {
                            var dict = DataParser_V1.ParseDecimal(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlDecimalDictionary.Set(key, value);
                            }
                        }
                        void Char()
                        {
                            var dict = DataParser_V1.ParseChar(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlCharDictionary.Set(key, value);
                            }
                        }
                        void Bool()
                        {
                            var dict = DataParser_V1.ParseBoolean(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                stlBooleanDictionary.Set(key, value);
                            }
                        }
                        #endregion

                        #region thread
                        Thread _str = new Thread(String);
                        Thread _short = new Thread(Short);
                        Thread _long = new Thread(Long);
                        Thread _float = new Thread(Float);
                        Thread _double = new Thread(Double);
                        Thread _decimal = new Thread(Decimal);
                        Thread _char = new Thread(Char);
                        Thread _int = new Thread(Int);
                        Thread _bool = new Thread(Bool);

                        _str.Start();
                        _short.Start();
                        _long.Start();
                        _float.Start();
                        _double.Start();
                        _decimal.Start();
                        _char.Start();
                        _int.Start();
                        _bool.Start();

                        _str.Join();
                        _short.Join();
                        _long.Join();
                        _float.Join();
                        _double.Join();
                        _decimal.Join();
                        _char.Join();
                        _int.Join();
                        _bool.Join();
                        break;
                        #endregion
                    }

                case CacheMode.ConcurrentDictionary:
                    {
                        #region method
                        void String()
                        {
                            var dict = DataParser_V1.ParseString(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdStringDictionary.Set(key, value);
                            }
                        }
                        void Short()
                        {
                            var dict = DataParser_V1.ParseShort(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdShortDictionary.Set(key, value);
                            }
                        }
                        void Long()
                        {
                            var dict = DataParser_V1.ParseLong(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdLongDictionary.Set(key, value);
                            }
                        }
                        void Int()
                        {
                            var dict = DataParser_V1.ParseInt(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdIntegerDictionary.Set(key, value);
                            }
                        }
                        void Float()
                        {
                            var dict = DataParser_V1.ParseFloat(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdFloatDictionary.Set(key, value);
                            }
                        }
                        void Double()
                        {
                            var dict = DataParser_V1.ParseDouble(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdDoubleDictionary.Set(key, value);
                            }
                        }
                        void Decimal()
                        {
                            var dict = DataParser_V1.ParseDecimal(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdDecimalDictionary.Set(key, value);
                            }
                        }
                        void Char()
                        {
                            var dict = DataParser_V1.ParseChar(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdCharDictionary.Set(key, value);
                            }
                        }
                        void Bool()
                        {
                            var dict = DataParser_V1.ParseBoolean(_databaseContent);

                            // 遍历 dict 中的每一项
                            foreach (var kvp in dict)
                            {
                                var key = kvp.Key;   // 获取第一个值 (key)
                                var value = kvp.Value;  // 获取第二个值 (value)

                                // 将 key-value 对存储到缓存
                                cdBooleanDictionary.Set(key, value);
                            }
                        }
                        #endregion

                        #region thread
                        Thread _str = new Thread(String);
                        Thread _short = new Thread(Short);
                        Thread _long = new Thread(Long);
                        Thread _float = new Thread(Float);
                        Thread _double = new Thread(Double);
                        Thread _decimal = new Thread(Decimal);
                        Thread _char = new Thread(Char);
                        Thread _int = new Thread(Int);
                        Thread _bool = new Thread(Bool);

                        _str.Start();
                        _short.Start();
                        _long.Start();
                        _float.Start();
                        _double.Start();
                        _decimal.Start();
                        _char.Start();
                        _int.Start();
                        _bool.Start();

                        _str.Join();
                        _short.Join();
                        _long.Join();
                        _float.Join();
                        _double.Join();
                        _decimal.Join();
                        _char.Join();
                        _int.Join();
                        _bool.Join();
                        break;
                        #endregion
                    }

#if NET8_0_OR_GREATER
                case CacheMode.TripleDictionaryCache:
                    TripleDictParse();
                    break;
#endif
            }
        }

        /// <summary>
        /// Reparse the entire database.<br />
        /// 重新解析整个数据库。
        /// </summary>
        [Obsolete]
        public void ParseAsync_V1()
        {
            switch (_cacheMode)
            {
                case CacheMode.CDAndSTL:
                    CDAndSTL();
                    break;
                case CacheMode.SemaphoreThreadLock:
                    SemaphoreThreadLock();
                    break;
                case CacheMode.ConcurrentDictionary:
                    ConcurrentDictionary();
                    break;
#if NET8_0_OR_GREATER
                case CacheMode.TripleDictionaryCache:
                    throw new NotSupportedException("Three-value dictionaries do not yet support asynchronous methods.");
                    // break;
#endif
            }
            async void CDAndSTL()
            {
                #region tasks
                // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行
                Task StringCacheTask = Task.Factory.StartNew(() => String(), TaskCreationOptions.LongRunning);
                Task IntCacheTask = Task.Factory.StartNew(() => Int(), TaskCreationOptions.LongRunning);
                Task ShortCacheTask = Task.Factory.StartNew(() => Short(), TaskCreationOptions.LongRunning);
                Task LongCacheTask = Task.Factory.StartNew(() => Long(), TaskCreationOptions.LongRunning);
                Task FloatCacheTask = Task.Factory.StartNew(() => Float(), TaskCreationOptions.LongRunning);
                Task DoubleCacheTask = Task.Factory.StartNew(() => Double(), TaskCreationOptions.LongRunning);
                Task DecimalCacheTask = Task.Factory.StartNew(() => Decimal(), TaskCreationOptions.LongRunning);
                Task CharCacheTask = Task.Factory.StartNew(() => Char(), TaskCreationOptions.LongRunning);
                Task BoolCacheTask = Task.Factory.StartNew(() => Bool(), TaskCreationOptions.LongRunning);

                // 等待所有任务完成
                await Task.WhenAll(StringCacheTask,
                                   IntCacheTask,
                                   ShortCacheTask,
                                   LongCacheTask,
                                   FloatCacheTask,
                                   DoubleCacheTask,
                                   DecimalCacheTask,
                                   CharCacheTask,
                                   BoolCacheTask);
                #endregion

                #region method
                void String()
                {
                    var dict = DataParser_V1.ParseString(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlStringDictionary.Set(key, value);
                        cdStringDictionary.Set(key, value);
                    }
                }
                void Short()
                {
                    var dict = DataParser_V1.ParseShort(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlShortDictionary.Set(key, value);
                        cdShortDictionary.Set(key, value);
                    }
                }
                void Long()
                {
                    var dict = DataParser_V1.ParseLong(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlLongDictionary.Set(key, value);
                        cdLongDictionary.Set(key, value);
                    }
                }
                void Int()
                {
                    var dict = DataParser_V1.ParseInt(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlIntegerDictionary.Set(key, value);
                        cdIntegerDictionary.Set(key, value);
                    }
                }
                void Float()
                {
                    var dict = DataParser_V1.ParseFloat(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlFloatDictionary.Set(key, value);
                        cdFloatDictionary.Set(key, value);
                    }
                }
                void Double()
                {
                    var dict = DataParser_V1.ParseDouble(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlDoubleDictionary.Set(key, value);
                        cdDoubleDictionary.Set(key, value);
                    }
                }
                void Decimal()
                {
                    var dict = DataParser_V1.ParseDecimal(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlDecimalDictionary.Set(key, value);
                        cdDecimalDictionary.Set(key, value);
                    }
                }
                void Char()
                {
                    var dict = DataParser_V1.ParseChar(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlCharDictionary.Set(key, value);
                        cdCharDictionary.Set(key, value);
                    }
                }
                void Bool()
                {
                    var dict = DataParser_V1.ParseBoolean(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlBooleanDictionary.Set(key, value);
                        cdBooleanDictionary.Set(key, value);
                    }
                }
                #endregion
            }

        async void SemaphoreThreadLock()
            {
                #region tasks
                // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行
                Task StringCacheTask = Task.Factory.StartNew(() => String(), TaskCreationOptions.LongRunning);
                Task IntCacheTask = Task.Factory.StartNew(() => Int(), TaskCreationOptions.LongRunning);
                Task ShortCacheTask = Task.Factory.StartNew(() => Short(), TaskCreationOptions.LongRunning);
                Task LongCacheTask = Task.Factory.StartNew(() => Long(), TaskCreationOptions.LongRunning);
                Task FloatCacheTask = Task.Factory.StartNew(() => Float(), TaskCreationOptions.LongRunning);
                Task DoubleCacheTask = Task.Factory.StartNew(() => Double(), TaskCreationOptions.LongRunning);
                Task DecimalCacheTask = Task.Factory.StartNew(() => Decimal(), TaskCreationOptions.LongRunning);
                Task CharCacheTask = Task.Factory.StartNew(() => Char(), TaskCreationOptions.LongRunning);
                Task BoolCacheTask = Task.Factory.StartNew(() => Bool(), TaskCreationOptions.LongRunning);

                // 等待所有任务完成
                await Task.WhenAll(StringCacheTask,
                                   IntCacheTask,
                                   ShortCacheTask,
                                   LongCacheTask,
                                   FloatCacheTask,
                                   DoubleCacheTask,
                                   DecimalCacheTask,
                                   CharCacheTask,
                                   BoolCacheTask);
                #endregion

                #region method
                void String()
                {
                    var dict = DataParser_V1.ParseString(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlStringDictionary.Set(key, value);
                    }
                }
                void Short()
                {
                    var dict = DataParser_V1.ParseShort(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlShortDictionary.Set(key, value);
                    }
                }
                void Long()
                {
                    var dict = DataParser_V1.ParseLong(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlLongDictionary.Set(key, value);
                    }
                }
                void Int()
                {
                    var dict = DataParser_V1.ParseInt(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlIntegerDictionary.Set(key, value);
                    }
                }
                void Float()
                {
                    var dict = DataParser_V1.ParseFloat(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlFloatDictionary.Set(key, value);
                    }
                }
                void Double()
                {
                    var dict = DataParser_V1.ParseDouble(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlDoubleDictionary.Set(key, value);
                    }
                }
                void Decimal()
                {
                    var dict = DataParser_V1.ParseDecimal(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlDecimalDictionary.Set(key, value);
                    }
                }
                void Char()
                {
                    var dict = DataParser_V1.ParseChar(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlCharDictionary.Set(key, value);
                    }
                }
                void Bool()
                {
                    var dict = DataParser_V1.ParseBoolean(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        stlBooleanDictionary.Set(key, value);
                    }
                }
                #endregion
            }

            async void ConcurrentDictionary()
            {
                #region tasks
                // 创建多个任务，并使用 LongRunning 来确保每个任务在独立线程中运行
                Task StringCacheTask = Task.Factory.StartNew(() => String(), TaskCreationOptions.LongRunning);
                Task IntCacheTask = Task.Factory.StartNew(() => Int(), TaskCreationOptions.LongRunning);
                Task ShortCacheTask = Task.Factory.StartNew(() => Short(), TaskCreationOptions.LongRunning);
                Task LongCacheTask = Task.Factory.StartNew(() => Long(), TaskCreationOptions.LongRunning);
                Task FloatCacheTask = Task.Factory.StartNew(() => Float(), TaskCreationOptions.LongRunning);
                Task DoubleCacheTask = Task.Factory.StartNew(() => Double(), TaskCreationOptions.LongRunning);
                Task DecimalCacheTask = Task.Factory.StartNew(() => Decimal(), TaskCreationOptions.LongRunning);
                Task CharCacheTask = Task.Factory.StartNew(() => Char(), TaskCreationOptions.LongRunning);
                Task BoolCacheTask = Task.Factory.StartNew(() => Bool(), TaskCreationOptions.LongRunning);

                // 等待所有任务完成
                await Task.WhenAll(StringCacheTask,
                                   IntCacheTask,
                                   ShortCacheTask,
                                   LongCacheTask,
                                   FloatCacheTask,
                                   DoubleCacheTask,
                                   DecimalCacheTask,
                                   CharCacheTask,
                                   BoolCacheTask);
                #endregion

                #region method
                void String()
                {
                    var dict = DataParser_V1.ParseString(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdStringDictionary.Set(key, value);
                    }
                }
                void Short()
                {
                    var dict = DataParser_V1.ParseShort(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdShortDictionary.Set(key, value);
                    }
                }
                void Long()
                {
                    var dict = DataParser_V1.ParseLong(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdLongDictionary.Set(key, value);
                    }
                }
                void Int()
                {
                    var dict = DataParser_V1.ParseInt(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdIntegerDictionary.Set(key, value);
                    }
                }
                void Float()
                {
                    var dict = DataParser_V1.ParseFloat(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdFloatDictionary.Set(key, value);
                    }
                }
                void Double()
                {
                    var dict = DataParser_V1.ParseDouble(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdDoubleDictionary.Set(key, value);
                    }
                }
                void Decimal()
                {
                    var dict = DataParser_V1.ParseDecimal(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdDecimalDictionary.Set(key, value);
                    }
                }
                void Char()
                {
                    var dict = DataParser_V1.ParseChar(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdCharDictionary.Set(key, value);
                    }
                }
                void Bool()
                {
                    var dict = DataParser_V1.ParseBoolean(_databaseContent);

                    // 遍历 dict 中的每一项
                    foreach (var kvp in dict)
                    {
                        var key = kvp.Key;   // 获取第一个值 (key)
                        var value = kvp.Value;  // 获取第二个值 (value)

                        // 将 key-value 对存储到缓存
                        cdBooleanDictionary.Set(key, value);
                    }
                }
                #endregion
            }
        }

#if NET8_0_OR_GREATER
        // 三值字典操作
        /// <summary>
        /// Parses the database and writes to the three-value dictionary cache.<br />
        /// 解析数据库并写入三值字典缓存。
        /// </summary>
        public void TripleDictParse()
        {
            #region 各数据类型字典
            var _str = DataParser_V1.ParseString(_databaseContent);
            var _short = DataParser_V1.ParseShort(_databaseContent);
            var _long = DataParser_V1.ParseLong(_databaseContent);
            var _int = DataParser_V1.ParseInt(_databaseContent);
            var _float = DataParser_V1.ParseFloat(_databaseContent);
            var _double = DataParser_V1.ParseDouble(_databaseContent);
            var _decimal = DataParser_V1.ParseDecimal(_databaseContent);
            var _char = DataParser_V1.ParseChar(_databaseContent);
            var _bool = DataParser_V1.ParseBoolean(_databaseContent);
            #endregion

            #region 方法
            void Str()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _str)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("string", key, value);
                }
            }

            void Short()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _short)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("short", key, value);
                }
            }

            void Long()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _long)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("long", key, value);
                }
            }

            void Int()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _int)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("int", key, value);
                }
            }

            void Float()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _float)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("float", key, value);
                }
            }

            void Double()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _double)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("double", key, value);
                }
            }

            void Decimal()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _decimal)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("decimal", key, value);
                }
            }

            void Char()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _char)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("char", key, value);
                }
            }

            void Bool()
            {
                // 遍历 dict 中的每一项
                foreach (var kvp in _bool)
                {
                    var key = kvp.Key;   // 获取第一个值 (key)
                    var value = kvp.Value;  // 获取第二个值 (value)

                    // 将 key-value 对存储到缓存
                    tripleDictionaryCache.Set("bool", key, value);
                }
            }
            #endregion

            #region 线程
            #region 创建线程实例
            Thread strThread = new(Str);
            Thread shortThread = new(Short);
            Thread longThread = new(Long);
            Thread floatThread = new(Float);
            Thread doubleThread = new(Double);
            Thread decimalThread = new(Decimal);
            Thread charThread = new(Char);
            Thread intThread = new(Int);
            Thread boolThread = new(Bool);
            #endregion
            #region 启动线程
            strThread.Start();
            shortThread.Start();
            longThread.Start();
            floatThread.Start();
            doubleThread.Start();
            decimalThread.Start();
            charThread.Start();
            intThread.Start();
            boolThread.Start();
            #endregion
            #region 等待线程执行完毕
            strThread.Join();
            shortThread.Join();
            longThread.Join();
            floatThread.Join();
            doubleThread.Join();
            decimalThread.Join();
            charThread.Join();
            intThread.Join();
            boolThread.Join();
            #endregion
            #endregion
        }
#endif
        #endregion
        #endregion

        #region Enum
        /// <summary>
        /// Cache mode.<br />
        /// 缓存模式。
        /// </summary>
        public enum CacheMode
        {
            /// <summary>
            /// Use concurrent dictionaries to achieve high concurrency stability.<br />
            /// 使用并发词典来实现高并发的稳定性。
            /// </summary>
            ConcurrentDictionary,

            /// <summary>
            /// Use semaphore based thread locks to achieve high concurrency stability.<br />
            /// 使用基于信号量的线程锁来实现高并发的稳定性。
            /// </summary>
            SemaphoreThreadLock,

#if NET8_0_OR_GREATER
            /// <summary>
            /// High concurrency stability is achieved by using concurrent dictionary as core and semaphore based thread lock.<br />
            /// 以并发词典为核，结合基于信号量的线程锁实现高并发的稳定性。
            /// </summary>
            TripleDictionaryCache,
#endif

            /// <summary>
            /// <see cref="ConcurrentDictionary"/> + <see cref="SemaphoreThreadLock"/>
            /// </summary>
            CDAndSTL,
        }

        /// <summary>
        /// Database pre-parsing process.<br />
        /// 数据库预解析流程。
        /// </summary>
        public enum DatabaseProcess
        {
            /// <summary>
            /// Based on regular expressions.<br />
            /// 基于正则表达式。
            /// </summary>
            V1,

            /// <summary>
            /// Parse database content directly based on string segmentation.<br />
            /// 直接基于字符串分割解析数据库内容。
            /// </summary>
            V2,
        }
        #endregion
    }
}
