using System;

namespace TTYPDB.Basic
{
#if NET8_0
    /// <summary>
    /// The main error module, most of them use this<br/>
    /// 主要的错误模块，大部分都使用这个
    /// </summary>
    public class TtypdbException(string message) : Exception(message) { }
    /// <summary>
    /// File opening failure<br/>
    /// 文件打开失败
    /// </summary>
    public class FileOpenFailException(string message) : TtypdbException(message) { }
    /// <summary>
    /// File read failure<br/>
    /// 文件读取失败
    /// </summary>
    public class FileReadingFailException(string message) : TtypdbException(message) { }
    /// <summary>
    /// File not found<br/>
    /// 文件未找到
    /// </summary>
    public class FileNotFoundException(string message) : TtypdbException(message) { }
    /// <summary>
    /// The file path is null or space<br/>
    /// 文件路径为null或空白
    /// </summary>
    public class FilePathIsNullOrWhiteSpace(string message) : TtypdbException(message) { }
    /// <summary>
    /// Incorrect or invalid data<br/>
    /// 错误或无效的数据
    /// </summary>
    public class InvalidDataException(string message) : TtypdbException(message) { }
    /// <summary>
    /// Database not found<br/>
    /// 未找到数据库
    /// </summary>
    public class DatabaseNotFoundException(string message) : TtypdbException(message) { }
#else
    /// <summary>
    /// The main error module, most of them use this<br/>
    /// 主要的错误模块，大部分都使用这个
    /// </summary>
    public class TtypdbException : Exception
    {
        public TtypdbException(string message) : base(message) { }
    }
    /// <summary>
    /// File opening failure<br/>
    /// 文件打开失败
    /// </summary>
    public class FileOpenFailException : TtypdbException
    {
        public FileOpenFailException(string message) : base(message) { }
    }
    /// <summary>
    /// File read failure<br/>
    /// 文件读取失败
    /// </summary>
    public class FileReadingFailException : TtypdbException
    {
        public FileReadingFailException(string message) : base(message) { }
    }
    /// <summary>
    /// File not found<br/>
    /// 文件未找到
    /// </summary>
    public class FileNotFoundException : TtypdbException
    {
        public FileNotFoundException(string message) : base(message) { }
    }
    /// <summary>
    /// The file path is null or space<br/>
    /// 文件路径为null或空白
    /// </summary>
    public class FilePathIsNullOrWhiteSpace : TtypdbException
    {
        public FilePathIsNullOrWhiteSpace(string message) : base(message) { }
    }
    /// <summary>
    /// Incorrect or invalid data<br/>
    /// 错误或无效的数据
    /// </summary>
    public class InvalidDataException : TtypdbException
    {
        public InvalidDataException(string message) : base(message) { }
    }
    /// <summary>
    /// Database not found<br/>
    /// 未找到数据库
    /// </summary>
    public class DatabaseNotFoundException : TtypdbException
    {
        public DatabaseNotFoundException(string message) : base(message) { }
    }
#endif
}
