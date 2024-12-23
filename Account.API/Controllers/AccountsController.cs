using AccountStore.API.Contracts;
using AccountStore.Core.Abstractions;
using AccountStore.Core.Models;
using AccountStore.DataAccess;
using AccountStore.DataAccess.Entites;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Net.WebRequestMethods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountStore.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountsService _accountsService;
        private readonly IAccountValidation _accountValidation;
        public AccountsController(IAccountsService accountService, IAccountValidation accountValidation)
        {
            _accountsService = accountService;
            _accountValidation = accountValidation;
            
        }


        [HttpGet]
        public async Task<ActionResult<List<AccountsResponce>>> GetAccounts()
        {
            var accounts = await _accountsService.GetAllAccounts();

            var response = accounts.Select(b => new AccountsResponce(b.Id, b.LastName, b.FirstName, b.Patronymic, b.DateOfBbirth, b.PassportNumber, b.PhoneNumber, b.Email, b.Address));

            return Ok(response);

        }


        [HttpGet("(id:guid)")]
        public async Task<ActionResult<Guid>> GetAccountById(Guid id)
        //public async Task<Guid> Get(Guid id)
        {
            //var userId = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == id);

            var accounts = await _accountsService.GetAccById(id);

            //var accountEntities = await _context.Accounts.ToListAsync();
            //var userMobele = accountEntities.FirstOrDefault(u => u.PhoneNumber == request.PhoneNumber);

            if (accounts != null)
            {
                return Ok($"Есть такой аккаунт {id}");
            }
            return BadRequest($"Нет такого аккаунта {id}");
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAccount([FromBody] AccountsRequest request)
        {
            HttpContext httpcontext = HttpContext;
            string xdevice = httpcontext.Request.Headers["X-Device"];

            if (string.IsNullOrEmpty(xdevice))
            {
                return BadRequest($"Пустой xdevice {xdevice}");
            }

            bool result = _accountValidation.CreateAccountValidation(request, xdevice);


            if (!result) return BadRequest("Валидация не прошла");

            
            var account = Account.Create(Guid.NewGuid(), request.LastName, request.FirstName, request.Patronymic, request.DateOfBbirth, request.PassportNumber, request.PhoneNumber, request.Email, request.Address).account;

            var userMobele = await _accountsService.GetByPhoneNumber(account);

            
            if (userMobele != null)
            {
                return BadRequest($"Есть уже аккаунт с таким номером телефона {request.PhoneNumber}");
            }


            var userEmail = await _accountsService.GetByEmail(account);

            if (userEmail != null)
            {
                return BadRequest($"Есть уже аккаунт с такой почтой {request.Email}");
            }

           
            var bookId = await _accountsService.CreateAccount(account);

            return Ok($"Аккаунт добавлен: {bookId}");
        }


        [HttpPost]
        public async Task<ActionResult<List<string>>> AccountFind([FromBody] AccountsRequest request)
        {
            var account = Account.Create(Guid.NewGuid(), request.LastName, request.FirstName, request.Patronymic, request.DateOfBbirth, request.PassportNumber, request.PhoneNumber, request.Email, request.Address).account;

            var accountFind = await _accountsService.AccountFind(account);


            if (accountFind.Count == 0)
            {
                return new ObjectResult(accountFind);
            }

            //return Ok($"Есть такой аккаунт {request.LastName}, {request.FirstName}, {request.Patronymic}, {request.PhoneNumber}, {request.Email}");

            return new ObjectResult(accountFind);

        }

        [HttpDelete("(id:guid)")]
        public async Task<ActionResult<Guid>> DeleteAccount(Guid id)
        {
            return Ok(await _accountsService.DeleteAccount(id));

        }


        [HttpDelete]
        public async Task<ActionResult<string>> DeleteAll()
        {
            return Ok(await _accountsService.DeleteAll());
        }





    }
}
