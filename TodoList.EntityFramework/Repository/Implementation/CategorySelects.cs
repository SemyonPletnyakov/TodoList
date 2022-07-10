using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain;
using TodoList.EntityFramework.Repository.Interfaces;

namespace TodoList.EntityFramework.Repository.Implementation
{
    public class CategorySelects : ICategorySelects
    {
        private TodoListContext db;
        public CategorySelects()
        {
            db = new TodoListContext();
        }
        public bool CreateCategory(Category categoryNew)
        {
            try
            {
                if (categoryNew.UserId != 0)
                {
                    db.Categories.Add(categoryNew);
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
        public Category GetCategoryById(int id)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == id);
            return category;
        }
        public List<Category> GetCategoriesByUserId(int userId)
        {
            var categories = db.Categories.Where(c=>c.UserId==userId).ToList();
            return categories;
        }
        public bool ChangeCategory(Category categoryNew)
        {
            try
            {
                Category category = db.Categories.FirstOrDefault(c => c.Id == categoryNew.Id);
                if (category != null)
                {
                    category.Name = categoryNew.Name;
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
        public bool DeleteCategoryById(int id)
        {
            try
            {
                Category category = db.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    db.Categories.Remove(category);
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
    }
}
