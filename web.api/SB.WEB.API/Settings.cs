using System.Configuration;

namespace SM.WEB.API
{
    class Settings
    {
        public static string Url { get { return ConfigurationManager.AppSettings["url"]; } }
        public static string Port { get { return ConfigurationManager.AppSettings["port"]; } }
        public static string AppName { get { return ConfigurationManager.AppSettings["appName"]; } }
    }
}
