using ClassLibrary.DAOs.Interfaces;
using ClassLibrary.models;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAOs
{
    public class BookDAO : IBookDAO
    {
        private string connectionString = "Server=127.0.0.1;Port=3307;Database=apiExtradosDB;User Id=root;Password=root1234;";

        // constructor privado
        public BookDAO() {}


        public int Create(Book book)
        {
            string query = "insert into books (title, author) values (@title, @author)";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(query, new
                {
                    title = book.Title,
                    author = book.Author,
                });
                return affectedRows;
            }
        }

        public int DeleteById(int id)
        {
            string query = "update books set active = @active where id = @id";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Execute(query, new { id = id, active = false });
            }
        }

        public IEnumerable<Book> GetAll()
        {
            string query = "select id, title, author from books where active = true";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var books = connection.Query<Book>(query);

                if (books == null || !books.Any())
                {
                    throw new Exception("No se encontraron libros");
                }
                return books;
            }
        }

        public Book GetById(int id)
        {
            string query = "select title, author from books where id = @id and active = true";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var book = connection.QueryFirst<Book>(query, new { id = id});

                if (book == null)
                {
                    throw new Exception("No se encontró el libro con id " + id);
                }
                return book;
            }
        }

        public int Update(int id, Book book)
        {
            string query = "update books set title = @title, author = @author where id = @id";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(query, new
                {
                    title = book.Title,
                    author = book.Author,
                    id = id,
                });
                return affectedRows;
            }
        }
    }
}
