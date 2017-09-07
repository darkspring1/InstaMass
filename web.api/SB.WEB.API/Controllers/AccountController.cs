﻿using SM.Common.Log;
using SM.Domain.Model;
using SM.WEB.API.Models;
using SM.WEB.Application.Services;
using System;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> CreateAccount(NewAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //todo: проверять, что логин и пароль верные
            //todo: проверять что пользователя с таким instaloginom нет в нашей базе
            //var existResult = await _accountServiceServiceFunc().IsExist(model.Login);

            //if (existResult.Result)
            //{
            //    return BadRequest("AccountAlreadyExists");
            //}


            var svResult = await _accountServiceServiceFunc().CreateAync(UserId, model.Login, model.Password);

            return ActionResult(svResult);
        }
    }
    
}
