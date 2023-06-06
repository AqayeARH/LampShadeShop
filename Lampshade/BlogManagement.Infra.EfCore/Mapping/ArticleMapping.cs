using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infra.EfCore.Mapping
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        //Fluent APIs are written in this class

        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //Set Table Name
            builder.ToTable("Articles");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.ShortDescription).IsRequired().HasMaxLength(350);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.KeyWords).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(500);
            builder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(350);
            builder.Property(x => x.PictureAlt).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(200).IsRequired();

            //Relations
            builder.HasOne(x => x.ArticleCategory)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
