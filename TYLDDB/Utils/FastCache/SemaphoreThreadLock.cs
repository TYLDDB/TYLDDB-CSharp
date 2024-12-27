﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TYLDDB.Utils.FastCache
{
    /// <summary>
    /// Use semaphore based thread locks to achieve high concurrency stability.<br />
    /// 使用基于信号量的线程锁来实现高并发的稳定性。
    /// </summary>
    public class SemaphoreThreadLock : ICache
    {
        private readonly Dictionary<string, string> keyValueDict;   // 存储键->值映射
        private readonly Dictionary<string, HashSet<string>> valueKeyDict; // 存储值->键的映射
        private readonly SemaphoreSlim semaphore;  // 控制并发访问

        /// <summary>
        /// Use cached key-value pairs for fast reads and writes.<br />
        /// 使用缓存的键值对来快速读写。
        /// </summary>
        public SemaphoreThreadLock()
        {
            keyValueDict = new Dictionary<string, string>();
            valueKeyDict = new Dictionary<string, HashSet<string>>();
            semaphore = new SemaphoreSlim(1, 1);  // 使用信号量来同步
        }

        /// <summary>
        /// Synchronization method: Obtain the corresponding value by key.<br />
        /// 同步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public string GetByKey(string key)
        {
            lock (semaphore)
            {
                keyValueDict.TryGetValue(key, out var value);
                return value;
            }
        }

        /// <summary>
        /// Asynchronous method: Obtains the corresponding value based on the key.<br />
        /// 异步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public async Task<string> GetByKeyAsync(string key)
        {
            await semaphore.WaitAsync();
            try
            {
                keyValueDict.TryGetValue(key, out var value);
                return value;
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
        public List<string> GetKeysByValue(string value)
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
        public async Task<List<string>> GetKeysByValueAsync(string value)
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
        /// 同步方法：设置一个键值对。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, string value)
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
        /// 异步方法：设置一个键值对。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, string value)
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
        /// 同步方法：移除一个键值对。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveByKey(string key)
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
        /// 异步方法：移除一个键值对。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RemoveByKeyAsync(string key)
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
        /// 同步方法：清空缓存。
        /// </summary>
        public void Clear()
        {
            lock (semaphore)
            {
                keyValueDict.Clear();
                valueKeyDict.Clear();
            }
        }

        /// <summary>
        /// 异步方法：清空缓存。
        /// </summary>
        /// <returns></returns>
        public async Task ClearAsync()
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
        public Dictionary<string, string> GetAllCache()
        {
            lock (semaphore)
            {
                // 返回完整的键值对字典
                return new Dictionary<string, string>(keyValueDict);
            }
        }

        /// <summary>
        /// Gets all key-value pairs.<br />
        /// 获取所有的键值对。
        /// </summary>
        /// <returns>Key-value pair<br />键值对</returns>
        public async Task<Dictionary<string, string>> GetAllCacheAsync()
        {
            await semaphore.WaitAsync();
            try
            {
                // 返回完整的键值对字典
                return new Dictionary<string, string>(keyValueDict);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
