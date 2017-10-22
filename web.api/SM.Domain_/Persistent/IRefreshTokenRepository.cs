using SM.Domain.Model;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByIdAsync(string key);

        Task RemoveAsync(string key);

        void AddNewTokenAsync(RefreshToken token);
    }
}