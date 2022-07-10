using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.DTO;
using TodoList.Application.Serviсes.Interfaces;
using TodoList.Common.Extension;
using Microsoft.AspNetCore.Authorization;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accServise;

        public AccountController(IAccountService accServise)
        {
            _accServise = accServise;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAccountInfo()
        {
            var jwt = Request.Headers.GetJwt();
            return Json(_accServise.GetAccountInfoByJwt(jwt));
        }
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            return Json(_accServise.Login(login, password));
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeAccount(AccountDTO accountDTO)
        {
            var jwt = Request.Headers.GetJwt();
            return Json(_accServise.ChangeAccountInfoByJwt(accountDTO, jwt));
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            var jwt = Request.Headers.GetJwt();
            return Json(_accServise.DeleteAccountInfoByJwt(jwt));
        }
    }
}
