using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.BookStatus).HasConversion<int>();
            builder.HasOne(b => b.Category).WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Publisher).WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.Authors).WithOne(a => a.Book)
                .HasForeignKey(a => a.BookId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(b => b.Borrowings).WithOne(borrowing => borrowing.Book)
                .HasForeignKey(borrowing => borrowing.BookId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(b => b.Reservations).WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
