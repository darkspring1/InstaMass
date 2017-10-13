using Akka.Actor;
using Akka.Configuration;
using SM.TaskEngine.Common;

namespace SM.TaskEngine.Api
{
    class Program
    {
        static ActorSystem System { get; set; }

        static void Main(string[] args)
        {
            System = AkkaConfig.CreateActorSystem();
            System.WhenTerminated.Wait();

        }
    }
}
