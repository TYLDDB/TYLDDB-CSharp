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
                case CacheMode.TripleDictionaryCache:
                    break;
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
                case CacheMode.TripleDictionaryCache:
                    break;
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
    }
}
