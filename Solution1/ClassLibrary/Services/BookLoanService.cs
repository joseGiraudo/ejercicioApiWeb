using ClassLibrary.DAOs.Interfaces;
using ClassLibrary.DTOs;
using ClassLibrary.models;
using ClassLibrary.Response;
using ClassLibrary.Services.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class BookLoanService : IBookLoanService
    {
        private readonly IBookLoanDAO _bookLoanDao;

        public BookLoanService(IBookLoanDAO bookLoanDao)
        {
            _bookLoanDao = bookLoanDao;
        }


        public async Task<ApiResponse<BookLoan>> LoanBook(BookLoanDTO bookLoanDto)
        {
            var response = new ApiResponse<BookLoan>();

            try
            {
                // Validar fecha de préstamo
                if (bookLoanDto.LoanDate == null)
                {
                    bookLoanDto.LoanDate = DateTime.Now;
                }

                // Crear objeto BookLoan a partir del DTO
                var bookLoan = new BookLoan()
                {
                    Book = new Book { Id = bookLoanDto.BookId },
                    User = new User { Id = bookLoanDto.UserId },
                    LoanDate = (DateTime)bookLoanDto.LoanDate,
                    DueDate = bookLoanDto.LoanDate.Value.AddDays(5),
                    ReturnDate = null,
                    Status = "Entregado"
                };

                // Guardar en base de datos
                var result = _bookLoanDao.LoanBook(bookLoan);

                // Evaluar resultado
                if (result != null)
                {
                    response.Data = result;
                }
                else
                {
                    response.SetError("No se pudo registrar el préstamo.", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                response.SetError($"Error al procesar el préstamo: {ex.Message}", HttpStatusCode.BadRequest);
            }

            return response;

        }

        public Task<ApiResponse<BookLoan>> ReturnBook(BookLoanDTO bookLoanDto)
        {
            throw new NotImplementedException();
        }
    }
}
