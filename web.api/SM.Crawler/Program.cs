using Microsoft.Extensions.Configuration;
using SM.Common.Settings;
using SM.Crawler.Ioc;
using SM.Domain.Services;
using StructureMap;
using System;

namespace SM.Crawler
{
    class Program
    {
        static SMSettings GetSettings()
        {
            var config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddJsonFile("appsettings.Development.json")
              .AddEnvironmentVariables()
              .Build();
            return new SMSettings(config);
        }


        static void Main(string[] args)
        {
            var settings = GetSettings();
            using (var container = new Container(new MainRegistry(settings.ConnectionString)))
            {
                var m = container.GetInstance<Manager>();
                m.StartAsync().Wait();
            }
            
            Console.WriteLine("Hello World!");
        }

       
    }
}
