using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IAccountRepository
    {
        bool Add(AddAccountDto account);
        Account Get(int accountNumber);
        void UpdateBalance(int accountNumber,decimal balance);
    }
}
