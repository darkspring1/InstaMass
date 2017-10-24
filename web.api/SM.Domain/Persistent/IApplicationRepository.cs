using SM.Domain.Model;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IApplicationRepository
    {
        Task<Application> GetByIdAsync(string key);
    }
}