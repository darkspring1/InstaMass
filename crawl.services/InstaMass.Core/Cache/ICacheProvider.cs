using System;
using System.Threading.Tasks;

namespace InstaMfl.Core.Cache
{
    public interface ICacheProvider
    {
        T AddOrGetExisting<T>(string key, Func<T> action, TimeSpan duration);

        Task<T> AddOrGetExistingAsync<T>(string key, Func<Task<T>> action, TimeSpan duration);

        bool Add<T>(string key, T value, TimeSpan duration);

        void Update<T>(string key, T value, TimeSpan duration);

        void Remove(string key);
    }
}
