namespace TYLDDB.Basic.Exception
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// An exception class to the three-value dictionary.<br />
    /// 三值字典的例外类。
    /// </summary>
    public class TripleDictionaryException(string message) : DictionaryException(message) { }

    /// <summary>
    /// The specified key was not found.<br />
    /// 未找到指定的键。
    /// </summary>
    public class TripleDictionaryKeyNotFoundException(string message) : TripleDictionaryException(message) { }
#endif
}
