using Akka.Actor;
using SM.Domain.Events;
using SM.TaskEngine.Api.ActorModel.Commands;
using System.Threading.Tasks;

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
            _taskApi.Tell(new CreateTagTask(t.Version, t.Id.ToString(), t.Login, t.Tags));
        }

        public Task HandleAsync(TagTaskWasCreated args)
        {
            return Task.FromResult(0);
        }
    }
}
