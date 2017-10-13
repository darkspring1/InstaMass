using Akka.Actor;
using SM.TaskEngine.Common;

namespace SM.TaskEngine.Api
{
    class Program
    {
        static ActorSystem System;

        static void Main(string[] args)
        {
            System = AkkaConfig.CreateActorSystem();
            System.WhenTerminated.Wait();
        }
    }
}
