using Microsoft.AspNetCore.Mvc;
using SM.Common.Log;
using SM.Domain.Services;
using SM.WEB.API.CORE.Auth;
using SM.WEB.API.Models;
using System;
using System.Threading.Tasks;

namespace SM.WEB.API.Controllers
{
    [SmAuthorize]
    public class AccountController : BaseController
    {
        private readonly Func<AccountService> _accountServiceServiceFunc;

        public AccountController(ILogger logger, Func<AccountService> accountServiceServiceFunc) : base(logger)
        {
            _accountServiceServiceFunc = accountServiceServiceFunc;
        }

        [HttpGet]
        [Route(Routes.ApiAccounts)]
        public Task<ActionResult> GetAccounts()
        {
            return ActionResultAsync(_accountServiceServiceFunc().FindByUser(UserId));
        }

        [HttpPost]
        [Route(Routes.ApiAccount)]
        public async Task<ActionResult> CreateAccount(NewAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var svResult = await _accountServiceServiceFunc().CreateAync(UserId, model.Login, model.Password);

            return ActionResult(svResult);
        }
    }
    
}
