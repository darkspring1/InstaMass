using System;
using System.Threading;
using Akka.Actor;
using InstaMass.ActorModel.Commands;
using Api.ActorModel.Actors;
using Akka.Cluster.Sharding;
using InstaMass.Api.Sharding;
using InstaMass.ActorModel.Actors;

namespace InstaMass.Api
{
    class Program
    {
        static ActorSystem System { get; set; }
        private static IActorRef PlayerCoordinator { get; set; }

        const int maxNumberOfNodes = 1000000;

        static void Main(string[] args)
        {
            System = ActorSystem.Create("instamass");

            var sharding = ClusterSharding.Get(System);
            // register actor type as a sharded entity
            var shardRegion = sharding.Start(
                typeName: nameof(InstaUserActionActor),
                entityProps: Props.Create<InstaUserActionActor>(),
                settings: ClusterShardingSettings.Create(System),
                messageExtractor: new MessageExtractor(maxNumberOfNodes * 10));


            var userStoreActor = System.ActorOf(Props.Create<UserStoreActor>(), "userStore");

            var coordinatorActor = System.ActorOf(Props.Create(() => new CoordinatorActor(userStoreActor, shardRegion)), "coordinator");

            coordinatorActor.Tell(StartExecuting.Instance);

            System.WhenTerminated.Wait();
        }

       
    }
}
