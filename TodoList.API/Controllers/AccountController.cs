using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.DTO;
using TodoList.Application.Serviсes.Interfaces;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserServiсe _userServise;

        public AccountController(IUserServiсe userServise)
        {
            _userServise = userServise;
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(string login, string password)
        {
            return Json(_userServise.LoginUser(login, password));
        }
    }
}
