using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SM.Common.Cache;
using SM.Domain.Model;

namespace SM.Domain.Persistent.EF
{
    class AuthTokenRepository : BaseRepository<AuthToken>, IAuthTokenRepository
    {
        public AuthTokenRepository(ICacheProvider cacheProvider, DataContext context) : base(cacheProvider, context)
        {
        }

        public void AddNewToken(AuthToken token)
        {
            Set.Add(token);
        }

        public Task<AuthToken[]> GetBySubjectAsync(string subject)
        {
             return Set.Where(t => t.Subject == subject).ToArrayAsync();
        }

        public void Remove(params AuthToken[] tokens)
        {
            Set.RemoveRange(tokens);
        }
    }
}
