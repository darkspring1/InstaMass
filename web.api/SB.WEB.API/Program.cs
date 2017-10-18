using Akka.Actor;
using Akka.Routing;
using SM.WEB.API.Akka;
using System;
using System.Linq;
using Topshelf;

namespace SM.WEB.API
{

    class Program
    {
        //private static sb.core.log.ILogger _logger;
        static Actors Actors { get; set; }
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


    /// <summary>
    /// Prints recommendations out to the console
    /// </summary>
    public class Printer : ReceiveActor
    {
        public Printer()
        {
            Receive<String>(res =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{Environment.NewLine}{res}");
                Console.ResetColor();
            });
        }
    }
}
