using System.Collections.Generic;
using System;
using System.Collections.Concurrent;
using System.Linq;
using TYLDDB.Basic.Exception;

namespace TYLDDB.Basic
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Three-value dictionary.<br />
    /// 三值字典。
    /// </summary>
    /// <typeparam name="TValue">The data type of the value.<br />值的数据类型。</typeparam>
    public class TripleDictionary<TValue>
    {
        private readonly ConcurrentDictionary<Tuple<string, string>, TValue> _dictionary;

        /// <summary>
        /// Three-value dictionary.<br />
        /// 三值字典。
        /// </summary>
        public TripleDictionary()
        {
            _dictionary = new ConcurrentDictionary<Tuple<string, string>, TValue>();
        }

        /// <summary>
        /// Add an element, only if the key for the combination does not exist.<br />
        /// 添加元素，只有当该组合的键不存在时才添加。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key.<br />键。</param>
        /// <param name="value">Value.<br />值。</param>
        /// <returns>Whether the value is added successfully.<br />是否成功添加。</returns>
        public bool Add(string type, string key, TValue value)
        {
            var keyTuple = new Tuple<string, string>(type, key);

            // 检查是否已经存在相同的键组合
            if (_dictionary.ContainsKey(keyTuple))
            {
                return false; // 如果已存在相同的键组合，返回 false 表示添加失败
            }

            // 如果没有该键组合，执行添加操作
            _dictionary[keyTuple] = value;
            return true; // 添加成功
        }

        /// <summary>
        /// Only values are updated, types and keys cannot be updated.<br />
        /// 只更新值，不能更新类型和键。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key.<br />键。</param>
        /// <param name="newValue">New value.<br />新值。</param>
        /// <returns>Whether the update is successful.<br />是否成功更新。</returns>
        public bool UpdateValue(string type, string key, TValue newValue)
        {
            var keyTuple = new Tuple<string, string>(type, key);

            // 检查该键组合是否存在
            if (!_dictionary.ContainsKey(keyTuple))
            {
                return false; // 如果不存在，返回 false
            }

            // 如果存在，更新值
            _dictionary[keyTuple] = newValue;
            return true; // 更新成功
        }

        /// <summary>
        /// Get the element.<br />
        /// 获取元素。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key.<br />键。</param>
        /// <returns>Value.<br />值。</returns>
        /// <exception cref="TripleDictionaryKeyNotFoundException">The specified key was not found.<br />未找到指定的键。</exception>
        public TValue Get(string type, string key)
        {
            var keyTuple = new Tuple<string, string>(type, key);
            if (_dictionary.TryGetValue(keyTuple, out TValue value))
            {
                return value;
            }
            else
            {
                throw new TripleDictionaryKeyNotFoundException("The key combination was not found.");
            }
        }

        /// <summary>
        /// Checks whether the specified type and key combination are included.<br />
        /// 检查是否包含指定的类型和键组合。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key.<br />键。</param>
        /// <returns>Whether the key is included.<br />是否包含该键。</returns>
        public bool ContainsKey(string type, string key) => _dictionary.ContainsKey(new Tuple<string, string>(type, key));

        /// <summary>
        /// Deleting a certain type removes all the key values of the class.<br />
        /// 删除某个类型会删除该类型下的所有键值对。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <returns>Whether the data type is removed successfully.<br />是否成功移除该数据类型。</returns>
        public bool RemoveType(string type)
        {
            var keysToRemove = _dictionary.Keys.Where(k => k.Item1 == type).ToList();

            if (keysToRemove.Count == 0)
            {
                return false; // 如果该类型没有键值对，返回 false
            }

            foreach (var key in keysToRemove)
            {
                _dictionary.TryRemove(key, out _);
            }
            return true;
        }

        /// <summary>
        /// Deletes the specified key and corresponding value from the specified type.<br />
        /// 删除指定类型中的指定键和对应的值。
        /// </summary>
        /// <param name="type">Data type.<br />数据类型。</param>
        /// <param name="key">Key.<br />键。</param>
        /// <returns>Whether the key is successfully removed.<br />是否成功移除该键。</returns>
        public bool RemoveKey(string type, string key)
        {
            var keyTuple = new Tuple<string, string>(type, key);

            // 尝试移除指定的键值对
            if (_dictionary.TryRemove(keyTuple, out _))
            {
                return true; // 删除成功
            }
            else
            {
                return false; // 如果没有找到该键组合，返回 false
            }
        }

        /// <summary>
        /// Removes the specified key and corresponding value from all types.<br />
        /// 删除所有类型中的指定键和对应的值。
        /// </summary>
        /// <param name="key">Key.<br />键。</param>
        /// <returns>Whether the key is successfully removed.<br />是否成功移除该键。</returns>
        public bool RemoveKey(string key)
        {
            bool removed = false;

            // 查找所有包含该 Key 的项，并删除它们
            var keysToRemove = _dictionary.Where(entry => entry.Key.Item2 == key)
                                          .Select(entry => entry.Key)
                                          .ToList();

            foreach (var keyTuple in keysToRemove)
            {
                if (_dictionary.TryRemove(keyTuple, out _))
                {
                    removed = true;
                }
            }

            return removed; // 如果至少删除了一个键值对，返回 true
        }

        /// <summary>
        /// Prints all types of key-value pairs.<br />
        /// 打印所有类型的键值对。
        /// </summary>
        public void PrintAll()
        {
            foreach (var entry in _dictionary)
            {
                Console.WriteLine($"Type: {entry.Key.Item1}, Key: {entry.Key.Item2} -> Value: {entry.Value}");
            }
        }
    }
#endif
}
