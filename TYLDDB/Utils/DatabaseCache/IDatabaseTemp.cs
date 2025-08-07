using System.Threading.Tasks;

namespace TYLDDB.Utils.DatabaseCache
{
    internal interface IDatabaseTemp<T>
    {
        /// <summary>
        /// Synchronization method: Obtain the corresponding value by key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        T GetByKey(string key);

        /// <summary>
        /// Asynchronous method: Obtain the corresponding value by key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        ValueTask<T> GetByKeyAsync(string key);

        /// <summary>
        /// Get a list of keys that correspond to a specific value.
        /// </summary>
        /// <param name="value">Value to match</param>
        /// <returns>Keys</returns>
        string[] GetKeysByValue(T value);

        /// <summary>
        /// Get keys that correspond to a specific value.
        /// </summary>
        /// <param name="value">Value to match</param>
        /// <returns>Keys</returns>
        ValueTask<string[]> GetKeysByValueAsync(T value);

        /// <summary>
        /// Set a cache entry for a specified key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Whether the operation is successful.</returns>
        bool Add(string key, T value);

        /// <summary>
        /// Set or update a cache entry for a specified key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Whether the operation is successful.</returns>
        bool AddOrUpdate(string key, T value);

        /// <summary>
        /// Update a cache entry for a specified key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Whether the operation is successful.</returns>
        bool Update(string key, T value);

        /// <summary>
        /// Remove a cache entry by its key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Whether the removal is successful.</returns>
        bool RemoveByKey(string key);

        /// <summary>
        /// Remove a cache entry by its key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Whether the removal is successful.</returns>
        ValueTask<bool> RemoveByKeyAsync(string key);

        /// <summary>
        /// Clear all cache entries.
        /// </summary>
        void Clear();
    }
}
