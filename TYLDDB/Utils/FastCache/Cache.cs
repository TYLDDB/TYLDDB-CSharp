using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TYLDDB.Utils.FastCache
{
    /// <summary>
    /// For cached abstract classes, you need to inherit the class to do concrete implementation.<br />
    /// 对于缓存的抽象类，需要继承该类来做具体实现。
    /// </summary>
    public abstract class Cache : ICache
    {
        /// <inheritdoc/>
        public abstract void Clear();
        /// <inheritdoc/>
        public virtual async Task ClearAsync() => await Task.Run(() => Clear());
        /// <inheritdoc/>
        public abstract Dictionary<string, string> GetAllCache();
        /// <inheritdoc/>
        public virtual async Task<Dictionary<string, string>> GetAllCacheAsync() => await Task.FromResult(GetAllCache());
        /// <inheritdoc/>
        public abstract string GetByKey(string key);
        /// <inheritdoc/>
        public virtual async Task<string> GetByKeyAsync(string key) => await Task.FromResult(GetByKey(key));
        /// <inheritdoc/>
        public abstract List<string> GetKeysByValue(string value);
        /// <inheritdoc/>
        public virtual async Task<List<string>> GetKeysByValueAsync(string value) => await Task.FromResult(GetKeysByValue(value));
        /// <inheritdoc/>
        public abstract bool RemoveByKey(string key);
        /// <inheritdoc/>
        public virtual async Task<bool> RemoveByKeyAsync(string key) => await Task.FromResult(RemoveByKey(key));
        /// <inheritdoc/>
        public abstract bool Set(string key, string value);
        /// <inheritdoc/>
        public virtual async Task<bool> SetAsync(string key, string value) => await Task.FromResult(Set(key, value));
    }
}
