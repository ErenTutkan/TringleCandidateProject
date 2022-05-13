using Core.DTOs;
using FluentValidation;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class AddAccountDtoValidator:AbstractValidator<AddAccountDto>
    {
        private readonly IAccountRepository _accountRepository;

        public AddAccountDtoValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            RuleFor(x => x.AccountNumber).NotNull().WithMessage("Boş Olamaz");
            RuleFor(x => x.AccountNumber).GreaterThan(0).WithMessage("Girilen Id 0'dan Büyük Olmalı");
            RuleFor(x => x.AccountNumber).Must(x =>
            {
                var account = _accountRepository.Get(x);
                if (account == null)
                    return true;
                else
                    return false;
            }).WithMessage("Bu Id'de Hesap Bulunuyor.");
            
        }
    }
}
