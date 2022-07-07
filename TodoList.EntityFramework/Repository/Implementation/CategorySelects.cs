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
        public bool CreateCategory(Category categoryNew)
        {
            using (TodoListContext db = new TodoListContext())
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
        }
        public Category GetCategoryById(int id)
        {
            using (TodoListContext db = new TodoListContext())
            {
                var category = db.Categories.FirstOrDefault(c => c.Id == id);
                return category;
            }
        }
        public List<Category> GetCategoriesByUserId(int userId)
        {
            using (TodoListContext db = new TodoListContext())
            {
                var categories = db.Categories.Where(c=>c.UserId==userId).ToList();
                return categories;
            }
        }
        public bool ChangeCategory(Category categoryNew)
        {
            using (TodoListContext db = new TodoListContext())
            {
                try
                {
                    Category category = db.Categories.FirstOrDefault(c => c.Id == categoryNew.Id);
                    if (category != null)
                    {
                        category = categoryNew;
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
        public bool DeleteCategoryById(int id)
        {
            using (TodoListContext db = new TodoListContext())
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
}
