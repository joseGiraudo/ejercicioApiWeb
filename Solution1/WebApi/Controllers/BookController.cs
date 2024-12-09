using ClassLibrary.DAOs;
using ClassLibrary.DAOs.Interfaces;
using ClassLibrary.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {
        private readonly IBookDAO _bookDAO;

        public BookController(IBookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _bookDAO.GetAll();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var book = _bookDAO.GetById(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            try
            {
                _bookDAO.Create(book);
                return Ok("Libro creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            try
            {
                _bookDAO.Update(id, book);
                return Ok("Libro actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _bookDAO.DeleteById(id);
                return Ok("Libro Eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
