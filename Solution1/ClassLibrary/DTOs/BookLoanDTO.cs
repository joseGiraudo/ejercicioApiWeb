﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTOs
{
    public class BookLoanDTO
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? LoanDate { get; set; }
    }
}
