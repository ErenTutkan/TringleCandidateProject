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
    public class WithdrawDtoValidator:AbstractValidator<WithdrawDto>
    {
        private readonly IAccountRepository _accountRepository;

        public WithdrawDtoValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            RuleFor(x=>x.AccountNumber).NotEmpty().WithMessage("Hesap Numara Alanı Boş Olamaz");
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Miktar Alanı Boş Olamaz");
            RuleFor(x => x.AccountNumber).Must(x =>
            {
                var account = _accountRepository.Get(x);
                if(account == null)
                    return false;
                return true;
            }).WithMessage("Böyle Bir Hesap Bulunamadı.");

            RuleFor(x => x.AccountNumber).Must(x =>
            {
                var account = _accountRepository.Get(x);
                if (account?.AccountType != AccountTypeEnum.Individual)
                    return false;
                return true;
            }).WithMessage("Hesap Türü Bireysel Olmalı");
            RuleFor(x => x.Amount).Must((withdraw, amount) =>
              {
                  var account=_accountRepository.Get(withdraw.AccountNumber);
                  if (account == null || account?.Balance < amount)
                      return false;
                  return true;
              }).WithMessage("Hesap Bakiyesi Çok Düşük");
        }
    }
}
