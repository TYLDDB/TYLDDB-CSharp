using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TYLDDB.Utils.FastCache.ConcurrentDictionary
{
    /// <summary>
    /// Use concurrent dictionaries to achieve high concurrency stability.<br />
    /// 使用并发词典来实现高并发的稳定性。
    /// </summary>
    public abstract class CdBooleanDictionary
    {
        /// <summary>
        /// Thread-safe dictionary to store cache data.<br />
        /// 线程安全的字典，用于存储缓存数据。
        /// </summary>
        private readonly ConcurrentDictionary<string, bool> _cache = new ConcurrentDictionary<string, bool>();

        /// <summary>
        /// Synchronization method: Obtain the corresponding value by key.<br />
        /// 同步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public virtual bool? GetByKey(string key)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// Asynchronous method: Obtain the corresponding value by key.<br />
        /// 同步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public virtual async Task<bool?> GetByKeyAsync(string key) => await Task.FromResult(GetByKey(key));

        /// <summary>
        /// Get a list of keys that correspond to a specific value.<br />
        /// 获取与指定值对应的所有键的列表。
        /// </summary>
        /// <param name="value">Value to match<br />要匹配的值</param>
        /// <returns>List of keys<br />键的列表</returns>
        public virtual List<string> GetKeysByValue(bool value)
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
        /// Get a list of keys that correspond to a specific value.<br />
        /// 获取与指定值对应的所有键的列表。
        /// </summary>
        /// <param name="value">Value to match<br />要匹配的值</param>
        /// <returns>List of keys<br />键的列表</returns>
        public virtual async Task<List<string>> GetKeysByValueAsync(bool value) => await Task.FromResult(GetKeysByValue(value));

        /// <summary>
        /// Set a cache entry for a specified key.<br />
        /// 为指定键设置缓存项。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the operation is successful.<br />操作是否成功。</returns>
        public virtual bool Set(string key, bool value)
        {
            // Using AddOrUpdate to ensure atomic insert or update operation
            _cache.AddOrUpdate(key, value, (existingKey, existingValue) => value);
            return true;
        }

        /// <summary>
        /// Set a cache entry for a specified key.<br />
        /// 为指定键设置缓存项。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the operation is successful.<br />操作是否成功。</returns>
        public virtual async Task<bool> SetAsync(string key, bool value) => await Task.FromResult(Set(key, value));

        /// <summary>
        /// Remove a cache entry by its key.<br />
        /// 根据键移除缓存项。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Whether the removal is successful.<br />移除操作是否成功。</returns>
        public virtual bool RemoveByKey(string key) => _cache.TryRemove(key, out _);

        /// <summary>
        /// Remove a cache entry by its key.<br />
        /// 根据键移除缓存项。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Whether the removal is successful.<br />移除操作是否成功。</returns>
        public virtual async Task<bool> RemoveByKeyAsync(string key) => await Task.FromResult(RemoveByKey(key));

        /// <summary>
        /// Clear all cache entries.<br />
        /// 清空所有缓存项。
        /// </summary>
        public virtual void Clear() => _cache.Clear();

        /// <summary>
        /// Clear all cache entries.<br />
        /// 清空所有缓存项。
        /// </summary>
        public virtual async Task ClearAsync() => await Task.Run(() => Clear());

        /// <summary>
        /// Get all cache entries as a dictionary.<br />
        /// 获取所有缓存项，返回字典。
        /// </summary>
        /// <returns>All cache entries as a dictionary.<br />所有缓存项的字典。</returns>
        public virtual Dictionary<string, bool> GetAllCache() => new Dictionary<string, bool>(_cache);
    }
}
