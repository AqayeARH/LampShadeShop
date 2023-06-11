using _0.Framework.Application;
using _0.Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infra.EfCore.Repositories
{
    public class AccountRepository : EfCoreGenericRepository<long, Account>,IAccountRepository
    {
        #region Constractor Injection

        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public EditAccountCommand GetDetailForEdit(long id)
        {
            return _context.Accounts
                .Select(x => new EditAccountCommand()
                {
                    Fullname = x.Fullname,
                    Id = x.Id,
                    Mobile = x.Mobile,
                    RoleId = x.RoleId,
                    Username = x.Username
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> GetByFilter(AccountSearchModel searchModel)
        {
            var query = _context.Accounts
                .Include(x=>x.Role)
                .Select(x => new AccountViewModel()
                {
                    Id = x.Id,
                    Mobile = x.Mobile,
                    Profile = x.Profile,
                    RoleId = x.RoleId,
                    Username = x.Username,
                    Fullname = x.Fullname,
                    Role = x.Role.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                }).AsQueryable();

            #region Filtering

            if (!string.IsNullOrEmpty(searchModel.Fullname))
            {
                query = query.Where(x => x.Fullname.Contains(searchModel.Fullname));
            }

            if (!string.IsNullOrEmpty(searchModel.Username))
            {
                query = query.Where(x => x.Username.Contains(searchModel.Username));
            }

            if (!string.IsNullOrEmpty(searchModel.Mobile))
            {
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }

            if (searchModel.RoleId > 0)
            {
                query = query.Where(x => x.RoleId == searchModel.RoleId);
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public Account GetBy(string username)
        {
            return _context.Accounts.SingleOrDefault(x => x.Username == username);
        }
    }
}
