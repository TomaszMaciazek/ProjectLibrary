using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                    new ApplicationRole
                    {
                        Id = 1,
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new ApplicationRole
                    {
                        Id = 2,
                        Name = "Librarian",
                        NormalizedName = "LIBRARIAN"
                    },
                    new ApplicationRole
                    {
                        Id = 3,
                        Name = "Reader",
                        NormalizedName = "READER"
                    }
                );
        }
    }
}
