using System;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IApplicationRepository ApplicationRepository { get; }

        IAuthTokenRepository AuthTokenRepository { get; }

        IAccountRepository AccountRepository { get; }
        ITaskRepository TaskRepository { get; }
        ITagTaskRepository TagTaskRepository { get; }

        void Complete();

        Task CompleteAsync();

        /// <summary>
        /// для парелельных запросов в ef core
        /// </summary>
        /// <returns></returns>
        IUnitOfWork CreateNewInstance();
    }
}
