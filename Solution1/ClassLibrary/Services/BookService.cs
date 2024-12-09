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

        public Task<ApiResponse<List<Book>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Book>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Book>> Update(int id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
