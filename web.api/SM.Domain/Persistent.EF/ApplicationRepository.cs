using System.Linq;
using System.Threading.Tasks;
using SM.Common.Cache;

namespace SM.Domain.Persistent.EF
{
    class ApplicationRepository : BaseRepository<Model.Application, State.ApplicationState>, IApplicationRepository
    {
        public ApplicationRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        protected override Model.Application Create(State.ApplicationState state)
        {
            return state == null ? null : new Model.Application(state);
        }

        public Task<Model.Application> GetByIdAsync(string id)
        {
            var stateTask = FirstOrDefaultAsync(Set.Entities.Where(a => a.Id == id));
            return CreateAsync(stateTask);
        }
    }
}
