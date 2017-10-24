using System;
using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;
using SM.Domain.Model;

namespace SM.Domain.Persistent.EF
{
    class RefreshTokenRepository : BaseRepository<Model.RefreshToken, State.RefreshTokenState>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        protected override Model.RefreshToken Create(State.RefreshTokenState state)
        {
            return state == null ? null : new Model.RefreshToken(state);
        }

        Task<RefreshToken> IRefreshTokenRepository.GetByIdAsync(string key)
        {
            var id = RefreshToken.GetHash(key);
            var stateTask = FirstOrDefaultAsync(Set.Entities.Where(a => a.Id == id));
            return CreateAsync(stateTask);
        }

        public async Task RemoveAsync(string key)
        {
            var id = RefreshToken.GetHash(key);
            var state = await Set.FindAsync(id);
            Set.Remove(state);
        }

        public void AddNewTokenAsync(RefreshToken token)
        {
            Set.Add(token.State);
        }
    }
}
