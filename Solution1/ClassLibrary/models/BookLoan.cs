using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.models
{
    public class BookLoan
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public DateTimeOffset LoanDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset? ReturnDate { get; set; }
        public string Status { get; set; }

        public BookLoan() { }

    }
}
