using AccountStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AccountStore.Core.Abstractions
{
    public interface IAccountsRepository
    {
       
        Task<List<Account>> Get();
        Task<Account> GetById(Guid id);
        Task<Account> GetByPhone(Account account);
        Task<Account> GetByMail(Account account);
        
        Task<List<Account>> AccFind(Account account);

        Task<Guid> Create(Account account);
        Task<Guid> Delete(Guid id);

        Task<string> DelAll();


        //Task<Guid> Update(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address);

    }
}
