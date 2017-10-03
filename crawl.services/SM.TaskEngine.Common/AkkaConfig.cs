using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System.Configuration;

namespace SM.TaskEngine.Common
{
    public static class AkkaConfig
    {
        static Config Config;
        /*
        static AkkaConfig()
        {
            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            var clusterConfig = section.AkkaConfig;
            Config = clusterConfig.GetConfig("sm");
        }
        */
        
        
        static AkkaConfig()
        {
            var section = (ConfigurationSection)ConfigurationManager.GetSection("akka");
            //((Akka.Configuration.Hocon.AkkaConfigurationSection)section).AkkaConfig
            //var clusterConfig = section.AkkaConfig;
            //Config = clusterConfig.GetConfig("sm");
        }

        //public static string ActorSystemName => Config.GetString("actorsystem");
        public static string ActorSystemName => "instamass";

        public static ActorSystem CreateActorSystem() => ActorSystem.Create(ActorSystemName);
    }
}
