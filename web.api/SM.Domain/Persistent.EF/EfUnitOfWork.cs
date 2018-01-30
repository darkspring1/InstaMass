using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SM.Common.Cache;

namespace SM.Domain.Persistent.EF
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        readonly IEntityFrameworkDataContext _context;
        readonly ICacheProvider _cacheProvider;
        public EfUnitOfWork(IEntityFrameworkDataContext context, ICacheProvider cacheProvider)
        {
            _context = context;
            _cacheProvider = cacheProvider;
            UserRepository = new UserRepository(cacheProvider, context);
            ApplicationRepository = new ApplicationRepository(cacheProvider, context);
            AuthTokenRepository = new AuthTokenRepository(cacheProvider, context);
            AccountRepository = new AccountRepository(cacheProvider, context);
            TaskRepository = new TaskRepository(cacheProvider, context);
            TagTaskRepository = new TagTaskRepository(cacheProvider, context);
        }

        public IUserRepository UserRepository { get; }

        public IApplicationRepository ApplicationRepository { get; }

        public IAuthTokenRepository AuthTokenRepository { get; }

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


        List<EfUnitOfWork> _uowInstances;
        public IUnitOfWork CreateNewInstance()
        {
            if (_uowInstances == null)
            {
                _uowInstances = new List<EfUnitOfWork>();
            }
            var result = new EfUnitOfWork(_context.CreateNewInstance(), _cacheProvider);
            _uowInstances.Add(result);
            return result;
        }

        public void Dispose()
        {
            if (_uowInstances != null)
            {
                foreach (var uow in _uowInstances)
                {
                    uow.Dispose();
                }
            }
            _context.Dispose();
        }
    }
}