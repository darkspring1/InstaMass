using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.WEB.API.Akka
{
    class Actors
    {
        ActorSystem _system;
        public Actors()
        {
            _system = ActorSystem.Create("instamass");


            // register actor type as a sharded entity

            var api = _system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "api");
            var printer = _system.ActorOf<Printer>();

            _system.Scheduler.Advanced.ScheduleRepeatedly(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), () =>
            {
                //api.Tell("ping", printer);

                if (api.Ask<Routees>(new GetRoutees()).Result.Members.Any())
                {
                    api.Tell("ping", printer);
                }

            });

            _system.WhenTerminated.Wait();
        }
    }
}
