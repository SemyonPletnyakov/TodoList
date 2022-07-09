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

namespace TodoList.Application.Serviсes.Implementation
{
    public class CategoryService : ICategoryService
    {
        IConfiguration _configuration;
        public CategoryService(IConfiguration conf)
        {
            _configuration = conf;
        }
        public CategoryDTO CreateCategory(CategoryDTO categoryDTO, string jwt)
        {
            int userId = Convert.ToInt32(GetUserIdFromJwt(jwt));
            Category category = new Category()
            {
                Name = categoryDTO.Name,
                UserId = userId
            };
            CategorySelects categorySelects = new CategorySelects();
            if (categorySelects.CreateCategory(category))
            {
                categoryDTO.Id = category.Id;
                return categoryDTO;
            }
            return null;
        }

        public bool ChangeCategory(CategoryDTO categoryDTO)
        {
            Category category = new Category()
            {
                Name = categoryDTO.Name,
                Id = categoryDTO.Id
            };
            CategorySelects categorySelects = new CategorySelects();
            return categorySelects.ChangeCategory(category);
        }
        public bool DeleteCategory(int id)
        {
            CategorySelects categorySelects = new CategorySelects();
            return categorySelects.DeleteCategoryById(id);
        }
        public CategoryDTO GetCategory(int id)
        {
            CategorySelects categorySelects = new CategorySelects();
            Category category = categorySelects.GetCategoryById(id);
            if (category != null)
            {
                CategoryDTO categoryDTO = new CategoryDTO()
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return categoryDTO;
            }
            return null;
        }
        public List<CategoryDTO> GetCategotyListByJwt(string jwt)
        {
            int userId = Convert.ToInt32(GetUserIdFromJwt(jwt));
            CategorySelects categorySelects = new CategorySelects();
            List<Category> categories = categorySelects.GetCategoriesByUserId(userId);
            if (categories != null)
            {
                List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
                foreach (Category c in categories)
                {
                    CategoryDTO cDTO = new CategoryDTO()
                    {
                        Id = c.Id,
                        Name = c.Name
                    };
                    categoriesDTO.Add(cDTO);
                }
                return categoriesDTO;
            }
            return null;
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
