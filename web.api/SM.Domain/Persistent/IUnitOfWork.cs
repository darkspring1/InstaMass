using SM.Domain.Model;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IAuthTokenRepository AuthTokenRepository { get; }

        IAccountRepository AccountRepository { get; }
        IRepository<SMTask> TaskRepository { get; }
        IRepository<TagTask> TagTaskRepository { get; }

        void Complete();

        Task CompleteAsync();

        /// <summary>
        /// для парелельных запросов в ef core
        /// </summary>
        /// <returns></returns>
        IUnitOfWork CreateNewInstance();
    }
}
