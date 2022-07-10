using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Common.Extension;
using Microsoft.AspNetCore.Authorization;
using TodoList.Application.Serviсes.Interfaces;
using TodoList.Application.DTO;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _catServise;

        public CategoryController(ICategoryService catServise)
        {
            _catServise = catServise;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Json(_catServise.GetCategory(id));
        }
        //для админов
        [Authorize]
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetCategoryListByUserId(int userId)
        {
            return Json(_catServise.GetCategotyListByUserId(userId));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCategoryListByUserJwt()
        {
            var jwt = Request.Headers.GetJwt();
            return Json(_catServise.GetCategotyListByJwt(jwt));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryByUserJwt(CategoryDTO categoryDTO)
        {
            var jwt = Request.Headers.GetJwt();
            return Json(_catServise.CreateCategoryByJwt(categoryDTO, jwt));
        }
        //для админов
        [Authorize]
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateCategoryByUserId(CategoryDTO categoryDTO, int userId)
        {
            return Json(_catServise.CreateCategoryByUserId(categoryDTO, userId));
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeCategory(CategoryDTO categoryDTO)
        {
            return Json(_catServise.ChangeCategory(categoryDTO));
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Json(_catServise.DeleteCategory(id));
        }
    }
}
