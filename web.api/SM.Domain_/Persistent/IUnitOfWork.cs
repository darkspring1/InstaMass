using System;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        IApplicationRepository ApplicationRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

        IAccountRepository AccountRepository { get; }
        ITaskRepository TaskRepository { get; }
        ITagTaskRepository LikeTaskRepository { get; }

        void Complete();

        Task CompleteAsync();

        /// <summary>
        /// для парелельных запросов в ef core
        /// </summary>
        /// <returns></returns>
        IUnitOfWork CreateNewInstance();
    }
}
