using ClassLibrary.models;
using ClassLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.Interfaz
{
    public interface IBookService
    {
        public Task<ApiResponse<List<Book>>> GetAll();

        public Task<ApiResponse<Book>> GetById(int id);

        public Task<ApiResponse<Book>> Create(Book book);

        public Task<ApiResponse<Book>> Update(int id, Book book);

        public Task<ApiResponse<string>> DeleteById(int id);
    }
}
