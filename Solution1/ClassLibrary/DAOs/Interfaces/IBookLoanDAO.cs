using ClassLibrary.DTOs;
using ClassLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAOs.Interfaces
{
    public interface IBookLoanDAO
    {
        public BookLoan LoanBook(BookLoan bookLoan);

        public BookLoan ReturnBook(BookLoan bookLoan);
        public BookLoan GetById(int id);
        public IEnumerable<BookLoan> GetAll();
    }
}
