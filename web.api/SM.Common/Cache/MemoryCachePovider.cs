using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace SM.Common.Cache
{
    public class MemoryCacheProvider : ICacheProvider, IDisposable
    {
        MemoryCache _cache;
        private static object _root = new object();
        public MemoryCacheProvider()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        T AddOrGetExisting<T>(string key, Func<T> action, Func<T, bool> addCondition, TimeSpan duration)
        {

            T item = _cache.Get<T>(key);

            Func<T, bool> condition = it => it == null || (addCondition != null && addCondition(it));

            if (condition(item))
            {
                lock (_root)
                {
                    item = _cache.Get<T>(key);
                    if (condition(item))
                    {

                        item = action();
                        _cache.Set<T>(key, item, new DateTimeOffset(DateTime.Now.Add(duration)));
                    }
                }

            }
            return item;
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

        public void Dispose()
        {
            _cache.Dispose();
        }
    }
}
