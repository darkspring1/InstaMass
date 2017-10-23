using Akka.Actor;
using Akka.Routing;
using StructureMap;

namespace SM.WEB.API.Akka
{
    class Actors
    {
        ActorSystem _system;

        IContainer _container;

        public IActorRef TaskApi { get; }

        public Actors(IContainer container)
        {
            _system = ActorSystem.Create("instamass");

            TaskApi = _system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "taskApi");

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
