using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.Cluster.Tools.Singleton;
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


        static void Main(string[] args)
        {
            //System = AkkaConfig.CreateActorSystem();

            System = ActorSystem.Create("instamass");


            var sharding = ClusterSharding.Get(System);

            //ClusterShardingSettings settings = ClusterShardingSettings.Create(System).WithCoordinatorSingletonSettings(ClusterSingletonManagerSettings.Create(System).WithRole("master"));

            
            // register actor type as a sharded entity
            
            var shardRegion = sharding.Start(
                typeName: typeof(InstagramAccount).Name,
                entityProps: Props.Create<InstagramAccount>(),
                settings: ClusterShardingSettings.Create(System),
                messageExtractor: new MessageExtractor());
               

            /*
            System.Scheduler.Schedule(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2), () => {
                const string login = "someLogin";
                var cmd = new UpdateAccount() { Login = login, Password = "1234" };
                //var cmd = "hello";
                Console.WriteLine(cmd);
                //proxy.Tell(new ShardEnvelope(login, cmd));
                shardRegion.Tell(new ShardEnvelope(login, "hello"));
            });
            */
            


            //shardRegion.Tell(new ShardEnvelope("someLogin", "hello"));

            /*
            var ref_ = System.ActorOf(Props.Create(() => new InstagramAccount()), "test");

            ref_.Tell(new UpdateAccount() { Login = "someLogin", Password = "dddd" });
            */
            System.WhenTerminated.Wait();
        }
    }
}
