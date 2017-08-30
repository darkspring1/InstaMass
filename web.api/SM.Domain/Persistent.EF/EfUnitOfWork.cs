using System;
using System.Threading.Tasks;
using SM.Common.Cache;

namespace SM.Domain.Persistent.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        readonly IEntityFrameworkDataContext _context;
        public EfUnitOfWork(IEntityFrameworkDataContext context, ICacheProvider cacheProvider)
        {
            _context = context;
            UserRepository = new UserRepository(cacheProvider, context);
            ApplicationRepository = new ApplicationRepository(cacheProvider, context);
            RefreshTokenRepository = new RefreshTokenRepository(cacheProvider, context);
            AccountRepository = new AccountRepository(cacheProvider, context);
        }

        public IUserRepository UserRepository { get; }

        public IApplicationRepository ApplicationRepository { get; }

        public IRefreshTokenRepository RefreshTokenRepository { get; }

        public IAccountRepository AccountRepository { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public Task CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}