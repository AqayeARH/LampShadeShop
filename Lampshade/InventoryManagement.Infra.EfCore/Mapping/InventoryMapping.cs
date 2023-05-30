using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infra.EfCore.Mapping
{
    public class InventoryMapping:IEntityTypeConfiguration<Inventory>
    {
        //Fluent APIs are written in this class

        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            //Set Table Name
            builder.ToTable("Inventories");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitPrice).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.InStock);

            //Relations
            builder.OwnsMany(x => x.InventoryOperations, modelBuilder =>
            {
                //Set Table Name
                modelBuilder.ToTable("InventoryOperations");

                //Set Primary Key
                modelBuilder.HasKey(x => x.Id);

                modelBuilder.Property(x => x.Description).IsRequired().HasMaxLength(500);

                //Relations
                modelBuilder.WithOwner(x => x.Inventory)
                    .HasForeignKey(x => x.InventoryId);
            });
        }
    }
}
