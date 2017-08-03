using System;
using Topshelf;

namespace SM.WEB.API
{

    class Program
    {
        //private static sb.core.log.ILogger _logger;
        
        static void Main(string[] args)
        {
            try
            {
                //_logger = new NLogLogger();
                var serviceName = Settings.AppName;
                //берём из конфига
                string port = Settings.Port;
                HostFactory.Run(x =>
                {
                    //переопределяем порт параметром из командно строки, если он есть
                    x.AddCommandLineDefinition("port", f => { port = f; });
                    x.ApplyCommandLine();
                    x.UseNLog();
                    //_logger.Info("Lisen {0}:{1}", Settings.Url, port);
                    x.Service<Service>(s =>
                    {
                        s.ConstructUsing(name => new Service());
                        s.WhenStarted(tc => tc.Start(Settings.Url, int.Parse(port)));
                        s.WhenStopped(tc => tc.Stop());

                    });

                    x.RunAsLocalSystem();

                    x.SetDescription(serviceName);
                    x.SetDisplayName(serviceName);
                    x.SetServiceName(serviceName);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //_logger.Error(e);
            }
        }
    }
}
