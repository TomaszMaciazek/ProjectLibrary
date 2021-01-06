using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class AddBorrowingDto : EditableAuditDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
