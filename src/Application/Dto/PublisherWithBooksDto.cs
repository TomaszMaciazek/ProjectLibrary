using System.Collections.Generic;

namespace Application.Dto
{
    public class PublisherWithBooksDto : PublisherDto
    {
        public IEnumerable<BookDto> Books { get; set; }
    }
}
