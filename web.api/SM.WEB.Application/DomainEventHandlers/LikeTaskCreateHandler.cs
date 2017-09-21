using SM.Domain.Events;
using System;


namespace SM.WEB.Application.DomainEventHandlers
{
    public class LikeTaskCreateHandler : ICanHandle<LikeTaskWasCreated>
    {

        public LikeTaskCreateHandler()
        {

        }

        public void Handle(LikeTaskWasCreated args)
        {
            throw new NotImplementedException();
        }
    }
}
