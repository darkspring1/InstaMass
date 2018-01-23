using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Common.Log;

namespace SM.WEB.API.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : BaseController
    {
        public OrdersController(ILogger logger) : base(logger)
        {
        }

        [Authorize]
        public string Get()
        {
            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            //var Name = ClaimsPrincipal.Current.Identity.Name;
            //var Name1 = User.Identity.Name;

            //var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;

            return "hello";
        }

    }
}
