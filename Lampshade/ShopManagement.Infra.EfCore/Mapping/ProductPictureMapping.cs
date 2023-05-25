using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infra.EfCore.Mapping
{
    public class ProductPictureMapping:IEntityTypeConfiguration<ProductPicture>
    {
        //Fluent APIs are written in this class

        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            //Set Table Name
            builder.ToTable("ProductPictures");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ProductId);
            builder.Property(x => x.IsRemoved);
            builder.Property(x => x.PictureAlt).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(200).IsRequired();

            //Relations
            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductPictures)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
