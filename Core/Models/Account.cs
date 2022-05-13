using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Account
    {
        private decimal _balance=0;
        public int AccountNumber { get; set; }
        public CurrencyCodeEnum CurrencyCode { get; set; }
        
        public decimal Balance
        {
            get=>Math.Round(_balance, 2);
            set => _balance = value;

        }

        public string OwnerName { get; set; }
        public AccountTypeEnum AccountType { get; set; }
    }
}
