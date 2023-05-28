using DiscountManagement.Domain.ColleaguesDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infra.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infra.EfCore
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {

        }

        #region Db Sets

        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleaguesDiscount> ColleaguesDiscounts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscountMapping).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
