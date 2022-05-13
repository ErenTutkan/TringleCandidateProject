using Core.DTOs;
using Core.Enums;
using Core.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Manager
{

    //Database Kullanmadığımız için UnitOfWork Yapısını Kullanmadım Ama Burada Kullanılmak Zorunda Her Transaction'ın Takibi Yapılması Gerekli
    public class TransactionManager : ITransactionManager
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionManager(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        private void AddTransaction(Transaction transaction)
        {
            _transactionRepository.Add(transaction);
        }
        public ResponseDto<List<Transaction>> GetByTransactionHistory(int accountNumber)
        {
            return ResponseDto<List<Transaction>>.Success(_transactionRepository.GetByTransactionHistory(accountNumber), 200);
        }
        public ResponseDto<NoContent> Deposit(DepositDto deposit)
        {
            _accountRepository.UpdateBalance(deposit.AccountNumber, deposit.Amount);
            var transaction = new Transaction()
            {
                AccountNumber = deposit.AccountNumber,
                Amount = deposit.Amount,
                CreatedAt = DateTime.UtcNow,
                TransactionType = TransactionTypeEnum.Deposit
            };
            _transactionRepository.Add(transaction);
            return ResponseDto<NoContent>.Success(201);
        }
        public ResponseDto<NoContent> Payment(PaymentDto payment)
        {
            _accountRepository.UpdateBalance(payment.SenderAccount, -(payment.Amount));
            var senderTransaction = new Transaction()
            {
                AccountNumber = payment.SenderAccount,
                Amount = payment.Amount,
                CreatedAt = DateTime.UtcNow,
                TransactionType = TransactionTypeEnum.Payment
            };
            _transactionRepository.Add(senderTransaction);
            _accountRepository.UpdateBalance(payment.ReceiverAccount, payment.Amount);
            var receiverTransaction = new Transaction()
            {
                AccountNumber = payment.ReceiverAccount,
                Amount = payment.Amount,
                CreatedAt = DateTime.UtcNow,
                TransactionType = TransactionTypeEnum.Payment
            };
            _transactionRepository.Add(receiverTransaction);
            return ResponseDto<NoContent>.Success(201);
        }

        public ResponseDto<NoContent> Withdraw(WithdrawDto withdraw)
        {
            _accountRepository.UpdateBalance(withdraw.AccountNumber, -withdraw.Amount);
            var transaction = new Transaction()
            {
                AccountNumber = withdraw.AccountNumber,
                Amount = withdraw.Amount,
                CreatedAt = DateTime.UtcNow,
                TransactionType = TransactionTypeEnum.Withdraw
            };
            _transactionRepository.Add(transaction);
            return ResponseDto<NoContent>.Success(201);
        }
    }
}
