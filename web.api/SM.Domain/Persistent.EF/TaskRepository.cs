using SM.Domain.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;
using Microsoft.EntityFrameworkCore;

namespace SM.Domain.Persistent.EF
{
    class TaskRepository :  BaseRepository<SMTask>, ITaskRepository
    {
        public TaskRepository(ICacheProvider cacheProvider, DataContext context) : base(cacheProvider, context)
        {
        }

        public Task<SMTask[]> GetByUserAsync(Guid userId)
        {
            return  Set
                .Where(t => t.Account.UserId == userId)
                .Include(t => t.Account)
                .ToArrayAsync();

        }
    }
}
