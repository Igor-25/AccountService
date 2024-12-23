using AccountStore.Core.Abstractions;
using AccountStore.Core.Models;
using Azure.Core;
using System.Net;

namespace AccountStore.API.Contracts
{
    public class AccountValidation : IAccountValidation
    {

        public bool CreateAccountValidation(AccountsRequest request, string xdevice)
        {
            switch (xdevice)
            {
                case "mail":
                    return
                AccountMail.CreateMail(
                Guid.NewGuid(),
                request.LastName,
                request.FirstName,
                request.Patronymic,
                request.DateOfBbirth,
                request.PassportNumber,
                request.PhoneNumber,
                request.Email,
                request.Address);

                case "mobile":
                    return
                AccountMobile.CreateMobile(
                Guid.NewGuid(),
                request.LastName,
                request.FirstName,
                request.Patronymic,
                request.DateOfBbirth,
                request.PassportNumber,
                request.PhoneNumber,
                request.Email,
                request.Address);

                case "web":
                    return
                AccountWeb.CreateWeb(
                Guid.NewGuid(),
                request.LastName,
                request.FirstName,
                request.Patronymic,
                request.DateOfBbirth,
                request.PassportNumber,
                request.PhoneNumber,
                request.Email,
                request.Address); ;

                default:
                    return false;

            }


        }


    }
}
