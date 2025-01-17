namespace TYLDDB.Basic.Exception
{
    /// <summary>
    /// The main error module, most of them use this<br/>
    /// 主要的错误模块，大部分都使用这个
    /// </summary>
    public class TylddbException : System.Exception
    {
        /// <summary>
        /// The main error module, most of them use this<br/>
        /// 主要的错误模块，大部分都使用这个
        /// </summary>
        public TylddbException(string message) : base(message) { }
    }

    /// <summary>
    /// Error module about file.<br />
    /// 有关文件的错误模块。
    /// </summary>
    public class FileException : TylddbException
    {
        /// <summary>
        /// Error module about file.<br />
        /// 有关文件的错误模块。
        /// </summary>
        public FileException(string message) : base(message) { }
    }

    /// <summary>
    /// Error module about write file function.<br />
    /// 有关写入文件功能的错误模块。
    /// </summary>
    public class WriteException : TylddbException
    {
        /// <summary>
        /// Error module about write file function.<br />
        /// 有关写入文件功能的错误模块。
        /// </summary>
        public WriteException(string message) : base(message) { }
    }

    /// <summary>
    /// Error module about write file function.<br />
    /// 有关写入文件功能的错误模块。
    /// </summary>
    public class ReadException : TylddbException
    {
        /// <summary>
        /// Error module about write file function.<br />
        /// 有关写入文件功能的错误模块。
        /// </summary>
        public ReadException(string message) : base(message) { }
    }

    /// <summary>
    /// Error module about database.<br />
    /// 有关数据库的错误模块。
    /// </summary>
    public class DatabaseException : TylddbException
    {
        /// <summary>
        /// Error module about database.<br />
        /// 有关数据库的错误模块。
        /// </summary>
        public DatabaseException(string message) : base(message) { }
    }

    /// <summary>
    /// Dictionary exception class.<br />
    /// 字典的错误类。
    /// </summary>
    public class DictionaryException : TylddbException
    {
        /// <summary>
        /// Dictionary exception class.<br />
        /// 字典的错误类。
        /// </summary>
        public DictionaryException(string message) : base(message) { }
    }

    // 未分类的错误
    /// <summary>
    /// Incorrect or invalid data<br/>
    /// 错误或无效的数据
    /// </summary>
    public class InvalidDataException : TylddbException
    {
        /// <summary>
        /// Incorrect or invalid data<br/>
        /// 错误或无效的数据
        /// </summary>
        public InvalidDataException(string message) : base(message) { }
    }
}
