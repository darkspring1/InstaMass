using System;
using System.Threading.Tasks;
using SM.Common.Cache;

namespace SM.Domain.Persistent.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        readonly IEntityFrameworkDataContext _context;
        readonly ICacheProvider _cacheProvider;
        public EfUnitOfWork(IEntityFrameworkDataContext context, ICacheProvider cacheProvider)
        {
            _context = context;
            _cacheProvider = cacheProvider;
            UserRepository = new UserRepository(cacheProvider, context);
            ApplicationRepository = new ApplicationRepository(cacheProvider, context);
            RefreshTokenRepository = new RefreshTokenRepository(cacheProvider, context);
            AccountRepository = new AccountRepository(cacheProvider, context);
            TaskRepository = new TaskRepository(cacheProvider, context);
            TagTaskRepository = new TagTaskRepository(cacheProvider, context);
        }

        public IUserRepository UserRepository { get; }

        public IApplicationRepository ApplicationRepository { get; }

        public IRefreshTokenRepository RefreshTokenRepository { get; }

        public IAccountRepository AccountRepository { get; }

        public ITaskRepository TaskRepository { get; }

        public ITagTaskRepository TagTaskRepository { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public Task CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public IUnitOfWork CreateNewInstance()
        {
            return new EfUnitOfWork(_context.CreateNewInstance(), _cacheProvider);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}