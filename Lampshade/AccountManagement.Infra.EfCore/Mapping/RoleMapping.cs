using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infra.EfCore.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        //Fluent APIs are written in this class

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //Set Table Name
            builder.ToTable("Roles");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            //Relations
            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
