using Core.DTOs;
using Core.Enums;
using FluentValidation;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class PaymentDtoValidator:AbstractValidator<PaymentDto>
    {
        private readonly IAccountRepository _accountRepository;

        public PaymentDtoValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            RuleFor(x=>x.SenderAccount).NotEmpty().WithMessage("Gönderici Alanı Olamaz");
            RuleFor(x => x.ReceiverAccount).NotEmpty().WithMessage("Alıcı Alanı Olamaz");
            RuleFor(x => x.SenderAccount).Must(x =>
            {
                var account = _accountRepository.Get(x);
                if(account.AccountType != AccountTypeEnum.Individual && account==null)
                    return false;
                return true;
            }).WithMessage("Böyle Bir Gönderici Hesabı Yok veya Hesap Türü Bireysel Olmak Zorunda.");
            RuleFor(x => x.ReceiverAccount).Must(x =>
            {
                var account = _accountRepository.Get(x);
                if (account?.AccountType != AccountTypeEnum.Corporate || account==null)
                    return false;
                return true;
            }).WithMessage("Böyle Bir Alıcı Hesabı Yok veya Hesap Türü Kurumsal Olmak Zorunda.");
            RuleFor(x => x.Amount).Must((payment, amount) =>
              {
                  var senderaccount=_accountRepository.Get(payment.SenderAccount);
                  if (senderaccount.Balance < amount)
                      return false;
                  return true;
              }).WithMessage("Göndericinin Bakiyesi Çok Düşük");
            RuleFor(x => x.Amount).NotNull().WithMessage("Miktar Alanı Boş Olamaz");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Miktar 0'dan Büyük Olmak Zorunda");
        }
    }
}
