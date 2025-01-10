namespace TYLDDB.Basic.Exception
{
    /// <summary>
    /// Database not found<br/>
    /// 未找到数据库
    /// </summary>
    public class DatabaseNotFoundException : TylddbException
    {
        /// <summary>
        /// Database not found<br/>
        /// 未找到数据库
        /// </summary>
        public DatabaseNotFoundException(string message) : base(message) { }
    }

    /// <summary>
    /// Description Failed to obtain the database content<br/>
    /// 数据库内容获取失败
    /// </summary>
    public class GetDatabaseContentErrorException : TylddbException
    {
        /// <summary>
        /// Description Failed to obtain the database content<br/>
        /// 数据库内容获取失败
        /// </summary>
        public GetDatabaseContentErrorException(string message) : base(message) { }
    }
}
