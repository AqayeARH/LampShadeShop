namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccountCommand : CreateAccountCommand
    {
        public long Id { get; set; }
    }
}
