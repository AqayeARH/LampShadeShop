using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.ColleaguesDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.ColleaguesDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infra.EfCore;
using DiscountManagement.Infra.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Infra.Configuration
{
    public class DiscountManagementIoc
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            #region Customer Discount

            service.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
            service.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();

            #endregion

            #region Colleagues Discount

            service.AddTransient<IColleaguesDiscountRepository, ColleaguesDiscountRepository>();
            service.AddTransient<IColleaguesDiscountApplication, ColleaguesDiscountApplication>();

            #endregion

            service.AddDbContext<DiscountContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
