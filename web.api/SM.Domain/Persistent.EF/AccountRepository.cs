using System;
using System.Threading.Tasks;
using SM.Common.Cache;
using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SM.Domain.Persistent.EF
{
    class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(ICacheProvider cacheProvider, DataContext context) : base(cacheProvider, context)
        {
        }

        public void CreateNewAccount(Account account)
        {
            Set.Add(account);
        }

        public async Task<Account[]> FindByUserAsync(Guid userId)
        {
            return await Set.Where(a => a.UserId == userId).ToArrayAsync();
        }

        Task<Account> FindByLogin(string login)
        {
            return Set.Where(a => a.Login == login).FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistAsync(string login)
        {
            var state = await FindByLogin(login);
            return state != null;
        }

        public Task<Account> GetByIdAsync(Guid accountId)
        {
            return Set.Where(a => a.Id == accountId).FirstAsync();
        }
    }
}
