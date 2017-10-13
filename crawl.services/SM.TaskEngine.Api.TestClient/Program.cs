using Akka.Actor;
using Akka.Routing;
using SM.TaskEngine.Api.ActorModel;
using SM.TaskEngine.Common;
using System;

namespace SM.TaskEngine.Api.TestClient
{
    class Program
    {
        static ActorSystem System { get; set; }
        static void Main(string[] args)
        {
            System = AkkaConfig.CreateActorSystem();

            var CoordinatorRouter =
                    System.ActorOf(
                        Props.Create(() => new ApiMaster())
                            .WithRouter(FromConfig.Instance), "tasker");

            System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), CoordinatorRouter, "hello", null);

            System.WhenTerminated.Wait();
        }
    }
}
