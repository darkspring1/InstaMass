using SM.Common.Log;
using SM.Common.Services;
using System.Web.Http;

namespace SM.WEB.API.Controllers
{
    public class BaseController : ApiController
    {
        protected ILogger Logger { get; private set; }

        public BaseController(ILogger logger)
        {
            Logger = logger;
        }

        protected IHttpActionResult ActionResult<T>(ServiceResult<T> serviceResult)
        {
            if (!serviceResult.IsSuccess)
            {
                return BadRequest();
            }

            if (serviceResult.Result == null)
            {
                return NotFound();
            }

            return Ok(serviceResult.Result);
        }

        protected IHttpActionResult ActionResult(ServiceResult serviceResult)
        {
            if (!serviceResult.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
