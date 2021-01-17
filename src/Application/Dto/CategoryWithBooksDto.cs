using System.Collections.Generic;

namespace Application.Dto
{
    public class CategoryWithBooksDto : CategoryDto
    {
        public IEnumerable<BaseBookDto> Books { get; set; }
    }
}
