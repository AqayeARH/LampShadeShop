using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infra.EfCore.Mapping
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        //Fluent APIs are written in this class

        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            //Set Table Name
            builder.ToTable("ArticleCategories");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(350);
            builder.Property(x => x.KeyWords).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(500);
            builder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(350);
            builder.Property(x => x.PictureAlt).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(200).IsRequired();

            //Relations
            builder.HasMany(x => x.Articles)
                .WithOne(x => x.ArticleCategory)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
