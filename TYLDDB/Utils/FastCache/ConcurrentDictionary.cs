using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TYLDDB.Utils.FastCache
{
    /// <summary>
    /// Use thread locks based on concurrent dictionaries to achieve high concurrency stability.<br />
    /// 使用基于信号量的线程锁来实现高并发的稳定性。
    /// </summary>
    public class ConcurrentDictionary : ICache
    {
        /// <summary>
        /// Thread-safe dictionary to store cache data.<br />
        /// 线程安全的字典，用于存储缓存数据。
        /// </summary>
        private readonly ConcurrentDictionary<string, string> _cache = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Synchronization method: Obtain the corresponding value by key.<br />
        /// 同步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public string GetByKey(string key)
        {
            _cache.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// Asynchronous method to get the corresponding value by key.<br />
        /// 异步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public async Task<string> GetByKeyAsync(string key)
        {
            return await Task.FromResult(GetByKey(key));
        }

        /// <summary>
        /// Get a list of keys that correspond to a specific value.<br />
        /// 获取与指定值对应的所有键的列表。
        /// </summary>
        /// <param name="value">Value to match<br />要匹配的值</param>
        /// <returns>List of keys<br />键的列表</returns>
        public List<string> GetKeysByValue(string value)
        {
            var keys = new List<string>();
            foreach (var kvp in _cache)
            {
                if (kvp.Value == value)
                {
                    keys.Add(kvp.Key);
                }
            }
            return keys;
        }

        /// <summary>
        /// Asynchronous method to get a list of keys that correspond to a specific value.<br />
        /// 异步方法：获取与指定值对应的所有键的列表。
        /// </summary>
        /// <param name="value">Value to match<br />要匹配的值</param>
        /// <returns>List of keys<br />键的列表</returns>
        public async Task<List<string>> GetKeysByValueAsync(string value)
        {
            return await Task.FromResult(GetKeysByValue(value));
        }

        /// <summary>
        /// Set a cache entry for a specified key.<br />
        /// 为指定键设置缓存项。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the operation is successful.<br />操作是否成功。</returns>
        public bool Set(string key, string value)
        {
            // Using AddOrUpdate to ensure atomic insert or update operation
            _cache.AddOrUpdate(key, value, (existingKey, existingValue) => value);
            return true;
        }

        /// <summary>
        /// Asynchronous method to set a cache entry for a specified key.<br />
        /// 异步方法：为指定键设置缓存项。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the operation is successful.<br />操作是否成功。</returns>
        public async Task<bool> SetAsync(string key, string value)
        {
            return await Task.FromResult(Set(key, value));
        }

        /// <summary>
        /// Remove a cache entry by its key.<br />
        /// 根据键移除缓存项。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Whether the removal is successful.<br />移除操作是否成功。</returns>
        public bool RemoveByKey(string key)
        {
            return _cache.TryRemove(key, out _);
        }

        /// <summary>
        /// Asynchronous method to remove a cache entry by its key.<br />
        /// 异步方法：根据键移除缓存项。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Whether the removal is successful.<br />移除操作是否成功。</returns>
        public async Task<bool> RemoveByKeyAsync(string key)
        {
            return await Task.FromResult(RemoveByKey(key));
        }

        /// <summary>
        /// Clear all cache entries.<br />
        /// 清空所有缓存项。
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        /// <summary>
        /// Asynchronous method to clear all cache entries.<br />
        /// 异步方法：清空所有缓存项。
        /// </summary>
        /// <returns>Asynchronous task for clearing.<br />清空操作的异步任务。</returns>
        public async Task ClearAsync()
        {
            await Task.Run(() => Clear());
        }

        /// <summary>
        /// Get all cache entries as a dictionary.<br />
        /// 获取所有缓存项，返回字典。
        /// </summary>
        /// <returns>All cache entries as a dictionary.<br />所有缓存项的字典。</returns>
        public Dictionary<string, string> GetAllCache()
        {
            return new Dictionary<string, string>(_cache);
        }

        /// <summary>
        /// Asynchronous method to get all cache entries as a dictionary.<br />
        /// 异步方法：获取所有缓存项，返回字典。
        /// </summary>
        /// <returns>All cache entries as a dictionary.<br />所有缓存项的字典。</returns>
        public async Task<Dictionary<string, string>> GetAllCacheAsync()
        {
            return await Task.FromResult(GetAllCache());
        }
    }
}
