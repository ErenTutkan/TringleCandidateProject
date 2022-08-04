using Core.DTOs;
using Core.Models;
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
            RuleFor(x => x.AccountNumber).NotNull().WithMessage("Hesap Numarası Boş Olamaz").WithErrorCode("404");
            RuleFor(x=>x.AccountNumber).Custom((x, context) =>
            {
                if ((!(int.TryParse(x.ToString(), out int value)) || value < 0))
                {
                    context.AddFailure($"{x} Hatalı Bir Hesap Numarası Girişi Yapılmıştır.");
                }
            });
            RuleFor(x => x.AccountNumber).GreaterThan(0).WithMessage("Hesap Numarası 0'dan Büyük Olmalı").WithErrorCode("404");
            RuleFor(x => x.AccountNumber).Must(x =>
            {
                var account = _accountRepository.Get(x);
                if (account == null)
                    return true;
                else
                    return false;
            }).WithMessage("Bu Id'de Hesap Bulunuyor.").WithErrorCode("404");
            RuleFor(x => x.OwnerName).NotEmpty().WithMessage("Hesap Sahibinin Adı Boş Bırakılamaz");
            RuleFor(x => x.OwnerName).NotNull().WithMessage("Hesap Sahibinin Adı Boş Bırakılamaz");
            RuleFor(x => x.OwnerName).MinimumLength(3).WithErrorCode("Hesap Sahibinin İsmi 3 Karakterden Fazla Olmalı.").WithErrorCode("404");
            RuleFor(x => x.OwnerName).MaximumLength(20).WithErrorCode("Hesap Sahibinin İsmi 20 Karakterden Fazla Olmamalı.").WithErrorCode("404");
            RuleFor(x => x.AccountNumber).GreaterThanOrEqualTo(0).WithMessage("Hesap Numarasına Girilen Değer 0'dan Az Olamaz.").WithErrorCode("404");
            RuleFor(x => x.AccountNumber).LessThanOrEqualTo(99999999).WithMessage("Hesap Numarasına Girilen Değer 8 Karakterden Fazla Olamaz.").WithErrorCode("404");
            RuleFor(x => x.CurrencyCode).NotNull().WithMessage("Bir Döviz Kodu Girmek Zorunludur.");
            
            RuleFor(x => x.CurrencyCode).IsInEnum();
        }
    }
}
