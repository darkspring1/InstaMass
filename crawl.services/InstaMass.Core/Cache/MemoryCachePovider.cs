using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace InstaMfl.Core.Cache
{
    public class MemoryCacheProvider : ICacheProvider, IDisposable
    {
        MemoryCache _cache;
        private static object _root = new object();
        public MemoryCacheProvider()
        {
            _cache = new MemoryCache("MemoryCachePovider");
        }

        T AddOrGetExisting<T>(string key, Func<T> action, Func<T, bool> addCondition, TimeSpan duration)
        {

            var item = _cache.GetCacheItem(key);

            Func<CacheItem, bool> condition = (it) => it == null || (addCondition != null && addCondition((T)it.Value));

            if (condition(item))
            {
                lock (_root)
                {
                    item = _cache.GetCacheItem(key);
                    if (condition(item))
                    {
                        item = new CacheItem(key, action());
                        _cache.Add(item.Key, item.Value, new DateTimeOffset(DateTime.Now.Add(duration)));
                    }
                }

            }
            return (T)item.Value;
        }

        public T AddOrGetExisting<T>(string key, Func<T> action, TimeSpan duration)
        {
            return AddOrGetExisting(key, action, null, duration);
        }

        public Task<T> AddOrGetExistingAsync<T>(string key, Func<Task<T>> action, TimeSpan duration)
        {
            Func<Task<T>, bool> condition = t => t.IsFaulted || t.IsCanceled;
            return AddOrGetExisting(key, action, condition, duration);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public bool Add<T>(string key, T value, TimeSpan duration)
        {
            return _cache.Add(key, value, new DateTimeOffset(DateTime.Now.Add(duration)));
        }

        public void Update<T>(string key, T value, TimeSpan duration)
        {
            Remove(key);
            Add(key, value, duration);
        }

        public void Dispose()
        {
            _cache.Dispose();
        }
    }
}
