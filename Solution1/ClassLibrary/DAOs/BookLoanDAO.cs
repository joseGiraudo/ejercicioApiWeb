using ClassLibrary.DAOs.Interfaces;
using ClassLibrary.DTOs;
using ClassLibrary.models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClassLibrary.DAOs
{
    public class BookLoanDAO : IBookLoanDAO
    {
        private readonly string _connectionString;

        public BookLoanDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public BookLoan LoanBook(BookLoan bookLoan)
        {
            string query = "insert into book_loans (bookId, userId, loanDate, dueDate, status) " +
                "values(@bookId, @userId, @loanDate, @dueDate, @status)";

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                int affRows = connection.Execute(query, new
                {
                    bookId = bookLoan.Book.Id,
                    userId = bookLoan.User.Id,
                    loanDate = bookLoan.LoanDate,
                    dueDate = bookLoan.DueDate,
                    status = bookLoan.Status,
                });
                if (affRows > 0)
                {
                    return bookLoan;
                }
                return null;
            }
        }

        public BookLoan ReturnBook(BookLoan bookLoan)
        {
            string query = "update book_loans set returnDate = @returnDate, status = @status where id = @id";

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                int affRows = connection.Execute(query, new
                {
                    returnDate = bookLoan.ReturnDate,
                    status = bookLoan.Status,
                    id = bookLoan.Id
                });
                if (affRows > 0)
                {
                    return bookLoan;
                }
                return null;
            }
        }

        public BookLoan GetById(int id)
        {
            string query = "select * from book_loans where id = @id";

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var bookLoan = connection.QueryFirstOrDefault<BookLoan>(query, new { id });

                return bookLoan;
            }
        }

        public IEnumerable<BookLoan> GetAll()
        {
            string query = "select * from book_loans";

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var bookLoans = connection.Query<BookLoan>(query);

                return bookLoans;
            }
        }
    }
}
