using SM.Domain.Model;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IUserRepository
    {
        
        Task<User> GetByIdAsync(object key);

        void RegisterNewUser(User newUser);
    }
}