using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Infra.EfCore.Mapping
{
    public class SliderMapping : IEntityTypeConfiguration<Slider>
    {
        //Fluent APIs are written in this class

        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            //Set Table Name
            builder.ToTable("Sliders");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Heading).HasMaxLength(200);
            builder.Property(x => x.Title).HasMaxLength(200);
            builder.Property(x => x.Text).HasMaxLength(300);
            builder.Property(x => x.BtnText).HasMaxLength(80).IsRequired();
            builder.Property(x => x.IsRemoved);
        }
    }
}
