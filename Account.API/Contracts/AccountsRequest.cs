using System.ComponentModel;

namespace AccountStore.API.Contracts
{
    public record AccountsRequest(
       [DefaultValue("Степашин")]
       string LastName,
       [DefaultValue("Степан")]
       string FirstName,
       [DefaultValue("Олегович")]
       string Patronymic,
       [DefaultValue("12/01/2001")]
       string DateOfBbirth,
       [DefaultValue("4444 666666")]
       string PassportNumber,
       [DefaultValue("71234567890")]
       string PhoneNumber,
       [DefaultValue("mail9@yaru")]
       string Email,
       [DefaultValue("Тула")]
       string Address);
}
