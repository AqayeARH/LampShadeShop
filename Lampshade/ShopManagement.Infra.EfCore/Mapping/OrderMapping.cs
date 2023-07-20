using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infra.EfCore.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Set Table Name
            builder.ToTable("Orders");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IssueTrackingNo).HasMaxLength(10);

            //Relations
            builder.OwnsMany(x => x.OrderItems, navigationBuilder =>
            {
                //Set Table Name
                navigationBuilder.ToTable("OrderItems");
                //Set Primary Key
                navigationBuilder.HasKey(x => x.Id);

                //Relations
                navigationBuilder.WithOwner(x => x.Order)
                    .HasForeignKey(x => x.OrderId);
            });
        }
    }
}
