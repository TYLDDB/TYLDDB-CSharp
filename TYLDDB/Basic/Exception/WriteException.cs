namespace TYLDDB.Basic.Exception
{
    /// <summary>
    /// Error database content to file.<br />
    /// 写入数据库内容到文件时的错误。
    /// </summary>
    public class WriteStringToFileException : WriteException
    {
        /// <summary>
        /// Error database content to file.<br />
        /// 写入数据库内容到文件时的错误。
        /// </summary>
        public WriteStringToFileException(string message) : base(message) { }
    }

    /// <summary>
    /// Block-writing error in database to file.<br />
    /// 分块写入数据库到文件的错误。
    /// </summary>
    public class WriteStringToFileInChunksException : WriteException
    {
        /// <summary>
        /// Block-writing error in database to file.<br />
        /// 分块写入数据库到文件的错误。
        /// </summary>
        public WriteStringToFileInChunksException(string message) : base(message) { }
    }
}
