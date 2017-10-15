using Akka.Actor;
using Akka.Cluster.Sharding;
using SM.TaskEngine.Common.Sharding;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount;
using SM.TaskEngine.Persistence.ActorModel.TagTask;

namespace SM.TaskEngine.Persistence
{
    class Program
    {
        static ActorSystem System { get; set; }


        static void Main(string[] args)
        {

            System = ActorSystem.Create("instamass");


            var sharding = ClusterSharding.Get(System);
            
            var instagramAccountShardRegion = sharding.Start(
                typeName: typeof(InstagramAccount).Name,
                entityProps: Props.Create<InstagramAccount>(),
                settings: ClusterShardingSettings.Create(System),
                messageExtractor: new MessageExtractor());


            var tagTaskShardRegion = sharding.Start(
                typeName: typeof(TagTask).Name,
                entityProps: Props.Create<TagTask>(),
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
