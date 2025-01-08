using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TYLDDB.Utils.FastCache.SemaphoreThreadLock
{
    /// <summary>
    /// Use semaphore based thread locks to achieve high concurrency stability.<br />
    /// 使用基于信号量的线程锁来实现高并发的稳定性。
    /// </summary>
    public class StlLongDictionary
    {
        private readonly Dictionary<string, long> keyValueDict;   // 存储键->值映射
        private readonly Dictionary<long, HashSet<string>> valueKeyDict; // 存储值->键的映射
        private readonly SemaphoreSlim semaphore;  // 控制并发访问

        /// <summary>
        /// Use cached key-value pairs for fast reads and writes.<br />
        /// 使用缓存的键值对来快速读写。
        /// </summary>
        public StlLongDictionary()
        {
            keyValueDict = new Dictionary<string, long>();
            valueKeyDict = new Dictionary<long, HashSet<string>>();
            semaphore = new SemaphoreSlim(1, 1);  // 使用信号量来同步
        }

        /// <summary>
        /// Synchronization method: Obtain the corresponding value by key.<br />
        /// 同步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public virtual long? GetByKey(string key)
        {
            lock (semaphore)
            {
                // 尝试从字典中获取值，如果键不存在则返回 null
                if (keyValueDict.TryGetValue(key, out var value))
                {
                    return value;  // 如果找到值，返回该值
                }

                return null;  // 如果没有找到对应的值，返回 null
            }
        }

        /// <summary>
        /// Asynchronous method: Obtains the corresponding value based on the key.<br />
        /// 异步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public virtual async Task<long?> GetByKeyAsync(string key)
        {
            await semaphore.WaitAsync();
            try
            {
                // 尝试从字典中获取值，如果键不存在则返回 null
                if (keyValueDict.TryGetValue(key, out var value))
                {
                    return value;  // 如果找到值，返回该值
                }

                return null;  // 如果没有找到对应的值，返回 null
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <summary>
        /// Synchronization method: Obtains one or more keys according to the value.<br />
        /// 同步方法：根据值获取对应的一个或多个键。
        /// </summary>
        /// <param name="value">Value<br />值</param>
        /// <returns>Key (List)<br />键 (List)</returns>
        public virtual List<string> GetKeysByValue(long value)
        {
            lock (semaphore)
            {
                if (valueKeyDict.ContainsKey(value))
                {
                    return valueKeyDict[value].ToList();
                }
                return new List<string>();
            }
        }

        /// <summary>
        /// Asynchronous method: Get one or more keys based on the value.<br />
        /// 异步方法：根据值获取对应的一个或多个键。
        /// </summary>
        /// <param name="value">Value<br />值</param>
        /// <returns>Key (List)<br />键 (List)</returns>
        public virtual async Task<List<string>> GetKeysByValueAsync(long value)
        {
            await semaphore.WaitAsync();
            try
            {
                if (valueKeyDict.ContainsKey(value))
                {
                    return valueKeyDict[value].ToList();
                }
                return new List<string>();
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <summary>
        /// Set a key-value pair.<br />
        /// 设置一个键值对。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the Settings are successful.<br />是否设置成功</returns>
        public virtual bool Set(string key, long value)
        {
            lock (semaphore)
            {
                if (keyValueDict.ContainsKey(key))
                {
                    return false; // 键已存在，不允许重复的键
                }

                keyValueDict[key] = value;

                if (!valueKeyDict.ContainsKey(value))
                {
                    valueKeyDict[value] = new HashSet<string>();
                }
                valueKeyDict[value].Add(key);
                return true;
            }
        }

        /// <summary>
        /// Set a key-value pair.<br />
        /// 设置一个键值对。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the Settings are successful.<br />是否设置成功</returns>
        public virtual async Task<bool> SetAsync(string key, long value)
        {
            await semaphore.WaitAsync();
            try
            {
                if (keyValueDict.ContainsKey(key))
                {
                    return false; // 键已存在，不允许重复的键
                }

                keyValueDict[key] = value;

                if (!valueKeyDict.ContainsKey(value))
                {
                    valueKeyDict[value] = new HashSet<string>();
                }
                valueKeyDict[value].Add(key);
                return true;
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <summary>
        /// Remove a key-value pair.<br />
        /// 移除一个键值对。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Check whether the removal is successful.<br />是否移除成功。</returns>
        public virtual bool RemoveByKey(string key)
        {
            lock (semaphore)
            {
                if (!keyValueDict.ContainsKey(key))
                {
                    return false;
                }
                var value = keyValueDict[key];
                keyValueDict.Remove(key);

                if (valueKeyDict.ContainsKey(value))
                {
                    valueKeyDict[value].Remove(key);
                    if (valueKeyDict[value].Count == 0)
                    {
                        valueKeyDict.Remove(value);
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Remove a key-value pair.<br />
        /// 移除一个键值对。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Check whether the removal is successful.<br />是否移除成功。</returns>
        public virtual async Task<bool> RemoveByKeyAsync(string key)
        {
            await semaphore.WaitAsync();
            try
            {
                if (!keyValueDict.ContainsKey(key))
                {
                    return false;
                }
                var value = keyValueDict[key];
                keyValueDict.Remove(key);

                if (valueKeyDict.ContainsKey(value))
                {
                    valueKeyDict[value].Remove(key);
                    if (valueKeyDict[value].Count == 0)
                    {
                        valueKeyDict.Remove(value);
                    }
                }
                return true;
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <summary>
        /// Empty the cache.<br />
        /// 清空缓存。
        /// </summary>
        public virtual void Clear()
        {
            lock (semaphore)
            {
                keyValueDict.Clear();
                valueKeyDict.Clear();
            }
        }

        /// <summary>
        /// Empty the cache.<br />
        /// 清空缓存。
        /// </summary>
        public virtual async void ClearAsync()
        {
            await semaphore.WaitAsync();
            try
            {
                keyValueDict.Clear();
                valueKeyDict.Clear();
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <summary>
        /// Gets all key-value pairs.<br />
        /// 获取所有的键值对。
        /// </summary>
        /// <returns>Key-value pair<br />键值对</returns>
        public virtual Dictionary<string, long> GetAllCache()
        {
            lock (semaphore)
            {
                // 返回完整的键值对字典
                return new Dictionary<string, long>(keyValueDict);
            }
        }

        /// <summary>
        /// Gets all key-value pairs.<br />
        /// 获取所有的键值对。
        /// </summary>
        /// <returns>Key-value pair<br />键值对</returns>
        public virtual async Task<Dictionary<string, long>> GetAllCacheAsync()
        {
            await semaphore.WaitAsync();
            try
            {
                // 返回完整的键值对字典
                return new Dictionary<string, long>(keyValueDict);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
