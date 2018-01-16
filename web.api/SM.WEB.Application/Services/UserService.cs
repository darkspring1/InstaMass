using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using SM.Domain.Services;
using System.Threading.Tasks;
using SM.Domain.Events;

namespace SM.WEB.Application.Services
{
    public class UserService : SMBaseService
    {
        public UserService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(unitOfWork, logger, eventDispatcher)
        {
        }

        public Task<ServiceResult<User>> CreateAync(string email, string userName, string password)
        {
            return RunAsync(async () => {
                var newUser = User.Create(email, userName, password);
                UnitOfWork.UserRepository.RegisterNewUser(newUser);
                await UnitOfWork.CompleteAsync();
                return newUser;
            });  
        }

        public Task<ServiceResult<User>> FindByEmailAsync(string email)
        {
            return RunAsync(() => UnitOfWork.UserRepository.FindAsync(email));
        }

        public Task<ServiceResult<User>> FindAsync(string email, string password)
        {
            return RunAsync(() => UnitOfWork.UserRepository.FindAsync(email, password));
        }
    }
}
