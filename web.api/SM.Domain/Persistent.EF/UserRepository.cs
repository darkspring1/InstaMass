using SM.Common.Cache;
using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System;
using System.Collections.Generic;
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

        IEnumerable<ExternalAuthProviderTypeState> GetExternalAuthProviderTypes()
        {
            const string key = "userRepository.GetExternalAuthProviderTypes";
            return AddOrGetExisting(key, () => Context.DbSet<ExternalAuthProviderTypeState>().Entities.ToArray());
        }

        ExternalAuthProviderTypeState GetExternalAuthProviderTypeByType(string type)
        {
            return GetExternalAuthProviderTypes().First();
        }

        protected override User Create(UserState state)
        {
            return state == null ? null : new User(state);
        }

        public void RegisterNewUser(User newUser)
        {
            if (newUser.State.ExternalAuthProviders.Any())
            {
                foreach (var p in newUser.State.ExternalAuthProviders)
                {
                    p.ExternalAuthProviderTypeId = GetExternalAuthProviderTypeByType(p.ExternalAuthProviderType.Type).Id;
                }
            }

            Set.Add(newUser.State);
        }

        public Task<User> FindAsync(ExternalAuthProviderType providerType, string externalUserId)
        {
            var externalAuthProviderStates = Context.DbSet<ExternalAuthProviderState>().Entities;
            var providerTypeStr = providerType.ToString().ToLower();
            var userState = FirstOrDefaultAsync(externalAuthProviderStates
                .Where(p => p.ExternalUserId == externalUserId && p.ExternalAuthProviderType.Type == providerTypeStr)
                .Include(p => p.User)
                .Include(p => p.User.ExternalAuthProviders)
                .Select(p => p.User));

            return CreateAsync(userState);
        }
    }
}