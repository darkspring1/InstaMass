using System;
using System.Threading;
using static System.Console;
using Akka.Actor;
using InstaMass.ActorModel.Commands;
using Api.ActorModel.Actors;
using Akka.Cluster.Tools.Singleton;

namespace InstaMass
{
    class Program
    {
        static ActorSystem System { get; set; }
        private static IActorRef PlayerCoordinator { get; set; }

        static void Main(string[] args)
        {
            System = ActorSystem.Create("instamass");

            var userStoreActor = System.ActorOf(Props.Create<UserStoreActor>(), "userStore");

            var coordinatorActor = System.ActorOf(Props.Create(() => new CoordinatorActor(userStoreActor)), "coordinator");

            coordinatorActor.Tell(StartExecuting.Instance);

            System.WhenTerminated.Wait();
        }

        private static void ErrorPlayer(string playerName)
        {
            System.ActorSelection($"/user/PlayerCoordinator/{playerName}")
                  .Tell(new SimulateError());
        }

        private static void DisplayPlayer(string playerName)
        {
            System.ActorSelection($"/user/PlayerCoordinator/{playerName}")
                  .Tell(new DisplayStatus());
        }

        private static void HitPlayer(string playerName, int damage)
        {
            System.ActorSelection($"/user/PlayerCoordinator/{playerName}")
                  .Tell(new HitPlayer(damage));
        }

        private static void CreatePlayer(string playerName)
        {
            PlayerCoordinator.Tell(new CreatePlayer(playerName));
        }
    }
}
