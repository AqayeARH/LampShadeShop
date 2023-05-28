using DiscountManagement.Domain.ColleaguesDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infra.EfCore.Mapping
{
    public class ColleaguesDiscountMapping : IEntityTypeConfiguration<ColleaguesDiscount>
    {
        public void Configure(EntityTypeBuilder<ColleaguesDiscount> builder)
        {
            //Set Table Name In Database
            builder.ToTable("ColleaguesDiscounts");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DiscountRate).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
