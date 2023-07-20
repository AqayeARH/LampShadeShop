using System.Linq.Expressions;
using _0.Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IGenericRepository<long, Account>
    {
        EditAccountCommand GetDetailForEdit(long id);
        List<AccountViewModel> GetByFilter(AccountSearchModel searchModel);
        Account GetBy(string username);
        List<AccountViewModel> GetAccounts();
    }
}
