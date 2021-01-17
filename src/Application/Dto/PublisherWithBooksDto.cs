using System.Collections.Generic;

namespace Application.Dto
{
    public class PublisherWithBooksDto : PublisherDto
    {
        public IEnumerable<BaseBookDto> Books { get; set; }
    }
}
