using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoList.Common.Extension
{
    public static class HeaderExtension
    {
        public static string GetJwt(this IHeaderDictionary header)
        {
            var authorizationHeader = header.ContainsKey("authorization")
               ? header["authorization"]
               : header["Authorization"];

            return authorizationHeader.ToString().GetJwtFromAuthorizationString();
        }

        public static string GetJwtFromAuthorizationString(this string authorizationHeader)
        {
            if (!String.IsNullOrWhiteSpace(authorizationHeader))
            {
                return authorizationHeader.ToString().Replace("Bearer ", "");
            }
            else
            {
                return null;
            }
        }
    }
}
