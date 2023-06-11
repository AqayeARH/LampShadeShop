using _0.Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccountCommand command);
        OperationResult Register(RegisterAccountCommand command);
        OperationResult Edit(EditAccountCommand command);
        OperationResult ChangePassword(ChangePasswordCommand command);
        EditAccountCommand GetForEdit(long id);
        List<AccountViewModel> GetList(AccountSearchModel searchModel);
        OperationResult Login(LoginViewModel loginModel);
        void Logout();
    }
}
