using Akka.Actor;
using Akka.Routing;
using SM.TaskEngine.Api.ActorModel;
using StructureMap;

namespace SM.WEB.API.Akka
{
    class SystemActors
    {
        ActorSystem _system;

        IContainer _container;

        public IActorRef TaskApi { get; }

        public SystemActors(ActorSystem system, IContainer container)
        {
            _system = system;

            TaskApi = _system.ActorOf(Props.Create(() => new ApiMaster()).WithRouter(FromConfig.Instance), "taskApi");

            /*
            var printer = _system.ActorOf<Printer>();

            _system.Scheduler.Advanced.ScheduleRepeatedly(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), () =>
            {
                //api.Tell("ping", printer);

                if (api.Ask<Routees>(new GetRoutees()).Result.Members.Any())
                {
                    api.Tell("ping", printer);
                }

            });
            */
            //_system.WhenTerminated.Wait();
        }
    }
}
