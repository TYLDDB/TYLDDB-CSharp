namespace TYLDDB.Basic.Exception
{
    /// <summary>
    /// File opening failure<br/>
    /// 文件打开失败
    /// </summary>
    public class FileOpenFailException : FileException
    {
        /// <summary>
        /// File opening failure<br/>
        /// 文件打开失败
        /// </summary>
        public FileOpenFailException(string message) : base(message) { }
    }

    /// <summary>
    /// File read failure<br/>
    /// 文件读取失败
    /// </summary>
    public class FileReadingFailException : FileException
    {
        /// <summary>
        /// File read failure<br/>
        /// 文件读取失败
        /// </summary>
        public FileReadingFailException(string message) : base(message) { }
    }

    /// <summary>
    /// File not found<br/>
    /// 文件未找到
    /// </summary>
    public class FileNotFoundException : FileException
    {
        /// <summary>
        /// File not found<br/>
        /// 文件未找到
        /// </summary>
        public FileNotFoundException(string message) : base(message) { }
    }

    /// <summary>
    /// The file path is null or space<br/>
    /// 文件路径为null或空白
    /// </summary>
    public class FilePathIsNullOrWhiteSpace : FileException
    {
        /// <summary>
        /// The file path is null or space<br/>
        /// 文件路径为null或空白
        /// </summary>
        public FilePathIsNullOrWhiteSpace(string message) : base(message) { }
    }
}
