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
    public class NoteController : Controller
    {
        private readonly INoteService _noteServise;

        public NoteController(INoteService noteServise)
        {
            _noteServise = noteServise;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            return Json(_noteServise.GetNote(id));
        }
        [Authorize]
        [HttpGet("Category/{categoryId}")]
        public async Task<IActionResult> GetNoteListByCategoryId(int categoryId)
        {
            return Json(_noteServise.GetNoteListByCategoryId(categoryId));
        }
        //для админов
        [Authorize]
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetNoteListByUserId(int userId)
        {
            return Json(_noteServise.GetNoteListByUserId(userId));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetNoteListByUserJwt()
        {
            var jwt = Request.Headers.GetJwt();
            return Json(_noteServise.GetNoteListByUserJwt(jwt));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteDTO noteDTO)
        {
            var jwt = Request.Headers.GetJwt();
            return Json(_noteServise.CreateNote(noteDTO));
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeNote(NoteDTO noteDTO)
        {
            return Json(_noteServise.ChangeNote(noteDTO));
        }
        [Authorize]
        [HttpPut("status")]
        public async Task<IActionResult> ChangeStatusNote(int id, bool status)
        {
            return Json(_noteServise.ChangeStatusNote(id, status));
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteNote(int id)
        {
            return Json(_noteServise.DeleteNote(id));
        }
    }
}
