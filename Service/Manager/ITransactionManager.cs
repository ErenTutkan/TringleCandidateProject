using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Manager
{
    public interface ITransactionManager
    {
        ResponseDto<NoContent> Payment(PaymentDto payment);
        ResponseDto<NoContent> Deposit(DepositDto deposit);
        ResponseDto<NoContent> Withdraw(WithdrawDto withdraw);
      
        ResponseDto<List<Transaction>> GetByTransactionHistory(int accountNumber);
    }
}
