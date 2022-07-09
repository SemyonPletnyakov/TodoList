﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTO;

namespace TodoList.Application.Sercises.Interfaces
{
    public interface IUserServiсe
    {
        public string LoginUser(string login, string password);
        public string RegisterUser(UserDTO userDTO);
        public UserDTO GetUserInfoByJwt(string jwt);
        public bool ChangeUserByJwt(UserDTO userDTO, string jwt);
        public bool DeleteUserByJwt(string jwt);
    }
}
