using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.ReservationStatus).HasConversion<int>();
            builder.HasOne(r => r.User).WithMany(u => u.Reservations).HasForeignKey(b => b.UserId);
        }
    }
}
