using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.DTO
{
    public class JwtDTO
    {
        public string access_token{ get; set; }
        public string username { get; set; }
    }
}
