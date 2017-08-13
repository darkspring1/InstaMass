using System;
using System.Threading.Tasks;

namespace SM.Common.Cache
{
    public interface ICacheProvider
    {
        T AddOrGetExisting<T>(string key, Func<T> action, TimeSpan duration);

        Task<T> AddOrGetExistingAsync<T>(string key, Func<Task<T>> action, TimeSpan duration);

        void Remove(string key);
    }
}
