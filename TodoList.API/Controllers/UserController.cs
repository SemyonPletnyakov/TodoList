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


        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            return Json(_userServise.AddUser(userDTO));
        }

        //для админа
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            return Json(_userServise.GetUserById(id));
        }

        //для админа
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeUser(UserDTO userDTO)
        {
            return Json(_userServise.ChangeUser(userDTO));
        }

        //для админа
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Json(_userServise.DeleteUserById(id));
        }
    }
}