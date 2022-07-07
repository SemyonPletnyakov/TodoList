using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain;

namespace TodoList.EntityFramework.Repository.Interfaces
{
    public interface IUserSelects
    {
        public User GetUserById(int id);
    }
}
