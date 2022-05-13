using Core.DTOs;
using Core.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Manager
{
    public class AccountManager:IAccountManager
    {
        private readonly IAccountRepository _accountRepository;

        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ResponseDto<NoContent> Create(AddAccountDto account)
        {
             _accountRepository.Add(account);
            return ResponseDto<NoContent>.Success(201);
        }
        public ResponseDto<Account> GetAccount(int accountNumber)
        {
            return ResponseDto<Account>.Success(_accountRepository.Get(accountNumber), 200);
        }
        
    }
}
