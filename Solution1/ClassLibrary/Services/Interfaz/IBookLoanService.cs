using ClassLibrary.DTOs;
using ClassLibrary.models;
using ClassLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.Interfaz
{
    public interface IBookLoanService
    {
        public Task<ApiResponse<BookLoan>> LoanBook(BookLoanDTO bookLoanDto);
        
        public Task<ApiResponse<BookLoan>> ReturnBook(BookReturnDTO bookReturnDto);
        public Task<ApiResponse<BookLoan>> ReturnBook(int id);
    }
}
