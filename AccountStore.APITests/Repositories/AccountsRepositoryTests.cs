using Xunit;
using AccountStore.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountStore.Core.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using AccountStore.Core.Abstractions;
using Assert = Xunit.Assert;
using AccountStore.DataAccess.Entites;
using AccountStore.DataAccess;
using AccountStore.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using AccountStore.APITests.TestPriority;
using Microsoft.Identity.Client;
using System.Security.Principal;
using System.Net;

namespace AccountStore.DataAccess.Repositories.Tests
{
    //[TestCaseOrderer("TestOrderExamples.TestCaseOrdering.AlphabeticalOrderer", "TestOrderExamples")]
    public class AccountsRepositoryTests : IDisposable
    {

        string connection = "Server=(localdb)\\mssqllocaldb;Database=accountdb;Trusted_Connection=True";
        private readonly DbContextOptions<AccountDbContext> _options;
        private readonly AccountDbContext _context;
        private readonly AccountsRepository _accountsRepository;
        public AccountsRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AccountDbContext>()
                        .UseSqlServer(connection)
                        .Options;
            _context = new AccountDbContext(_options);

            _accountsRepository = new AccountsRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }


        async Task StartTest()
        {
            await _accountsRepository.DelAll();
            await _context.AddAsync(new AccountEntity { Id = Guid.NewGuid(), LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", DateOfBbirth = "01/02/1975", PassportNumber = "4578 468324", PhoneNumber = "70000000000", Email = "mail0@ya.ru", Address = "Москва" });
            await _context.AddAsync(new AccountEntity { Id = Guid.NewGuid(), LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", DateOfBbirth = "20/05/1990", PassportNumber = "6734 974567", PhoneNumber = "71111111111", Email = "mail1@ya.ru", Address = "Тула" });
            await _context.AddAsync(new AccountEntity { Id = Guid.NewGuid(), LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", DateOfBbirth = "16/05/1993", PassportNumber = "4566 782463", PhoneNumber = "72222222222", Email = "mail2@ya.ru", Address = "Звенигород" });
            await _context.AddAsync(new AccountEntity { Id = Guid.NewGuid(), LastName = "Медведев", FirstName = "Дмитрий", Patronymic = "Анатольевич", DateOfBbirth = "12/04/1982", PassportNumber = "2341 589045", PhoneNumber = "73333333333", Email = "mail3@ya.ru", Address = "Москва" });
            await _context.SaveChangesAsync();
        }

        //[Fact, TestPriority(1)]
        [Fact()]
        public async Task GetTest()
        {
            // Arrange
            await StartTest();

            // Act
            var listAcc = await _accountsRepository.Get();

            // Assert
            Assert.Equal(4, listAcc.Count);
            
            Assert.NotNull(listAcc.First(u => u.LastName == "Иванов"));
            Assert.NotNull(listAcc.First(u => u.LastName == "Петров"));
            Assert.NotNull(listAcc.First(u => u.LastName == "Сидоров"));
            Assert.NotNull(listAcc.First(u => u.LastName == "Медведев"));

        }

       

        [Fact()]
        public async Task GetByIdTest()
        {
            // Arrange
            await StartTest();
            var listAcc = await _accountsRepository.Get();
            Guid Id = listAcc[0].Id;
            string firstName = listAcc[0].FirstName;
            // Act

            var result = await _accountsRepository.GetById(Id);

            // Assert

            var accountIsType = Assert.IsType<Account>(result);
            Assert.Equal(firstName, result.FirstName);

        }

        [Fact()]
        public async Task GetByPhoneTest()
        {
            // Arrange
            await StartTest();
            var listAcc = await _accountsRepository.Get();
            Account account = listAcc[0];
            string item = listAcc[0].PhoneNumber;
            
            // Act

            var result = await _accountsRepository.GetByPhone(account);

            // Assert
            Assert.Equal(item, result.PhoneNumber);
         
        }

        [Fact()]
        public async Task GetByMailTest()
        {
            // Arrange
            await StartTest();
            var listAcc = await _accountsRepository.Get();
            Account account = listAcc[0];
            string item = listAcc[0].Email;

            // Act

            var result = await _accountsRepository.GetByPhone(account);

            // Assert
            Assert.Equal(item, result.Email);
        }

        [Fact()]
        public async Task AccFindTest()
        {
            // Arrange
            await StartTest();
            var listAcc = await _accountsRepository.Get();
            Account account = listAcc[0];
            string firstName = listAcc[0].FirstName;
            string phone = listAcc[0].PhoneNumber;

            // Act
            var result = await _accountsRepository.AccFind(account);

            var accountFind = await _context.Accounts.ToListAsync();
            accountFind = accountFind.Where(p => p.FirstName == firstName).ToList();
            accountFind = accountFind.Where(p => p.PhoneNumber == phone).ToList();

            // Assert
            Assert.Equal(result.Count, accountFind.Count);
        }

        [Fact()]
        public async Task CreateTest()
        {

            // Arrange
            await StartTest();

            int count = _context.Accounts.Count();
            var account = Account.Create(Guid.NewGuid(), "Test", "Test", "Test", "01/02/1975", "0000 000000", "77777777777", "test@ya.ru", "Test").account;

            // Act
            await _accountsRepository.Create(account);

            // Assert

            Assert.Equal(count + 1, _context.Accounts.Count());

            
        }

        [Fact()]
        public async Task DeleteTest()
        {
            // Arrange
            await StartTest();
            var listAcc = await _accountsRepository.Get();
            Guid Id = listAcc[0].Id;
            string firstName = listAcc[0].FirstName;
            int count =_context.Accounts.Count();
            
            // Act
            await _accountsRepository.Delete(Id);

            // Assert

            Assert.Equal(count-1, _context.Accounts.Count());
        }

        [Fact()]
        public async Task DelAllTest()
        {
            // Arrange
            await StartTest();
            var accounts= await _context.Accounts.ToListAsync();

            var listAcc = await _accountsRepository.Get();
            Account account = listAcc[0];

            // Act
            await _accountsRepository.DelAll();

            // Assert
            
            Assert.Equal(0, _context.Accounts.Count());
            await StartTest();


        }





    }
}