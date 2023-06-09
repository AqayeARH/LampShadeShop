using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infra.EfCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(15);
            builder.Property(x => x.ShortDescription).HasMaxLength(350).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(250).IsRequired();
            builder.Property(x => x.KayWords).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(350).IsRequired();

            //Relations
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);

            builder.HasMany(x => x.ProductPictures)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
