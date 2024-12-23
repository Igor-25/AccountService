
using AccountStore.Core.Models;
using AccountStore.DataAccess.Entites;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountStore.DataAccess
{
    public class AccountDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; } = null!;
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
        .UseSeeding((context, _) =>
        {
            context.Set<AccountEntity>().Add(new AccountEntity { Id = Guid.NewGuid(), LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", DateOfBbirth = "01/02/1975", PassportNumber = "4578 468324", PhoneNumber = "70000000000", Email = "mail0@ya.ru", Address = "Москва" });
            context.Set<AccountEntity>().Add(new AccountEntity { Id = Guid.NewGuid(), LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", DateOfBbirth = "20/05/1990", PassportNumber = "6734 974567", PhoneNumber = "71111111111", Email = "mail1@ya.ru", Address = "Тула" });
            context.Set<AccountEntity>().Add(new AccountEntity { Id = Guid.NewGuid(), LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", DateOfBbirth = "16/05/1993", PassportNumber = "4566 782463", PhoneNumber = "72222222222", Email = "mail2@ya.ru", Address = "Звенигород" });
            context.Set<AccountEntity>().Add(new AccountEntity { Id = Guid.NewGuid(), LastName = "Медведев", FirstName = "Дмитрий", Patronymic = "Анатольевич", DateOfBbirth = "12/04/1982", PassportNumber = "2341 589045", PhoneNumber = "73333333333", Email = "mail3@ya.ru", Address = "Москва" });

            context.SaveChanges();
        });



    }
}
