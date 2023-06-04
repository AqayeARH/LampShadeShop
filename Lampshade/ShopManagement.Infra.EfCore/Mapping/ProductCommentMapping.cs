using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCommentAgg;

namespace ShopManagement.Infra.EfCore.Mapping
{
    public class ProductCommentMapping:IEntityTypeConfiguration<ProductComment>
    {
        //Fluent APIs are written in this class


        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            //Set Table Name
            builder.ToTable("ProductComments");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Text).IsRequired().HasMaxLength(750);

            //Relations
            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductComments)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
