using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Filters
{
    public class ValidationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors=context.ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList();
                context.Result = new ObjectResult(ResponseDto<NoContent>.Fail(errors, 400)) { StatusCode=400};
            }
        }
    }
}
