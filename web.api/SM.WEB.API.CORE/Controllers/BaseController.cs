using SM.Common.Log;
using SM.Common.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SM.WEB.API.Controllers
{
    public class BaseController : Controller
    {
        protected ILogger Logger { get; private set; }

        private readonly Lazy<Guid> _userId;
        protected Guid UserId => _userId.Value;

        public BaseController(ILogger logger)
        {
            Logger = logger;
            _userId = new Lazy<Guid>(() =>
            {
                var claim = ((System.Security.Claims.ClaimsPrincipal)this.User).Claims.First(c => c.Type == SMClaimTypes.UserId);
                return Guid.Parse(claim.Value);
            });
        }

        

        protected ActionResult ActionResult<T>(ServiceResult<T> serviceResult)
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

        protected async Task<ActionResult> ActionResultAsync<T>(Task<ServiceResult<T>> serviceResultTask)
        {
            return ActionResult(await serviceResultTask);
        }

        protected ActionResult ActionResult(ServiceResult serviceResult)
        {
            if (!serviceResult.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }


        protected ActionResult ApiErrorCode(ApiErrorCode code, string description)
        {
            //return Content<object>(HttpStatusCode.BadRequest, new { ErrorCode = (int)code, ErrorDescription = description });
            return BadRequest(new { ErrorCode = (int)code, ErrorDescription = description });
        }
    }
}
