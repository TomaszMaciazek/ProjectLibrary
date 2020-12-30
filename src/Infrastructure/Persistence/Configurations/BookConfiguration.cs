using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasMany(b => b.Authors).WithOne(a => a.Book).HasForeignKey(a => a.BookId);
            builder.HasOne(b => b.Category).WithMany(c => c.Books).HasForeignKey(b => b.CategoryId);
            builder.HasOne(b => b.Publisher).WithMany(p => p.Books).HasForeignKey(b => b.PublisherId);
            builder.HasMany(b => b.Borrowings).WithOne(borrowing => borrowing.Book).HasForeignKey(borrowing => borrowing.BookId);
            builder.HasMany(b => b.Reservations).WithOne(r => r.Book).HasForeignKey(r => r.BookId);
        }
    }
}
