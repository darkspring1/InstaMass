using System;
using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;

namespace SM.Domain.Persistent.EF
{
    public abstract class CacheRepository
    {
        readonly TimeSpan _cacheTime = TimeSpan.FromMinutes(5);
        readonly ICacheProvider _cacheProvider;

        protected readonly DataContext Context;

        public CacheRepository(ICacheProvider cacheProvider, DataContext context)
        {
            _cacheProvider = cacheProvider;
            Context = context;
        }


        protected Task<T> AddOrGetExistingAsync<T>(string key, Func<Task<T>> action)
        {
            return _cacheProvider.AddOrGetExistingAsync(key, action, _cacheTime);
        }

        protected T AddOrGetExisting<T>(string key, Func<T> action)
        {
            return _cacheProvider.AddOrGetExisting(key, action, _cacheTime);
        }

        protected void RemoveFromCache(string key)
        {
            _cacheProvider.Remove(key);
        }


        protected T[] AddOrGetExistingEntities<T>(string key) where T : class
        {
            return AddOrGetExisting(key, () => Context.Set<T>().ToArray());
        }
    }
}