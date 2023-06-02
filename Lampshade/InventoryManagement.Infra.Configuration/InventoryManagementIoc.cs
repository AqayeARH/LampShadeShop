using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infra.EfCore;
using InventoryManagement.Infra.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infra.Configuration
{
    public class InventoryManagementIoc
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IInventoryRepository, InventoryRepository>();
            service.AddTransient<IInventoryApplication, InventoryApplication>();

            service.AddDbContext<InventoryContext>(options =>
            {
                options.UseSqlServer(connectionString);

                options.ConfigureWarnings(warnings =>
                    warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
            });
        }
    }
}
