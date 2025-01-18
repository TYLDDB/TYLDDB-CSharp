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

#if NET8_0_OR_GREATER
        private readonly TripleDictionaryCache tripleDictionaryCache;
#endif
#endregion
    }
}
