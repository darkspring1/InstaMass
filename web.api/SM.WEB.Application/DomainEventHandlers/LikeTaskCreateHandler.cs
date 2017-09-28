using SM.Domain.Events;
using System;
using System.Threading.Tasks;

namespace SM.WEB.Application.DomainEventHandlers
{
    public class LikeTaskCreateHandler : ICanHandle<LikeTaskWasCreated>
    {

        public LikeTaskCreateHandler()
        {

        }

        public void Handle(LikeTaskWasCreated args)
        {
            
        }

        public Task HandleAsync(LikeTaskWasCreated args)
        {
            return Task.FromResult(0);
        }
    }
}
