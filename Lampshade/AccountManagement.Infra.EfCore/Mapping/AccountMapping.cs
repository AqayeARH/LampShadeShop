using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infra.EfCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        //Fluent APIs are written in this class

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            //Set Table Name
            builder.ToTable("Accounts");

            //Set Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Mobile).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Profile).HasMaxLength(1000);

            //Relations
            builder.HasOne(x => x.Role)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
