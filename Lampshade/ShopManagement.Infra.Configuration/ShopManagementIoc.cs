using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infra.EfCore;
using ShopManagement.Infra.EfCore.Repositories;

namespace ShopManagement.Infra.Configuration
{
    public static class ShopManagementIoc
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            #region Product Category

            service.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            service.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            #endregion

            service.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}