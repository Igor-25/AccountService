

using AccountStore.Core.Models;
using System.Security.Principal;

namespace AccountStore.Core.Abstractions
{
    public interface IAccountsService
    {

        
        
        Task<List<Account>> GetAllAccounts();
        Task<Account> GetAccById(Guid id);
        Task<Account> GetByPhoneNumber(Account account);
        Task<Account> GetByEmail(Account account);
        Task<List<Account>> AccountFind(Account account);
        Task<Guid> CreateAccount(Account account);
        Task<Guid> DeleteAccount(Guid id);

        Task<string> DeleteAll();


        //Task<Guid> Update(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address);
    }
}
