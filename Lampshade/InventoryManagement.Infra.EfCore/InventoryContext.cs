using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infra.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infra.EfCore
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }

        #region Db Sets

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryOperation> InventoryOperations { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
