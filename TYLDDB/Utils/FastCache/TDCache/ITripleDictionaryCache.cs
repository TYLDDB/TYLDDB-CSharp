#if NET8_0_OR_GREATER
namespace TYLDDB.Utils.FastCache.TDCache
{
    /// <summary>
    /// Three-value dictionary interface.<br />
    /// 三值字典的接口。
    /// </summary>
    public interface ITripleDictionaryCache
    {
        /// <summary>
        /// Set a cache entry for a specified key.<br />
        /// 为指定键设置缓存项。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the operation is successful.<br />操作是否成功。</returns>
        public abstract bool? Set(string type, string key, object value);

        /// <summary>
        /// Synchronization method: Obtain the corresponding value by key.<br />
        /// 同步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public abstract bool? Get(string type, string key);

        /// <summary>
        /// Remove a cache entry by its key.<br />
        /// 根据键移除缓存项。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key">Key<br />键</param>
        /// <returns>Whether the removal is successful.<br />移除操作是否成功。</returns>
        public abstract bool? Remove(string type, string key);

        /// <summary>
        /// Clear all cache entries.<br />
        /// 清空所有缓存项。
        /// </summary>
        public abstract void Clear();
    }
}
#endif