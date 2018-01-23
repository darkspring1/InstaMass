using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Common.Log;
using SM.WEB.API.Models;
using SM.WEB.Application.Services;
using System;
using System.Threading.Tasks;

namespace SM.WEB.API.Controllers
{
    [Authorize]
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

            if (svResult.IsSuccessAndNotNullResult)
            {
                return Ok(svResult.Result);
            }

            if (svResult.ErrorCode == AccountService.AccountAlreadyRegistred)
            {
                return ApiErrorCode(API.ApiErrorCode.AccountAlreadyRegistred, "Account was already registred");
            }

            return BadRequest();
        }
    }
    
}
