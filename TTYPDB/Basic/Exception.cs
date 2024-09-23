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
    /// Incorrect or invalid data<br/>
    /// 错误或无效的数据
    /// </summary>
    public class InvalidDataException(string message) : TtypdbException(message) { }
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
    /// Incorrect or invalid data<br/>
    /// 错误或无效的数据
    /// </summary>
    public class InvalidDataException : TtypdbException
    {
        public InvalidDataException(string message) : base(message) { }
    }
#endif
}
