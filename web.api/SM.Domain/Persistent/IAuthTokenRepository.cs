using SM.Domain.Model;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IAuthTokenRepository
    {
        Task<AuthToken[]> GetBySubjectAsync(string subject);

        void Remove(params AuthToken[] tokens);

        void AddNewToken(AuthToken token);
    }
}