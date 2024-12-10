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

                var result = _bookLoanDao.LoanBook(bookLoan);

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

        public async Task<ApiResponse<BookLoan>> ReturnBook(BookReturnDTO bookReturnDto)
        {
            var response = new ApiResponse<BookLoan>();

            try
            {
                // Validar fecha de préstamo
                if (bookReturnDto.ReturnDate == null)
                {
                    bookReturnDto.ReturnDate = DateTime.Now;
                }

                // obtengo el prestamo
                var bookLoan = _bookLoanDao.GetById(bookReturnDto.Id);

                if (bookLoan == null)
                {
                    response.SetError("No se encontro el prestamo", HttpStatusCode.NotFound);
                    return response;
                }

                // aca podria validar algo si el return date es mayor que el due date

                bookLoan.ReturnDate = bookReturnDto.ReturnDate;
                bookLoan.Status = "Libro Devuelto";

                var result = _bookLoanDao.ReturnBook(bookLoan);

                if (result != null)
                {
                    response.Data = result;
                    return response;
                }
                else
                {
                    response.SetError("No se pudo registrar la devolución.", HttpStatusCode.BadRequest);
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.SetError($"Error al procesar la devolución {ex.Message}", HttpStatusCode.BadRequest);
                return response;
            }
        }

        public async Task<ApiResponse<BookLoan>> ReturnBook(int id)
        {
            var response = new ApiResponse<BookLoan>();

            try
            {
                // obtengo el prestamo
                var bookLoan = _bookLoanDao.GetById(id);

                if (bookLoan == null)
                {
                    response.SetError("No se encontro el prestamo", HttpStatusCode.NotFound);
                    return response;
                }

                // aca podria validar algo si el return date es mayor que el due date

                bookLoan.ReturnDate = DateTime.Now;
                bookLoan.Status = "Libro Devuelto";

                var result = _bookLoanDao.ReturnBook(bookLoan);

                if (result != null)
                {
                    response.Data = result;
                    return response;
                }
                else
                {
                    response.SetError("No se pudo registrar la devolución.", HttpStatusCode.BadRequest);
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.SetError($"Error al procesar la devolución {ex.Message}", HttpStatusCode.BadRequest);
                return response;
            }
        }
    }
}
