using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Filters
{
    public class NotFoundAccountFilter:ActionFilterAttribute
    {
        private readonly IAccountRepository _accountRepository;

        public NotFoundAccountFilter(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

           var id= context.ActionArguments["accountNumber"];
            var account = _accountRepository.Get((int)id);
            if(account != null)
            {
                await next.Invoke();
                return;
            }
            context.Result =new NotFoundObjectResult(ResponseDto<NoContent>.Fail(new List<string> { $"{id} Numaralı Hesap Bulunamamıştır" }, 404));
        }
    }
}
