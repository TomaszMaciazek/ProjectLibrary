using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProlongRequestConfiguration : IEntityTypeConfiguration<ProlongRequest>
    {
        public void Configure(EntityTypeBuilder<ProlongRequest> builder)
        {
            builder.HasOne(r => r.Borrowing).WithMany(b => b.ProlongRequests).HasForeignKey(r => r.BorrowingId);
            builder.HasOne(r => r.User).WithMany(u => u.ProlongRequests).HasForeignKey(r => r.UserId);
        }
    }
}
