﻿#if NET8_0_OR_GREATER
using System;
using System.Threading;
using TYLDDB.Basic;

namespace TYLDDB.Utils.FastCache.TDCache
{
    /// <summary>
    /// Three-valued dictionary cache, based on concurrent dictionary as core, combined with semaphore based thread lock to achieve high concurrency stability.<br />
    /// 三值字典缓存，以并发词典为核，结合基于信号量的线程锁实现高并发的稳定性。
    /// </summary>
    public class TripleDictionaryCache : ITripleDictionaryCache, IDisposable
    {
        private TripleDictionary<object> dictionary;
        private readonly SemaphoreSlim semaphore;  // 控制并发访问

        /// <summary>
        /// Three-valued dictionary cache, based on concurrent dictionary as core, combined with semaphore based thread lock to achieve high concurrency stability.<br />
        /// 三值字典缓存，以并发词典为核，结合基于信号量的线程锁实现高并发的稳定性。
        /// </summary>
        public TripleDictionaryCache()
        {
            dictionary = new TripleDictionary<object>();
            semaphore = new SemaphoreSlim(1, 1);  // 使用信号量来同步
        }

        /// <summary>
        /// Call <see cref="Clear"/> directly.<br />
        /// 直接调用<see cref="Clear"/>。
        /// </summary>
        public void Dispose()
        {
            Clear();
        }

        /// <summary>
        /// Clear all cache entries.<br />
        /// 清空所有缓存项。
        /// </summary>
        public void Clear()
        {
            lock (semaphore)
            {
                dictionary = new TripleDictionary<object>();
            }
        }

        /// <summary>
        /// Synchronization method: Obtain the corresponding value by key.<br />
        /// 同步方法：根据键获取对应的值。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key<br />键</param>
        /// <returns>Value<br />值</returns>
        public object Get(string type, string key)
        {
            lock (semaphore)
            {
                return dictionary.Get(type, key);
            }
        }

        /// <summary>
        /// Remove a cache entry by its key.<br />
        /// 根据键移除缓存项。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key<br />键</param>
        /// <returns>Whether the removal is successful.<br />移除操作是否成功。</returns>
        public bool Remove(string type, string key)
        {
            lock (semaphore)
            {
                return dictionary.RemoveKey(type, key);
            }
        }

        /// <summary>
        /// Set a cache entry for a specified key.<br />
        /// 为指定键设置缓存项。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key<br />键</param>
        /// <param name="value">Value<br />值</param>
        /// <returns>Whether the operation is successful.<br />操作是否成功。</returns>
        public bool Set(string type, string key, object value)
        {
            lock (semaphore)
            {
                return dictionary.Add(type, key, value);
            }
        }
    }
}
#endif