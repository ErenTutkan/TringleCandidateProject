using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private static List<Transaction> _transactions = new List<Transaction>();
        public void Add(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public List<Transaction> GetByTransactionHistory(int accountId)
        {
            return _transactions.Where(x=>x.AccountNumber==accountId).ToList();
        }
    }
}
