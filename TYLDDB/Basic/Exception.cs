using System;

namespace TYLDDB.Basic
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// The main error module, most of them use this<br/>
    /// 主要的错误模块，大部分都使用这个
    /// </summary>
    public class TylddbException(string message) : Exception(message) { }
    /// <summary>
    /// File opening failure<br/>
    /// 文件打开失败
    /// </summary>
    public class FileOpenFailException(string message) : TylddbException(message) { }
    /// <summary>
    /// File read failure<br/>
    /// 文件读取失败
    /// </summary>
    public class FileReadingFailException(string message) : TylddbException(message) { }
    /// <summary>
    /// File not found<br/>
    /// 文件未找到
    /// </summary>
    public class FileNotFoundException(string message) : TylddbException(message) { }
    /// <summary>
    /// The file path is null or space<br/>
    /// 文件路径为null或空白
    /// </summary>
    public class FilePathIsNullOrWhiteSpace(string message) : TylddbException(message) { }
    /// <summary>
    /// Incorrect or invalid data<br/>
    /// 错误或无效的数据
    /// </summary>
    public class InvalidDataException(string message) : TylddbException(message) { }
    /// <summary>
    /// Database not found<br/>
    /// 未找到数据库
    /// </summary>
    public class DatabaseNotFoundException(string message) : TylddbException(message) { }
    /// <summary>
    /// Description Failed to obtain the database content<br/>
    /// 数据库内容获取失败
    /// </summary>
    public class GetDatabaseContentErrorException(string message) : TylddbException(message) { }
#else
    /// <summary>
    /// The main error module, most of them use this<br/>
    /// 主要的错误模块，大部分都使用这个
    /// </summary>
    public class TylddbException : Exception
    {
        /// <summary>
        /// The main error module, most of them use this<br/>
        /// 主要的错误模块，大部分都使用这个
        /// </summary>
        public TylddbException(string message) : base(message) { }
    }
    /// <summary>
    /// File opening failure<br/>
    /// 文件打开失败
    /// </summary>
    public class FileOpenFailException : TylddbException
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
    public class FileReadingFailException : TylddbException
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
    public class FileNotFoundException : TylddbException
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
    public class FilePathIsNullOrWhiteSpace : TylddbException
    {
        /// <summary>
        /// The file path is null or space<br/>
        /// 文件路径为null或空白
        /// </summary>
        public FilePathIsNullOrWhiteSpace(string message) : base(message) { }
    }
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
#endif
}
