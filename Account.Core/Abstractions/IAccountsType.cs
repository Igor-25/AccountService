using AccountStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountStore.Core.Abstractions
{
    public interface IAccountsType
    {
        (Account account, string Error) CreateAccount(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address);
    }
}