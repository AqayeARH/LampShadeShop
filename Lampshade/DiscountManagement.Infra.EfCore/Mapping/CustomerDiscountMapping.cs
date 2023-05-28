using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infra.EfCore.Mapping
{
    public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            //Set Table Name In Database
            builder.ToTable("CustomerDiscounts");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DiscountRate);
            builder.Property(x => x.EndDate);
            builder.Property(x => x.StartDate);
            builder.Property(x => x.ProductId);
            builder.Property(x => x.Reason).HasMaxLength(500);
        }
    }
}
