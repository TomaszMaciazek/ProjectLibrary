using Application.Common;
using System.Collections.Generic;

namespace Application.Dto
{
    public class BaseBookDto : EditableAuditDto
    {
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int AvailabeBooksCount { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public ICollection<string> Authors { get; set; }
    }
}
