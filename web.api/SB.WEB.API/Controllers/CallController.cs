using System.Web.Http;

namespace SM.WEB.API.Controllers
{

    public class CallController : ApiController
    {
        public CallController() 
        {
        }

        [HttpGet]
        //[Route("api/call/statuses")]
        public void Statuses()
        {
            
        }
    }
    
}
