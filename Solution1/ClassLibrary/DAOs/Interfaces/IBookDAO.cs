using ClassLibrary.DTOs;
using ClassLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAOs.Interfaces
{
    public interface IBookDAO
    {
        public IEnumerable<Book> GetAll();

        public Book GetById(int id);

        public int Create(Book book);

        public int Update(int id, Book book);

        public int DeleteById(int id);


    }
}
