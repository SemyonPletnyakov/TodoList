using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTO;

namespace TodoList.Application.Serviсes.Interfaces
{
    public interface ICategoryService
    {
        public CategoryDTO CreateCategory(CategoryDTO categoryDTO, string jwt);
        public bool ChangeCategory(CategoryDTO categoryDTO);
        public bool DaleteCategory(int id);
        public CategoryDTO GetCategory(int id);
        public List<CategoryDTO> GetCategotyListByJwt(string jwt);
    }
}
