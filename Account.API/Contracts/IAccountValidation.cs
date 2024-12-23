using AccountStore.API.Contracts;
using AccountStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountStore.API.Contracts
{
    public interface IAccountValidation
    {

        bool CreateAccountValidation(AccountsRequest request, string xdevice);
        
        
        //Task<Guid> CreateAccount(Account account);
        ////Task<Guid> DeleteAccount(Guid id);
        //Task<List<Account>> GetAllAccounts();
        ////Task<Guid> Update(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address);


    }
}
