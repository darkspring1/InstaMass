using Microsoft.Owin;
using Newtonsoft.Json;
using NLog;
using System.Threading.Tasks;

namespace SM.WEB.API.Middlewares
{
    public class NLogMiddleware : OwinMiddleware
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public NLogMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            _logger.Info("START REQUEST {0}", context.Request.Uri);
            _logger.Info("REQUEST {0} Host: {1}", context.Request.Uri, context.Request.Host);
            _logger.Info("REQUEST {0} LocalIpAddress:  {1}", context.Request.Uri, context.Request.LocalIpAddress);
            _logger.Info("REQUEST {0} LocalPort: {1}", context.Request.Uri, context.Request.LocalPort);
            _logger.Info("REQUEST {0} RemoteIpAddress: {1}", context.Request.Uri, context.Request.RemoteIpAddress);
            _logger.Info("REQUEST {0} RemotePort: {1}", context.Request.Uri, context.Request.RemotePort);
            _logger.Info("REQUEST {0} Method: {1}", context.Request.Uri, context.Request.Method);
            _logger.Info("REQUEST {0} QueryString: {1}", context.Request.Uri, context.Request.QueryString);
            //_logger.Info("REQUEST {0} Length: {1}", context.Request.Uri, context.Request.Body.Length);
            _logger.Info("REQUEST {0} Headers: {1}", context.Request.Uri, JsonConvert.SerializeObject(context.Request.Headers));
            return Next.Invoke(context);
            //var t = Next.Invoke(context);
            //иначе можно словит "Доступ к ликведированному объекту"
            //t.ContinueWith(a => _logger.Info("END REQUEST {0}", context.Request.Uri));
            //return t;
        }
    }
}
