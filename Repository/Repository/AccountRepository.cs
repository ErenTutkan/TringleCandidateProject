using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private static List<Account> _accounts = new List<Account>();

        public bool Add(AddAccountDto account)
        {

            _accounts.Add(new Account()
            {
                AccountNumber = account.AccountNumber,
                Balance = 0,
                AccountType = account.AccountType,
                CurrencyCode = account.CurrencyCode,
                OwnerName = account.OwnerName
            });
            return true;
        }

        public Account Get(int accountNumber)
        {
            return _accounts.FirstOrDefault(x=>x.AccountNumber==accountNumber);
        }

        public void UpdateBalance(int accountNumber, decimal balance)
        {
            var accountIndex=_accounts.FindIndex(x => x.AccountNumber == accountNumber);
            _accounts[accountIndex].Balance+=balance;
        }
    }
}
