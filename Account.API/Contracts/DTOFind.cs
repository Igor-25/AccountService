using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DefaultValue("Иванов")]
        public string LastName { get; set; } = string.Empty;
        [DefaultValue("")]
        public string FirstName { get; set; } = string.Empty;
        [DefaultValue("")]
        public string Patronymic { get; set; } = string.Empty;
        [DefaultValue("")] 
        public string PhoneNumber { get; set; } = string.Empty;
        [DefaultValue("")] 
        public string Email { get; set; } = string.Empty;



    }
}
