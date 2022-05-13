using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Filters;
using Service.Manager;

namespace TringleCandidateProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerCustomBase
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        
        [HttpPost]
        public IActionResult Add(AddAccountDto newAccount)
        {
            return CreateActionResult(_accountManager.Create(newAccount));
        }
        [HttpGet("{accountNumber:int}")]
        [ServiceFilter(typeof(NotFoundAccountFilter))]
        public IActionResult GetById(int accountNumber)
        {
            return CreateActionResult(_accountManager.GetAccount(accountNumber));
        }
    }
}
