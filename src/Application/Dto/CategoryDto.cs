using Application.Common;
using System.Collections.Generic;

namespace Application.Dto
{
    public class CategoryDto : AuditDto
    {
        public string Name { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
    }
}
