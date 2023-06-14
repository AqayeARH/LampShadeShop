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

            builder.OwnsMany(x => x.Permissions, modelBuilder =>
            {
                //Set Primary Key
                modelBuilder.HasKey(x => new { x.Code, x.RoleId });

                modelBuilder.Property(x => x.Code).ValueGeneratedNever();
                modelBuilder.Property(x => x.RoleId).ValueGeneratedNever();

                //Set Table Name
                modelBuilder.ToTable("RolePermissions");
                modelBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);

                //Relations
                modelBuilder.WithOwner(x => x.Role)
                    .HasForeignKey(x=>x.RoleId);
            });

        }
    }
}
