﻿using Application.Common;
using System;

namespace Application.Dto
{
    public class BorrowingDto : AuditDto
    {
        public BookDto Book { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string UserFirstAndLastName { get; set; }
        public string UserCardNumber { get; set; }
    }
}
