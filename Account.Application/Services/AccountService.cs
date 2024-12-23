using AccountStore.Core.Abstractions;
using AccountStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static AccountStore.Application.Services.AccountService;

namespace AccountStore.Application.Services
{
    public class AccountService : IAccountsService
    {

        private readonly IAccountsRepository _accountsRepository;
        public AccountService(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _accountsRepository.Get();
        }

        public async Task<Account> GetAccById(Guid id)
        {
            return await _accountsRepository.GetById(id);
        }

        public async Task<Account> GetByPhoneNumber(Account account)
        {
            return await _accountsRepository.GetByPhone(account);
        }

        public async Task<Account> GetByEmail(Account account)
        {
            return await _accountsRepository.GetByMail(account);
        }

        public async Task<List<Account>> AccountFind(Account account)

        {
            return await _accountsRepository.AccFind(account);
        }


        public async Task<Guid> CreateAccount(Account account)
        {
            return await _accountsRepository.Create(account);
        }


        public async Task<Guid> DeleteAccount(Guid id)
        {
            return await _accountsRepository.Delete(id);
        }

        public async Task<string> DeleteAll()
        {
            return await _accountsRepository.DelAll();
        }

    }
}
