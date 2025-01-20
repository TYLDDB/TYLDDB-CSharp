using TYLDDB.Utils.Database;
using TYLDDB.Utils.FastCache.ConcurrentDictionary;
using TYLDDB.Utils.FastCache.SemaphoreThreadLock;

#if NET8_0_OR_GREATER
using TYLDDB.Utils.FastCache.TDCache;
#endif

namespace TYLDDB
{
    public partial class LDDB
    {
        #region 私有字段
        private string _filePath;  // 存储文件路径
        private string _fileContent; // 存储文件内容
        private string _databaseContent; // 存储数据库内容
        private readonly Database_V1 database_v1;
        private readonly Database_V2 database_v2;
        private readonly CacheMode _cacheMode;
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
    }
}
