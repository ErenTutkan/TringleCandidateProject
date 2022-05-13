using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository.Repository
{
    public  interface ITransactionRepository
    {
        List<Transaction> GetByTransactionHistory(int accountNumber);
        void Add(Transaction transaction);
    }
}
