using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SM.Common.Cache;
using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;

namespace SM.Domain.Persistent.EF
{
    class AuthTokenRepository : BaseRepository<Model.AuthToken, State.AuthTokenState>, IAuthTokenRepository
    {
        public AuthTokenRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        public void AddNewToken(AuthToken token)
        {
            Set.Add(token.State);
        }

        public Task<AuthToken[]> GetBySubjectAsync(string subject)
        {
             return CreateArrayAsync(Set.Entities.Where(t => t.Subject == subject).ToArrayAsync());
        }

        public void Remove(params AuthToken[] tokens)
        {
            Set.RemoveRange(tokens.Select(t => t.State).ToArray());
        }

        protected override AuthToken Create(AuthTokenState state)
        {
            return new AuthToken(state);
        }
    }
}
