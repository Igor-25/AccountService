using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AccountStore.Core.Abstractions;
using AccountStore.Core.Models;
using AccountStore.DataAccess.Entites;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AccountStore.DataAccess.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AccountDbContext _context;
        public AccountsRepository(AccountDbContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> Get()
        {
            var accountEntities = await _context.Accounts
                .AsNoTracking()
                .ToListAsync();
            var accounts = accountEntities
                .Select(b => Account.Create(b.Id, b.LastName, b.FirstName, b.Patronymic, b.DateOfBbirth, b.PassportNumber, b.PhoneNumber, b.Email, b.Address).account)
                .ToList();
            return accounts;
        }


        public async Task<Account> GetById(Guid id)
        {
            var entity = await _context.Accounts.FirstOrDefaultAsync(u => u.Id == id);

            var accounts = Account.Create(entity.Id, entity.LastName, entity.FirstName, entity.Patronymic, entity.DateOfBbirth, entity.PassportNumber, entity.PhoneNumber, entity.Email, entity.Address).account;

            return accounts;
        }

        public async Task<Account> GetByPhone(Account account)
        {
            var entity = await _context.Accounts.FirstOrDefaultAsync(u => u.PhoneNumber == account.PhoneNumber);
            if (entity == null) return null;

            var accounts = Account.Create(entity.Id, entity.LastName, entity.FirstName, entity.Patronymic, entity.DateOfBbirth, entity.PassportNumber, entity.PhoneNumber, entity.Email, entity.Address).account;
            return accounts;
        }

        public async Task<Account> GetByMail(Account account)
        {
            var entity = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == account.Email);

            if (entity == null) return null;
            var accounts = Account.Create(entity.Id, entity.LastName, entity.FirstName, entity.Patronymic, entity.DateOfBbirth, entity.PassportNumber, entity.PhoneNumber, entity.Email, entity.Address).account;
            return accounts;
        }

        public async Task<List<Account>> AccFind(Account account)
        {

            var accountFind = await _context.Accounts.ToListAsync();

            if (!string.IsNullOrEmpty(account.LastName) && (account.LastName != "string"))
            {
                accountFind = accountFind.Where(p => p.LastName == account.LastName).ToList();
            }

            if (!string.IsNullOrEmpty(account.FirstName) && (account.FirstName != "string"))
            {
                accountFind = accountFind.Where(p => p.FirstName == account.FirstName).ToList();
            }

            if (!string.IsNullOrEmpty(account.Patronymic) && (account.Patronymic != "string"))
            {
                accountFind = accountFind.Where(p => p.Patronymic == account.Patronymic).ToList();
            }

            if (!string.IsNullOrEmpty(account.PhoneNumber) && (account.PhoneNumber != "string"))
            {
                accountFind = accountFind.Where(p => p.PhoneNumber == account.PhoneNumber).ToList();
            }

            if (!string.IsNullOrEmpty(account.Email) && (account.Email != "string"))
            {
                accountFind = accountFind.Where(p => p.Email == account.Email).ToList();
            }
            List<Account> accounts = new List<Account>();

            foreach (var entity in accountFind)
            {
                accounts.Add(Account.Create(entity.Id, entity.LastName, entity.FirstName, entity.Patronymic, entity.DateOfBbirth, entity.PassportNumber, entity.PhoneNumber, entity.Email, entity.Address).account);
            }
            return accounts;
            

        }



        public async Task<Guid> Create(Account account)
        {
            var accountEntity = new AccountEntity
            {
                Id = account.Id,
                LastName = account.LastName,
                FirstName = account.FirstName,
                Patronymic = account.Patronymic,
                DateOfBbirth = account.DateOfBbirth,
                PassportNumber = account.PassportNumber,
                PhoneNumber = account.PhoneNumber,
                Email = account.Email,
                Address = account.Address

            };

            await _context.Accounts.AddAsync(accountEntity);
            await _context.SaveChangesAsync();

            return accountEntity.Id;
        }


        public async Task<Guid> Delete(Guid id)
        {
            await _context.Accounts
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;

        }


        public async Task<string> DelAll()
        {
            await _context.Accounts.ExecuteDeleteAsync();

            return "Всё удалено!";
        }


    }
}
