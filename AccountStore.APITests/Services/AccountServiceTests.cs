using Xunit;
using AccountStore.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountStore.DataAccess.Entites;
using AccountStore.Core.Abstractions;
using Moq;
using AccountStore.Core.Models;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Assert = Xunit.Assert;
using AccountStore.DataAccess.Repositories;
using AccountStore.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AccountStore.Application.Services.Tests
{
    public class AccountServiceTests
    {


        [Fact()]
        public async Task GetAllAccountsTest()
        {
            
            // Arrange
            var mock = new Mock<IAccountsRepository>();
            mock.Setup(repo => repo.Get()).Returns(GetTestAccounts());
            var accountService = new AccountService(mock.Object);

            // Act
            var result = accountService.GetAllAccounts();

            // Assert
            Assert.Equal(1, (await result).Count);

            var num = Assert.IsType<int>((await result).Count);

            var TaskListAccount = Assert.IsType<Task<List<Account>>>(result);

            var ListAccount = Assert.IsType<List<Account>>(await result);

            Assert.Equal("Иван", ListAccount[0].FirstName);

            Assert.NotNull(result);


        }

        public async Task<List<Account>> GetTestAccounts()
        {
            var account = new List<Account>{
            Account.Create(Guid.NewGuid(), "Иванов", "Иван", "-", "-", "-", "-", "-", "-").account,

            };
            return account;
        }


    }
}