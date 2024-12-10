using ClassLibrary.DTOs;
using ClassLibrary.models;
using ClassLibrary.Services;
using ClassLibrary.Services.Interfaz;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("loans")]
    public class BookLoanController : ControllerBase
    {
        
        private readonly IBookLoanService _bookLoanService;

        public BookLoanController(IBookLoanService bookLoanService)
        {
            _bookLoanService = bookLoanService;
        }

        [HttpPost]
        public async Task<IActionResult> LoanBook([FromBody] BookLoanDTO bookLoanDto)
        {
            var response = await _bookLoanService.LoanBook(bookLoanDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _bookLoanService.ReturnBook(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
