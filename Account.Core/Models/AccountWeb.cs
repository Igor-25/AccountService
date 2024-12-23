using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountStore.Core.Models
{
    public class AccountWeb
    {
        private AccountWeb(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address)
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
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string Patronymic { get; set; } = string.Empty;
        [Required]
        public string DateOfBbirth { get; set; } = string.Empty;
        
        [RegularExpression(@"^\d{4}\s\d{6}$")]
        public string PassportNumber { get; set; } = string.Empty;
        
        [RegularExpression(@"^[7]\d{10}$")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;


        public static bool CreateWeb(Guid id, string lastName, string firstName, string patronymic, string dateOfBbirth, string passportNumber, string phoneNumber, string email, string address)
        {
            var error = string.Empty;
            var account = new AccountWeb(id, lastName, firstName, patronymic, dateOfBbirth, passportNumber, phoneNumber, email, address);

            var results = new List<ValidationResult>();
            var context = new ValidationContext(account);
            if (Validator.TryValidateObject(account, context, results, true))
                return true;
            else
                return false;

        }



    }
}
