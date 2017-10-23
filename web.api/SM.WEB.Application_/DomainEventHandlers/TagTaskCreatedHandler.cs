using System.Threading.Tasks;
using Akka.Actor;
using SM.Domain.Events;

namespace SM.WEB.Application.DomainEventHandlers
{
    public class TagTaskCreatedHandler : ICanHandle<TagTaskWasCreated>
    {
        IActorRef _taskApi;
        public TagTaskCreatedHandler(IActorRef  taskApi)
        {
            _taskApi = taskApi;
        }

        public void Handle(TagTaskWasCreated @event)
        {
            var t = @event.Task;
            _taskApi.Tell(new SM.TaskEngine.Api.ActorModel.Commands.CreateTagTask(t.Version, t.Id.ToString(), @event.InstagramAccountLogin, t.Tags));
        }

        public Task HandleAsync(TagTaskWasCreated domainEvent)
        {
            Handle(domainEvent);
            return Task.FromResult(0);
        }
    }
}
