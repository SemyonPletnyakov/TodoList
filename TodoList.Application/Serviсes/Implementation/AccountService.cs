using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTO;
using TodoList.Application.Serviсes.Interfaces;
using TodoList.Common;
using TodoList.Domain;
using TodoList.EntityFramework.Repository.Implementation;
using TodoList.EntityFramework.Repository.Interfaces;

namespace TodoList.Application.Serviсes.Implementation
{
    public class AccountService : IAccountService
    {
        IConfiguration _configuration;
        IUserSelects userSelects;
        public AccountService(IConfiguration conf)
        {
            _configuration = conf;
            userSelects = new UserSelects();
        }

        public string Login(string login, string password)
        {
            try
            {
                int? userId = userSelects.FindUserIdByLoginAndPassword(login, password);
                if (userId != null)
                {
                    return GetJwtDTOById(userId);
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
        private string GetJwtDTOById(int? id)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid", id.ToString())
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                var autOp = _configuration.GetSection("Auth").Get<AuthOptions>();
                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: autOp.Issuer,
                        audience: autOp.Audience,
                        notBefore: now,
                        claims: claimsIdentity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(autOp.Lifetime)),
                        signingCredentials: new SigningCredentials(autOp.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                /*JwtDTO response = new JwtDTO()
                {
                    access_token = encodedJwt,
                    username = claimsIdentity.Name
                };*/
                return "Bearer " + encodedJwt;
            }
            catch
            {
                return null;
            }
        }

        public AccountDTO GetAccountInfoByJwt(string jwt)
        {
            int userId = Convert.ToInt32(GetUserIdFromJwt(jwt));
            User user = userSelects.GetUserById(userId);
            AccountDTO accountDTO = new AccountDTO()
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Fio = user.Fio
            };
            return accountDTO;
        }
        public AccountDTO ChangeAccountInfoByJwt(AccountDTO accountDTO, string jwt)
        {
            int userId = Convert.ToInt32(GetUserIdFromJwt(jwt));
            User userNew = new User()
            {
                Id = userId,
                Login = accountDTO.Login,
                Password = accountDTO.Password,
                Email = accountDTO.Email,
                Fio = accountDTO.Fio
            };
            if (userSelects.ChangeUser(userNew))
            {
                return accountDTO;
            }
            return null;
        }
        public bool DeleteAccountInfoByJwt(string jwt)
        {
            int userId = Convert.ToInt32(GetUserIdFromJwt(jwt));
            if (userSelects.DeleteUserById(userId))
            {
                return true;
            }
            return false;
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
