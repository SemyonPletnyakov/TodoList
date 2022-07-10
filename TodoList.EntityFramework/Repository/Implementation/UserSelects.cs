using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain;
using TodoList.EntityFramework.Repository.Interfaces;

namespace TodoList.EntityFramework.Repository.Implementation
{
    public class UserSelects : IUserSelects
    {
        private TodoListContext db;
        public UserSelects()
        {
            db = new TodoListContext();
        }
        
        public bool CreateUser(User userNew)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Login == userNew.Login);
                if (user == null)
                {
                    user = db.Users.FirstOrDefault(u => u.Email == userNew.Email);
                    if (user == null)
                    {
                        db.Users.Add(userNew);
                        db.SaveChanges();
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
        
        public User GetUserById(int id) 
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            return user;
            
        }
        
        public bool ChangeUser(User userNew)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userNew.Id);
                if (user != null)
                {
                    user.Login = userNew.Login;
                    user.Password = userNew.Password;
                    user.Email = userNew.Email;
                    user.Fio = userNew.Fio;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteUserById(int id)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public int? FindUserIdByLoginAndPassword(string login, string password)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    return user.Id;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

    }
}
