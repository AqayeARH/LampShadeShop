using AccountManagement.Application.Contracts.Account;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infra.AccountAcl
{
    public class ShopAccountAcl: IShopAccountAcl
    {
        #region Constractor Injection

        private readonly IAccountApplication _accountApplication;
        public ShopAccountAcl(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        #endregion
        public (long accountId, string name, string mobile) GetAccountBy(long id)
        {
            var account = _accountApplication.GetAccountBy(id);

            return (account.Id, account.Fullname, account.Mobile);
        }
    }
}