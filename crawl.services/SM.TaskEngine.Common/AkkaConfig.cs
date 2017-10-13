using Akka.Actor;
using Akka.Configuration;

namespace SM.TaskEngine.Common
{
    public static class AkkaConfig
    {   
        public static ActorSystem CreateActorSystem(string hoconFile = "akka.hocon")
        {
            Config config = System.IO.File.ReadAllText(hoconFile);
            var app = config.GetConfig("app");
            var actorsystem = app.GetString("actorsystem");
            return ActorSystem.Create(actorsystem, config);
        }
    }
}
