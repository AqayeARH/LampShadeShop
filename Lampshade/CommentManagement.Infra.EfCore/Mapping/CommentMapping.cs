using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infra.EfCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        //Fluent APIs are written in this class


        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //Set Table Name
            builder.ToTable("Comments");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Text).IsRequired().HasMaxLength(750);
        }
    }
}
