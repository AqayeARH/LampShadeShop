using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infra.EfCore;
using AccountManagement.Infra.EfCore.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infra.Configuration
{
    public static class AccountManagementIoc
    {
        public static void Configure(IServiceCollection service,string connectionString)
        {
            #region Account

            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<IAccountApplication, AccountApplication>();

            #endregion

            #region Role

            service.AddTransient<IRoleRepository, RoleRepository>();
            service.AddTransient<IRoleApplication, RoleApplication>();

            #endregion

            service.AddDbContext<AccountContext>(options =>
            {
                options.UseSqlServer(connectionString);

                //Use it in ef core 6
                options.ConfigureWarnings(warnings =>
                    warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
            });
        }
    }
}