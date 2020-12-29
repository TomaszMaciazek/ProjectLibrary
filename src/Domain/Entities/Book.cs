using Domain.Common;

namespace Domain.Entities
{
    public class Book : AuditEntity
    {
        public int Id { get; set; }
        public int ReleaseYear { get; set; }
        public string Title { get; set; }
        public Publisher Publisher { get; set; }
        public Author Author { get; set; }
    }
}
