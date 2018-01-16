using SM.Domain.Model;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IUserRepository
    {
        
        Task<User> GetByIdAsync(object key);

        void RegisterNewUser(User newUser);


        Task<User> FindAsync(string email, string password);


        /// <summary>
        /// найти пользователя по Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> FindAsync(string email);
    }
}