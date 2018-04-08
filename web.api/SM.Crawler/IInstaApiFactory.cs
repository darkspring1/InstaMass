using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
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
        public IInstaApi Create(string login, string password)
        {
            return InstaApiBuilder
                   .CreateBuilder()
                 .UseLogger(new Logger())
                 .SetUser(new UserSessionData { UserName = login, Password = password })
                 .Build();
        }
    }
}
