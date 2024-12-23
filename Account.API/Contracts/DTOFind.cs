using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountStore.Core.Models
{
    public class DTOFind
    {
        //4. Поля, по которым можно искать пользователя: фамилия, имя, отчество, телефон, электронная почта.
        //Должен быть функционал поиска по одному, либо по нескольким полям из этого списка.
        //private DTOFind(string lastName, string firstName, string patronymic, string phoneNumber, string email)
        //{
        //    LastName = lastName;
        //    FirstName = firstName;
        //    Patronymic = patronymic;
        //    PhoneNumber = phoneNumber;
        //    Email = email;
        //}

        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;


        //public static (DTOFind dtoFind, string Error) Create(string lastName, string firstName, string patronymic, string phoneNumber, string email)
        //{
        //    var error = string.Empty;
        //    var dtoFind = new DTOFind(lastName, firstName, patronymic, phoneNumber, email);

        //    return (dtoFind, error);
        //}

        

    }
}
