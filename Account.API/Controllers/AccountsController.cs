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
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Net.WebRequestMethods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountStore.API.Controllers
{

    //[Route("api/[controller]/[action]")]
    [Route("api/[controller]")]
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

        /// <summary>
        /// Поиск всех аккаунтов
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<ActionResult<List<AccountsResponce>>> GetAccounts()
        {
            var accounts = await _accountsService.GetAllAccounts();

            var response = accounts.Select(b => new AccountsResponce(b.Id, b.LastName, b.FirstName, b.Patronymic, b.DateOfBbirth, b.PassportNumber, b.PhoneNumber, b.Email, b.Address));

            return Ok(response);

        }


        /// <summary>
        /// Поиск аккаунта по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("(id:guid)")]
        public async Task<ActionResult<Account>> GetAccountById(Guid id)
        {
        
            var account = await _accountsService.GetAccById(id);

            if (account != null)
            {
                //return Ok($"Есть такой аккаунт {id}");
                return new ObjectResult(account);
            }
            return BadRequest($"Нет такого аккаунта {id}");
        }



        /// <summary>
        /// Создание аккаунта
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     
        ///     {
        ///        "lastName": "",
        ///        "firstName": "",
        ///        "patronymic": "",
        ///        "dateOfBbirth": "",
        ///        "passportNumber": "",
        ///        "phoneNumber": "",
        ///        "email": "",
        ///        "address": ""
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Аккаунт</param>
        /// <returns></returns>
        [HttpPost("Create")]
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

        /// <summary>
        /// Поиск аккаунта
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     
        ///     {
        ///        "lastName": "",
        ///        "firstName": "",
        ///        "patronymic": "",
        ///        "phoneNumber": "",
        ///        "email": ""
        ///     }
        /// 
        /// </remarks>
        /// <param name="dtoFind">Данные для поиска</param>
        /// <returns></returns>
        [HttpPost("Find")]
        public async Task<ActionResult<List<string>>> AccountFind([FromBody] DTOFind dtoFind)
        {
            var account = Account.Create(Guid.NewGuid(), dtoFind.LastName, dtoFind.FirstName, dtoFind.Patronymic, "", "", dtoFind.PhoneNumber, dtoFind.Email, "").account;
            var accountFind = await _accountsService.AccountFind(account);
            if (accountFind.Count == 0)
            {
                return new ObjectResult(accountFind);
            }
            //return Ok($"Есть такой аккаунт {request.LastName}, {request.FirstName}, {request.Patronymic}, {request.PhoneNumber}, {request.Email}");
            return new ObjectResult(accountFind);



            //var accountDto = DTOFind.Create(dtoFind.LastName, dtoFind.FirstName, dtoFind.Patronymic, dtoFind.PhoneNumber, dtoFind.Email).dtoFind;
            //var accountFind = await _accountsService.AccountFind(accountDto);
            //if (accountFind.Count == 0)
            //{
            //    return new ObjectResult(accountFind);
            //}
            ////return Ok($"Есть такой аккаунт {request.LastName}, {request.FirstName}, {request.Patronymic}, {request.PhoneNumber}, {request.Email}");
            //return new ObjectResult(accountFind);
        }


        ///// <summary>
        ///// Поиск аккаунта
        ///// </summary>
        ///// <remarks>
        ///// Пример запроса (находит всех):
        /////     
        /////     {
        /////        "lastName" : "",
        /////        "firstName" : "",
        /////        "patronymic" : "",
        /////        "dateOfBbirth" : "",
        /////        "passportNumber" : "",
        /////        "phoneNumber" : "",
        /////        "email" : "",
        /////        "address" : ""
        /////     }
        ///// 
        ///// </remarks>
        ///// <param name="request">Аккаунт</param>
        ///// <returns></returns>
        //[HttpPost("Find")]
        //public async Task<ActionResult<List<string>>> AccountFind([FromBody] AccountsRequest request)
        //{
        //    var account = Account.Create(Guid.NewGuid(), request.LastName, request.FirstName, request.Patronymic, request.DateOfBbirth, request.PassportNumber, request.PhoneNumber, request.Email, request.Address).account;
        //    var accountFind = await _accountsService.AccountFind(account);
        //    if (accountFind.Count == 0)
        //    {
        //        return new ObjectResult(accountFind);
        //    }
        //    //return Ok($"Есть такой аккаунт {request.LastName}, {request.FirstName}, {request.Patronymic}, {request.PhoneNumber}, {request.Email}");
        //    return new ObjectResult(accountFind);
        //}

        /// <summary>
        /// Удаление аккаунта по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpDelete("(id:guid)")]
        public async Task<ActionResult<Guid>> DeleteAccount(Guid id)
        {
            Guid idOut =await _accountsService.DeleteAccount(id);

            if (idOut == Guid.Empty)
            {
                return BadRequest($"Нет такого Id");
            }

            return Ok($"Удален аккаунт {idOut}");
        }


        /// <summary>
        /// Удаление всех аккаунтов
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> DeleteAll()
        {
            return Ok(await _accountsService.DeleteAll());
        }





    }
}
