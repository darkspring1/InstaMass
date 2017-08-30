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

        void Complete();

        Task CompleteAsync();
    }
}
