using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTO;

namespace TodoList.Application.Serviсes.Interfaces
{
    public interface IAccountService
    {
        public string Login(string login, string password);
        public AccountDTO GetAccountInfoByJwt(string jwt);
        public AccountDTO ChangeAccountInfoByJwt(AccountDTO accountDTO, string jwt);
        public bool DeleteAccountInfoByJwt(string jwt);
    }
}
