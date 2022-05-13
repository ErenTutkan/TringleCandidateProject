using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Manager
{
    public interface IAccountManager
    {
        ResponseDto<NoContent> Create(AddAccountDto newAccount);
        ResponseDto<Account> GetAccount(int accountNumber);
    }
}
