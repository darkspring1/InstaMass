using Akka.Actor;
using Akka.Routing;
using SM.TaskEngine.Api.ActorModel;
using SM.TaskEngine.Api.ActorModel.Commands;
using SM.TaskEngine.Common;
using System;

namespace SM.TaskEngine.Api.TestClient
{
    class Program
    {
        static ActorSystem System { get; set; }
        static IActorRef Printer { get; set; }
        static void Main(string[] args)
        {
            System = AkkaConfig.CreateActorSystem();

            var CoordinatorRouter =
                    System.ActorOf(
                        Props.Create(() => new ApiMaster())
                            .WithRouter(FromConfig.Instance), "tasker");

            Printer = System.ActorOf(Props.Create(() => new Printer()));

            //System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), CoordinatorRouter, new UpdateAccount("test", "Test" + DateTime.Now.ToBinary(), 1), Printer);

            System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), CoordinatorRouter, new CreateTagTask(1, Guid.NewGuid().ToString(), "someLogin", new[] { "tits" }), Printer);

            System.WhenTerminated.Wait();
        }
    }
}
