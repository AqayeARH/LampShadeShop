using _0.Framework.Application;
using _0.Framework.Application.Authentication;
using _0.Framework.Application.PasswordHasher;
using _0.Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        #region Constractor Injection

        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IRoleRepository _roleRepository;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, 
            IFileUploader fileUploader, IAuthenticationHelper authenticationHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authenticationHelper = authenticationHelper;
            _roleRepository = roleRepository;
        }

        #endregion
        public OperationResult Create(CreateAccountCommand command)
        {
            var operation = new OperationResult();
            if (_accountRepository.IsExist(x => x.Mobile == command.Mobile
                       || x.Username == command.Username))
            {
                return operation.Error("نام کاربری یا شماره تلفن وارد شده قبلا در سایت ثبت شده است");
            }

            var password = _passwordHasher.Hash(command.Password);

            var profile = _fileUploader.Upload(command.Profile, "account");

            var account = new Account(command.Fullname, command.Username, password, command.Mobile, profile,
                command.RoleId);
            _accountRepository.Create(account);
            _accountRepository.Save();
            return operation.Success();
        }

        public OperationResult Register(RegisterAccountCommand command)
        {
            var operation = new OperationResult();
            if (_accountRepository.IsExist(x => 
                    x.Mobile == command.Mobile || x.Username == command.Username))
            {
                return operation.Error("نام کاربری یا شماره تلفن وارد شده قبلا در سایت ثبت شده است");
            }

            var password = _passwordHasher.Hash(command.Password);

            var account = new Account(command.Fullname, command.Username, password, 
                command.Mobile, "", Roles.User);
            
            _accountRepository.Create(account);
            _accountRepository.Save();
            return operation.Success();
        }

        public OperationResult Edit(EditAccountCommand command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);

            if (account == null)
            {
                return operation.Error("حسابی با مشخصات ارسالی یافت نشد");
            }

            if (_accountRepository.IsExist(x =>
                    (x.Mobile == command.Mobile || x.Username == command.Username) && x.Id != command.Id))
            {
                return operation.Error("نام کاربری یا شماره تلفن وارد شده قبلا در سایت ثبت شده است");
            }

            var profile = _fileUploader.Upload(command.Profile, "account");
            account.Edit(command.Fullname, command.Username, command.Mobile, profile, command.RoleId);
            _accountRepository.Save();
            return operation.Success();
        }

        public OperationResult ChangePassword(ChangePasswordCommand command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);

            if (account == null)
            {
                return operation.Error("حسابی با مشخصات ارسالی یافت نشد");
            }

            if (!command.Password.Equals(command.RePassword))
            {
                return operation.Error("کلمه عبور با تکرار آن همخوانی ندارد");
            }

            var password = _passwordHasher.Hash(command.Password);

            account.ChangePassword(password);
            _accountRepository.Save();
            return operation.Success();
        }

        public EditAccountCommand GetForEdit(long id)
        {
            return _accountRepository.GetDetailForEdit(id);
        }

        public List<AccountViewModel> GetList(AccountSearchModel searchModel)
        {
            return _accountRepository.GetByFilter(searchModel);
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public OperationResult Login(LoginViewModel loginModel)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(loginModel.Username);

            if (account == null)
            {
                return operation.Error("کاربری با مشخصات ارسالی یافت نشد");
            }

            var (verified, _) = _passwordHasher.Check(account.Password, loginModel.Password);

            if (!verified)
            {
                return operation.Error("کاربری با مشخصات ارسالی یافت نشد");
            }

            var permissions = _roleRepository.GetBy(account.RoleId).Permissions
                .Select(x => x.Code).ToList();

            _authenticationHelper.Signin(new AuthenticationViewModel()
            {
                Fullname = account.Fullname,
                Username = account.Username,
                Id = account.Id,
                RoleId = account.RoleId,
                RememberMe = loginModel.RememberMe,
                Permissions = permissions,
                Mobile = account.Mobile
            });

            return operation.Success();
        }

        public void Logout()
        {
            _authenticationHelper.SignOut();
        }
    }
}