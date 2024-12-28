using System.Collections.Generic;
using System.Threading.Tasks;

namespace TYLDDB.Utils.FastCache
{
    /// <summary>
    /// Use cached key-value pairs for fast reads and writes.<br />
    /// 使用缓存的键值对来快速读写。
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Synchronization method: Obtain the corresponding value by key.<br />
        /// 同步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        string GetByKey(string key);

        /// <summary>
        /// Asynchronous method: Obtains the corresponding value based on the key.<br />
        /// 异步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        Task<string> GetByKeyAsync(string key);

        /// <summary>
        /// Synchronization method: Obtains one or more keys according to the value.<br />
        /// 同步方法：根据值获取对应的一个或多个键。
        /// </summary>
        /// <param name="value">Value<br />值</param>
        /// <returns>Key (List)<br />键 (List)</returns>
        List<string> GetKeysByValue(string value);

        /// <summary>
        /// Asynchronous method: Get one or more keys based on the value.<br />
        /// 异步方法：根据值获取对应的一个或多个键。
        /// </summary>
        /// <param name="value">Value<br />值</param>
        /// <returns>Key (List)<br />键 (List)</returns>
        Task<List<string>> GetKeysByValueAsync(string value);

        /// <summary>
        /// Set a key-value pair.<br />
        /// 设置一个键值对。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the Settings are successful.<br />是否设置成功。</returns>
        bool Set(string key, string value);

        /// <summary>
        /// Set a key-value pair.<br />
        /// 设置一个键值对。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the Settings are successful.<br />是否设置成功。</returns>
        Task<bool> SetAsync(string key, string value);

        /// <summary>
        /// Remove a key-value pair.<br />
        /// 移除一个键值对。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Whether the removal was successful or not.<br />是否移除成功。</returns>
        bool RemoveByKey(string key);

        /// <summary>
        /// Remove a key-value pair.<br />
        /// 移除一个键值对。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Whether the removal was successful or not.<br />是否移除成功。</returns>
        Task<bool> RemoveByKeyAsync(string key);

        /// <summary>
        /// Clear the cache.<br />
        /// 清空缓存。
        /// </summary>
        void Clear();

        /// <summary>
        /// Clear the cache.<br />
        /// 清空缓存。
        /// </summary>
        Task ClearAsync();

        /// <summary>
        /// Gets all key-value pairs.<br />
        /// 获取所有的键值对。
        /// </summary>
        /// <returns>Key-value pair<br />键值对</returns>
        Dictionary<string, string> GetAllCache();

        /// <summary>
        /// Gets all key-value pairs.<br />
        /// 获取所有的键值对。
        /// </summary>
        /// <returns>Key-value pair<br />键值对</returns>
        Task<Dictionary<string, string>> GetAllCacheAsync();
    }
}
