using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTO;
using TodoList.Application.Serviсes.Interfaces;
using TodoList.Common;
using TodoList.Domain;
using TodoList.EntityFramework.Repository.Implementation;
using TodoList.EntityFramework.Repository.Interfaces;

namespace TodoList.Application.Serviсes.Implementation
{
    public class NoteService : INoteService
    {
        IConfiguration _configuration;
        INoteSelects noteSelects;
        public NoteService(IConfiguration conf)
        {
            _configuration = conf;
            noteSelects = new NoteSelects();
        }
        public NoteDTO CreateNote(NoteDTO noteDTO)
        {
            Note note = new Note()
            {
                Heading = noteDTO.Heading,
                Text = noteDTO.Text,
                CreateDate = DateTime.Now,
                Deadline = noteDTO.Deadline,
                Status = false,
                CategoryId = noteDTO.CategoryId
            };
            if (noteSelects.CreateNote(note))
            {
                noteDTO.Id = note.Id;
                noteDTO.CreateDate = note.CreateDate;
                noteDTO.Status = note.Status;
                return noteDTO;
            }
            return null;
        }
        public NoteDTO GetNote(int id)
        {
            Note note = noteSelects.GetNoteById(id);
            if(note != null)
            {
                NoteDTO noteDTO = new NoteDTO()
                {
                    Id = note.Id,
                    Heading = note.Heading,
                    Text = note.Text,
                    CreateDate = note.CreateDate,
                    Deadline = note.Deadline,
                    Status = note.Status,
                    CategoryId = note.CategoryId
                };
                return noteDTO;
            }
            return null;
        }
        public List<NoteDTO> GetNoteListByCategoryId(int categoryId)
        {
            List<Note> noteList = noteSelects.GetNoteByCategoryId(categoryId);
            if (noteList != null)
            {
                List<NoteDTO> noteListDTO = new List<NoteDTO>();
                foreach(Note n in noteList)
                {
                    NoteDTO nDTO = new NoteDTO()
                    {
                        Id = n.Id,
                        Heading = n.Heading,
                        Text = n.Text,
                        CreateDate = n.CreateDate,
                        Deadline = n.Deadline,
                        Status = n.Status,
                        CategoryId = n.CategoryId
                    };
                    noteListDTO.Add(nDTO);

                }
                return noteListDTO;
            }
            return null;
        }
        public List<NoteDTO> GetNoteListByUserJwt(string jwt)
        {
            int userId = Convert.ToInt32(GetUserIdFromJwt(jwt));
            List<Note> noteList = noteSelects.GetNoteByUserId(userId);
            if (noteList != null)
            {
                List<NoteDTO> noteListDTO = new List<NoteDTO>();
                foreach (Note n in noteList)
                {
                    NoteDTO nDTO = new NoteDTO()
                    {
                        Id = n.Id,
                        Heading = n.Heading,
                        Text = n.Text,
                        CreateDate = n.CreateDate,
                        Deadline = n.Deadline,
                        Status = n.Status,
                        CategoryId = n.CategoryId
                    };
                    noteListDTO.Add(nDTO);

                }
                return noteListDTO;
            }
            return null;
        }
        public List<NoteDTO> GetNoteListByUserId(int userId)
        {
            List<Note> noteList = noteSelects.GetNoteByUserId(userId);
            if (noteList != null)
            {
                List<NoteDTO> noteListDTO = new List<NoteDTO>();
                foreach (Note n in noteList)
                {
                    NoteDTO nDTO = new NoteDTO()
                    {
                        Id = n.Id,
                        Heading = n.Heading,
                        Text = n.Text,
                        CreateDate = n.CreateDate,
                        Deadline = n.Deadline,
                        Status = n.Status,
                        CategoryId = n.CategoryId
                    };
                    noteListDTO.Add(nDTO);

                }
                return noteListDTO;
            }
            return null;
        }
        public NoteDTO ChangeNote(NoteDTO noteDTO)
        {
            Note note = new Note()
            {
                Heading = noteDTO.Heading,
                Text = noteDTO.Text,
                CreateDate = DateTime.Now,
                Deadline = noteDTO.Deadline,
                Status = false,
                CategoryId = noteDTO.CategoryId
            };
            if(noteSelects.ChangeNote(note))
                return noteDTO;
            return null;
        }
        public bool ChangeStatusNote(int id, bool status)
        {
            return noteSelects.ChangeStatusNote(id, status);
        }

        public bool DeleteNote(int id)
        {
            return noteSelects.DeleteNoteById(id);
        }

        private string GetUserIdFromJwt(string jwtString)
        {
            try
            {
                bool isValid = ValidateToken(jwtString);
                if (isValid)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(jwtString);
                    var tokenS = (JwtSecurityToken)jsonToken;

                    var id = tokenS.Claims.
                        Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").
                        Select(claim => claim.Value).FirstOrDefault();
                    return id;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return true;
            }
            catch
            {
                return false;
            }

        }
        private TokenValidationParameters GetValidationParameters()
        {
            var autOp = _configuration.GetSection("Auth").Get<AuthOptions>();

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = autOp.Issuer,
                ValidateAudience = true,
                ValidAudience = autOp.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = autOp.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };
        }
    }
}
