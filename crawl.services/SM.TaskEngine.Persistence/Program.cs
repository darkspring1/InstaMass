using Akka.Actor;
using Akka.Cluster.Sharding;
using SM.TaskEngine.Common;
using SM.TaskEngine.Common.Sharding;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount;


namespace SM.TaskEngine.Persistence
{
    class Program
    {
        static ActorSystem System { get; set; }


        static void Main(string[] args)
        {
            System = AkkaConfig.CreateActorSystem();

            var sharding = ClusterSharding.Get(System);
            // register actor type as a sharded entity
            var shardRegion = sharding.Start(
                typeName: typeof(InstagramAccount).Name,
                entityProps: Props.Create<InstagramAccount>(),
                settings: ClusterShardingSettings.Create(System),
                messageExtractor: new MessageExtractor());

            System.WhenTerminated.Wait();
        }
    }
}
