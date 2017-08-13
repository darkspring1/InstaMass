using SM.Common.Cache;
using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Domain.Persistent.EF
{
    public class UserRepository : BaseRepository<User, UserState>, IUserRepository
    {
        public UserRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        public Task<User> GetByIdAsync(object key)
        {
            var id = (Guid)key;
            var stateTask = FirstOrDefaultAsync(Include(Set.Entities.Where(u => u.Id == id)));
            return CreateAsync(stateTask);
        }

        internal IQueryable<UserState> Include(IQueryable<UserState> users)
        {
            return users;
        }
        /*
        public User GetByToken(string token)
        {
            var tokens = Context.DbSet<TokenState>().Entities;
            var userStates = Set.Entities
                .Join(tokens, u => u.Id, t => t.UserId, (u, t) => new { User = u, Token = t })
                .Where(ut => ut.Token.Token == token && (ut.Token.TokenExpireAt == null || ut.Token.TokenExpireAt > DateTime.UtcNow))
                .Select(ut => ut.User);
            var user = Include(userStates).FirstOrDefault();

            if (user == null) { return null; }

            return new User(user);
        }*/

        protected override User Create(UserState state)
        {
            return state == null ? null : new User(state);
        }

        public void RegisterNewUser(User newUser)
        {
            Set.Add(newUser.State);
        }
    }
}