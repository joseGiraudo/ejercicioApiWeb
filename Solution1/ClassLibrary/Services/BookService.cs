using ClassLibrary.DAOs.Interfaces;
using ClassLibrary.models;
using ClassLibrary.Response;
using ClassLibrary.Services.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly IBookDAO _bookDAO;
        public BookService(IBookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }

        public async Task<ApiResponse<Book>> Create(Book book)
        {
            var response = new ApiResponse<Book>();

            var affRows = _bookDAO.Create(book);

            if (affRows > 0)
            {
                response.Data = book;
                return response;
            }
            response.SetError("No se pudo crear el usuario ", System.Net.HttpStatusCode.InternalServerError);
            return response;
        }

        public Task<ApiResponse<string>> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<Book>>> GetAll()
        {
            var response = new ApiResponse<List<Book>>();

            var books = _bookDAO.GetAll();

            if(books == null || books.Count() == 0)
            {
                response.SetError("No se encontraron libros", System.Net.HttpStatusCode.NotFound);
                return response;
            }

            response.Data = (List<Book>)books;
            return response;
        }

        public async Task<ApiResponse<Book>> GetById(int id)
        {
            var response = new ApiResponse<Book>();

            var book = _bookDAO.GetById(id);

            if (book == null)
            {
                response.SetError("No se encontró el libro con id: " + id, System.Net.HttpStatusCode.NotFound);
                return response;
            }

            response.Data = book;
            return response;
        }

        public async Task<ApiResponse<Book>> Update(int id, Book book)
        {
            var response = new ApiResponse<Book>();

            var affRows = _bookDAO.Create(book);

            if (affRows > 0)
            {
                response.Data = book;
                return response;
            }
            response.SetError("No se pudo crear el usuario ", System.Net.HttpStatusCode.InternalServerError);
            return response;
        }
    }
}
