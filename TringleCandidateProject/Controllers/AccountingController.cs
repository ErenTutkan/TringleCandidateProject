using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Filters;
using Service.Manager;

namespace TringleCandidateProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountingController : ControllerCustomBase
    {
        private readonly ITransactionManager _transactionManager;

        public AccountingController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        [HttpPost("/deposit")]
        public IActionResult Deposit(DepositDto depositDto)
        {
            return CreateActionResult(_transactionManager.Deposit(depositDto));
        }
        [HttpPost("/withdraw")]
        public IActionResult Withdraw(WithdrawDto withdrawDto)
        {
            return CreateActionResult(_transactionManager.Withdraw(withdrawDto));
        }
        [HttpGet("{accountNumber:int}")]
        [ServiceFilter(typeof(NotFoundAccountFilter))]
        public IActionResult GetById(int accountNumber)
        {
            return CreateActionResult(_transactionManager.GetByTransactionHistory(accountNumber));
        }
        [HttpPost("/payment")]
        public IActionResult Payment(PaymentDto paymentDto)
        {
            return CreateActionResult(_transactionManager.Payment(paymentDto));
        }

    }
}
