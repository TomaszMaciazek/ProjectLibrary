using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    class UserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.HasData(
                new ApplicationUserRole
                {
                    RoleId = 1,
                    UserId = 1
                }
                );
        }
    }
}
