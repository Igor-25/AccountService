using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountStore.Core.Models
{
    public class Account
    {
        private Account(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            DateOfBbirth = dateOfBbirth;
            PassportNumber = passportNumber;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
        }

        public Guid Id { get; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string DateOfBbirth { get; set; } = string.Empty;

        //[RegularExpression(@"^\d{4}\s\d{6}$")]
        public string PassportNumber { get; set; } = string.Empty;

        //[RegularExpression(@"^[7]\d{10}$")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;


        public static (Account account, string Error) Create(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address)
        {
            var error = string.Empty;
            //if (string.IsNullOrEmpty(lastName)&&
            //    string.IsNullOrEmpty(firstName)&&
            //    string.IsNullOrEmpty(patronymic)&&
            //    string.IsNullOrEmpty(dateOfBbirth)&&
            //    string.IsNullOrEmpty(passportNumber)&&
            //    string.IsNullOrEmpty(email)&&
            //    string.IsNullOrEmpty(address))
            //    { error = "All fields cannot be empty! "; }

            var account = new Account(id, lastName, firstName, patronymic, dateOfBbirth, passportNumber, phoneNumber, email, address);
                        
            //var results = new List<ValidationResult>();
            //var context = new ValidationContext(account);
            //if (Validator.TryValidateObject(account, context, results, true))
            //    error = "проходит валидацию";
            //else
            //    error = "НЕ проходит валидацию";

            return (account, error);
        }

        //public static (Account account, string Error) CreateMobile(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address)
        //{
        //    var error = string.Empty;
        //    var account = new Account(id, lastName, firstName, patronymic, dateOfBbirth, passportNumber, phoneNumber, email, address);

        //    var results = new List<ValidationResult>();
        //    var context = new ValidationContext(account);
        //    if (Validator.TryValidateObject(account, context, results, true))
        //        error = "проходит валидацию";
        //    else
        //        error = "НЕ проходит валидацию";

        //    return (account, error);
        //}

        //public static (Account account, string Error) CreateWeb(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address)
        //{
        //    var error = string.Empty;
        //    var account = new Account(id, lastName, firstName, patronymic, dateOfBbirth, passportNumber, phoneNumber, email, address);

        //    var results = new List<ValidationResult>();
        //    var context = new ValidationContext(account);
        //    if (Validator.TryValidateObject(account, context, results, true))
        //        error = "проходит валидацию";
        //    else
        //        error = "НЕ проходит валидацию";

        //    return (account, error);
        //}



    }
}
