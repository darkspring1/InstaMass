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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ICacheProvider cacheProvider, DataContext context) : base(cacheProvider, context)
        {
        }

        public Task<User> GetByIdAsync(object key)
        {
            var id = (Guid)key;
            return Set.Where(u => u.Id == id).FirstOrDefaultAsync();
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
            return Set
                .Where(u => u.EmailStr == email && u.PasswordHash == passwordHash)
                .FirstOrDefaultAsync();
        }

        public Task<User> FindAsync(string email)
        {
            return Set.Where(u => u.EmailStr == email).FirstOrDefaultAsync();
        }
    }
}