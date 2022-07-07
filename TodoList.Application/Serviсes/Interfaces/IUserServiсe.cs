using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTO;

namespace TodoList.Application.Sercises.Interfaces
{
    public interface IUserServiсe
    {
        public UserDTO GetUserInfo(int id);
    }
}
