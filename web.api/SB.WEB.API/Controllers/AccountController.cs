using SM.Common.Log;
using SM.Domain.Model;
using SM.WEB.API.Models;
using SM.WEB.Application.Services;
using System;
using System.Web.Http;

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
        public Account[] GetAccounts()
        {
            //_accountServiceServiceFunc().FindByUser();
            return null;
        }


        [HttpPost]
        [Route(Routes.ApiAccount)]
        public Account CreateAccount(NewAccountModel model)
        {
            //_accountServiceServiceFunc().FindByUser();
            return null;
        }
    }
    
}
