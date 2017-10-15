using Akka.Actor;
using Akka.Cluster.Sharding;
using SM.TaskEngine.Common;
using SM.TaskEngine.Common.Sharding;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Commands;
using System;

namespace SM.TaskEngine.Persistence
{
    class Program
    {
        static ActorSystem System { get; set; }

        const int maxNumberOfNodes = 1000000;

        static void Main(string[] args)
        {
            //System = AkkaConfig.CreateActorSystem();

            System = ActorSystem.Create("instamass");

            // register actor type as a sharded entity
            var proxy = ClusterSharding.Get(System).StartProxy(
                typeName: typeof(InstagramAccount).Name,
                role: "persistence",
                messageExtractor: new MessageExtractor());

            System.Scheduler.Schedule(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2), () => {
                const string login = "someLogin";
                var cmd = new UpdateAccount() { Password = "1234" };
                //var cmd = "hello";
                Console.WriteLine(cmd);
                proxy.Tell(new ShardEnvelope(login, cmd));
                //proxy.Tell(new ShardEnvelope(login, "hello"));
            });

            System.WhenTerminated.Wait();
        }
    }
}
