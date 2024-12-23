namespace AccountStore.API.Contracts
{
    public record AccountsRequest(
       string LastName,
       string FirstName,
       string Patronymic,
       string DateOfBbirth,
       string PassportNumber,
       string PhoneNumber,
       string Email,
       string Address);


}
