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
using TodoList.Domain;
using TodoList.EntityFramework.Repository.Interfaces;
using TodoList.EntityFramework.Repository.Implementation;
using TodoList.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TodoList.Application.Serviсes.Implementation
{
    public class UserServiсe : IUserServiсe
    {
        IConfiguration _configuration;
        IUserSelects userSelects;
        public UserServiсe(IConfiguration conf)
        {
            _configuration = conf;
            userSelects = new UserSelects();
        }
        public bool AddUser(UserDTO userDTO)
        {
            try
            {
                User user = new User()
                {
                    Login = userDTO.Login,
                    Password = userDTO.Password,
                    Email = userDTO.Email,
                    Fio = userDTO.Fio
                };
                if (userSelects.CreateUser(user))
                {
                    int? userId = user.Id;
                    if (userId != null)
                    {
                        CategorySelects categorySelects = new CategorySelects();
                        Category category = new Category()
                        {
                            Name = "Без категории",
                            UserId = userId.Value
                        };
                        categorySelects.CreateCategory(category);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public UserDTO GetUserById(int id)
        {
            try
            {
                User user = userSelects.GetUserById(id);
                UserDTO userDTO = new UserDTO()
                {
                    Id = id,
                    Login = user.Login,
                    Password = user.Password,
                    Email = user.Email,
                    Fio = user.Fio
                };
                return userDTO;
            }
            catch
            {
                return null;
            }
        }
        public UserDTO ChangeUser(UserDTO userDTO)
        {
            try
            {
                User userNew = new User()
                {
                    Id = userDTO.Id,
                    Login = userDTO.Login,
                    Password = userDTO.Password,
                    Email = userDTO.Email,
                    Fio = userDTO.Fio
                };
                if (userSelects.ChangeUser(userNew))
                {
                    return userDTO;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteUserById(int id)
        {
            if (userSelects.DeleteUserById(id))
            {
                return true;
            }
            return false;
        }
    }
}
