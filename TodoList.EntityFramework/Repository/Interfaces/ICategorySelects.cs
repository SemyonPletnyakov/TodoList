using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain;

namespace TodoList.EntityFramework.Repository.Interfaces
{
    public interface ICategorySelects
    {
        public bool CreateCategory(Category categoryNew);
        public Category GetCategoryById(int id);
        public List<Category> GetCategoriesByUserId(int userId);
        public bool ChangeCategory(Category categoryNew);
        public bool DeleteCategoryById(int id);
    }
}
