using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class AddAccountDto
    {
        public int AccountNumber { get; set; }
        public CurrencyCodeEnum CurrencyCode { get; set; }

        public string OwnerName { get; set; }
        public AccountTypeEnum AccountType { get; set; }
    }
}
