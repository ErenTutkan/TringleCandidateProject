using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Transaction
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum  TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
