using System;
using System.Threading;
using static System.Console;
using Akka.Actor;
using InstaMass.ActorModel.Commands;
using Api.ActorModel.Actors;

namespace InstaMass
{
    class Program
    {
        private static ActorSystem System { get; set; }
        private static IActorRef PlayerCoordinator { get; set; }

        static void Main(string[] args)
        {
            System = ActorSystem.Create("InstaMass");

            var userStoreActor = System.ActorOf(Props.Create<UserStoreActor>(), "userStore");

            var coordinatorActor = System.ActorOf(Props.Create(() => new CoordinatorActor(userStoreActor)), "coordinator");


            coordinatorActor.Tell(StartExecuting.Instance);
            
            //PlayerCoordinator = System.ActorOf<PlayerCoordinatorActor>("PlayerCoordinator");

            Console.ReadKey();

            
            
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
