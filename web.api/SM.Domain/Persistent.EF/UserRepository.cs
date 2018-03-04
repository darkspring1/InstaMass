using Microsoft.EntityFrameworkCore;
using SM.Common.Cache;
using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Domain.Persistent.EF
{
    public class UserRepository : BaseRepository<User, User>, IUserRepository
    {
        public UserRepository(ICacheProvider cacheProvider, DataContext context) : base(cacheProvider, context)
        {
        }

        public Task<User> GetByIdAsync(object key)
        {
            var id = (Guid)key;
            var stateTask = FirstOrDefaultAsync(Include(Set.Where(u => u.Id == id)));
            return CreateAsync(stateTask);
        }

        internal IQueryable<User> Include(IQueryable<User> users)
        {
            return users;
        }

        IEnumerable<ExternalAuthProviderTypeState> GetExternalAuthProviderTypes()
        {
            const string key = "userRepository.GetExternalAuthProviderTypes";
            return AddOrGetExisting(key, () => Context.Set<ExternalAuthProviderTypeState>().ToArray());
        }

        ExternalAuthProviderTypeState GetExternalAuthProviderTypeByType(string type)
        {
            return GetExternalAuthProviderTypes().First();
        }

        protected override User Create(User state)
        {
            return state;
        }

        public void RegisterNewUser(User newUser)
        {
            if (newUser.ExternalAuthProviders.Any())
            {
                foreach (var p in newUser.ExternalAuthProviders)
                {
                    p.UserId = newUser.Id;
                    p.ExternalAuthProviderType = GetExternalAuthProviderTypeByType(p.ExternalAuthProviderType.Type);
                    p.ExternalAuthProviderTypeId = p.ExternalAuthProviderType.Id;
                }
            }
            Set.Add(newUser);
        }

        

        public Task<User> FindAsync(string email, string password)
        {
            var passwordHash = User.SHA(password);
            var user = FirstOrDefaultAsync(Set.Where(u => u.EmailStr == email && u.PasswordHash == passwordHash));
            return CreateAsync(user);
        }

        public Task<User> FindAsync(string email)
        {
            var user = FirstOrDefaultAsync(Set.Where(u => u.EmailStr == email));
            return CreateAsync(user);
        }
    }
}