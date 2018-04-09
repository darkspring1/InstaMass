using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using SM.Common.Cache;
using System;
using System.Net.Http;

namespace SM.Crawler
{
    interface IInstaApiFactory
    {
        IInstaApi Create(string login, string password);
    }

    class Logger : IInstaLogger
    {
        public void LogException(Exception exception)
        {

        }

        public void LogInfo(string info)
        {
            Console.WriteLine(info);
        }

        public void LogRequest(HttpRequestMessage request)
        {

        }

        public void LogRequest(Uri uri)
        {

        }

        public void LogResponse(HttpResponseMessage response)
        {

        }

        public void Write(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
    }

    class InstaApiFactory : IInstaApiFactory
    {
        private readonly ICacheProvider _cache;

        public InstaApiFactory(ICacheProvider cache)
        {
            _cache = cache;
        }

        public IInstaApi Create(string login, string password)
        {
            var key = $"insta_api_{login}";
            return _cache.AddOrGetExisting(key, () =>
            {
                return InstaApiBuilder
                   .CreateBuilder()
                 .UseLogger(new Logger())
                 .SetUser(new UserSessionData { UserName = login, Password = password })
                 .Build();
            },
            TimeSpan.FromMinutes(5));

            
        }
    }
}
