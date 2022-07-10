using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.DTO;
using TodoList.Application.Serviсes.Interfaces;
using TodoList.Common.Extension;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserServiсe _userServise;

        public UserController(IUserServiсe userServise)
        {
            _userServise = userServise;
        }

        [Authorize]
        [HttpGet]
        public async Task<UserDTO> GetUser()
        {
            var jwt = Request.Headers.GetJwt();
            return _userServise.GetUserInfoByJwt(jwt);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            return Json(_userServise.RegisterUser(userDTO));
        }
        /*[HttpGet("{id}")]
        public async Task<UserDTO> GetUser(int id)
        {
            var jwt = Request.Headers.GetJwt();
            return _userServise.GetUserInfoByJwt(jwt);
        }*/
    }
}
