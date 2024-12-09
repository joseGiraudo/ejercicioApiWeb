using ClassLibrary.DAOs.Interfaces;
using ClassLibrary.DTOs;
using ClassLibrary.models;
using Dapper;
using Microsoft.VisualBasic;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAOs
{
    public class BookLoanDAO : IBookLoanDAO
    {
        private string connectionString = "Server=127.0.0.1;Port=3307;Database=apiExtradosDB;User Id=root;Password=root1234;";

        public BookLoan LoanBook(BookLoan bookLoan)
        {
            string query = "insert into loan_books (bookId, userId, loanDate, dueDate, status) " +
                "values(@bookId, @userId, @loanDate, @dueDate, @status)";

            using (var connection = new MySqlConnection(connectionString))
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
            string query = "update loan_books set returnDate = @returnDate, status = @status where id = @id";

            using (var connection = new MySqlConnection(connectionString))
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
    }
}
