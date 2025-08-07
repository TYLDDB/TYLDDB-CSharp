using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TYLDDB.Basic.Exception;

namespace TYLDDB.Utils.DatabaseCache
{
    /// <summary>
    /// Concurrent dictionary cache.
    /// </summary>
    /// <typeparam name="T">string, int, short...</typeparam>
    public class ConcurrentDictionaryCache<T> : IDatabaseCache<T>
    {
        private readonly ConcurrentDictionary<string, T> _cache;

        /// <summary>
        /// Concurrent dictionary cache.
        /// </summary>
        public ConcurrentDictionaryCache()
        {
            _cache = new ConcurrentDictionary<string, T>();
        }

        /// <inheritdoc/>
        public void Clear() => _cache.Clear();

        /// <inheritdoc/>
        public T GetByKey(string key)
        {
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }
            throw new GetDatabaseContentErrorException("Failed to obtain the value. Please check the database");
        }

        /// <inheritdoc/>
        public ValueTask<T> GetByKeyAsync(string key)
        {
            if (_cache.TryGetValue(key, out var value))
                return ValueTask.FromResult(value);   // 同步路径：0 分配

            // 缓存未命中，抛异常也直接包成 ValueTask
            return ValueTask.FromException<T>(new GetDatabaseContentErrorException("Failed to obtain the value. Please check the database"));
        }

        /// <summary>
        /// Retrieve all matching keys based on the value.
        /// Note: ConcurrentDictionary can only perform fast forward(key→value) lookups;
        /// Reverse lookups require a linear scan, with a complexity of O(n).
        /// </summary>
        public string[] GetKeysByValue(T value)
        {
            /*
            if (value == null)                       // 可根据 T 是否为 class 调整
                return Array.Empty<string>();

            // 先估计结果数量，避免 List 多次扩容
            var result = new List<string>(_cache.Count / 4);

            foreach (var kv in _cache)
            {
                // 使用 EqualityComparer<T>.Default 以支持值类型/引用类型/null
                if (EqualityComparer<T>.Default.Equals(kv.Value, value))
                    result.Add(kv.Key);
            }

            return result.Count == 0
                ? Array.Empty<string>()
                : result.ToArray();
            */
            
            // /* 优化版方法 Come from deepseek
            // 提前获取比较器实例
            var comparer = EqualityComparer<T>.Default;
            var result = new List<string>(Math.Min(_cache.Count, 64)); // 更合理的初始容量

            foreach (var kv in _cache)
            {
                // 使用预获取的比较器进行高效比较
                if (comparer.Equals(kv.Value, value))
                    result.Add(kv.Key);
            }

            // 直接返回集合或空数组
            return result.Count > 0 ? result.ToArray() : Array.Empty<string>();
            // */
        }

        /// <inheritdoc/>
        public ValueTask<string[]> GetKeysByValueAsync(T value)
        {
            // 如果缓存为空，直接返回空数组（避免分配）
            if (_cache.Count == 0)
            {
                return ValueTask.FromResult(Array.Empty<string>());
            }

            var comparer = EqualityComparer<T>.Default;
            var result = new List<string>(Math.Min(_cache.Count, 64));

            // 同步处理（假设_cache是线程安全的）
            foreach (var kv in _cache)
            {
                if (comparer.Equals(kv.Value, value))
                    result.Add(kv.Key);
            }

            // 根据结果数量选择最优返回方式
            return result.Count switch
            {
                0 => ValueTask.FromResult(Array.Empty<string>()),
                1 => ValueTask.FromResult(new[] { result[0] }),  // 避免ToArray分配
                _ => ValueTask.FromResult(result.ToArray())
            };
        }

        /// <inheritdoc/>
        public bool RemoveByKey(string key) => _cache.TryRemove(key, out _);

        /// <inheritdoc/>
        public ValueTask<bool> RemoveByKeyAsync(string key) => new(_cache.TryRemove(key, out _));

        /// <summary>
        /// Get all cache entries as a dictionary.
        /// </summary>
        /// <returns>Dictionary</returns>
        public ConcurrentDictionary<string, T> GetAllCache() => _cache;

        /// <inheritdoc/>
        public bool Add(string key, T value) => _cache.TryAdd(key, value);

        /// <inheritdoc/>
        public bool AddOrUpdate(string key, T value)
        {
            _cache.AddOrUpdate(key, value, (existingKey, existingValue) => value);
            return true;
        }

        /// <inheritdoc/>
        public bool Update(string key, T value) => _cache.TryUpdate(key, value, _cache[key]);
    }
}
