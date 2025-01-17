using TYLDDB.Utils.Database;
using TYLDDB.Utils.FastCache.ConcurrentDictionary;
using TYLDDB.Utils.FastCache.SemaphoreThreadLock;

namespace TYLDDB
{
    public partial class LDDB
    {
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
        }
    }
}
