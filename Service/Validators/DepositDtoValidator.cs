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
    public class DepositDtoValidator:AbstractValidator<DepositDto>
    {
        private readonly IAccountRepository _accountRepository;

        public DepositDtoValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            RuleFor(x=>x.AccountNumber).NotNull().WithMessage("Id Alanı Boş Olamaz");
            
            RuleFor(x => x.AccountNumber).Must(x =>
            {
                var account = _accountRepository.Get(x);
                if (account != null)
                    return true;
                else
                    return false;
            }).WithMessage("Böyle Bir Hesap Bulunamadı.");
            RuleFor(x => x.AccountNumber).Must(x =>
            {
                var account = _accountRepository.Get(x);
                if ( account?.AccountType != AccountTypeEnum.Individual )
                    return false;
                return true;
            }).WithMessage("Hesap Türü Bireysel Olmalı");
            RuleFor(x => x.Amount).NotNull().WithMessage("Miktar Boş Olamaz").GreaterThan(0).WithMessage("Girilen Para Değeri 0'dan Büyük Olmak Zorunda");
        }
    }
}
