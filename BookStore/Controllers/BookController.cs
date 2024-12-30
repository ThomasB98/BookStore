using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Book;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET:
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await _bookService.GetAllBooksAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // GET: 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var response = await _bookService.GetBookByIdAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // POST
        [HttpPost]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data" });
            }

            var response = await _bookService.AddBookAsync(book);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // PUT
        [HttpPut]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> UpdateBook([FromBody] BookUpdateDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data" });
            }

            var response = await _bookService.UpdateBookAsync(book);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // DELETE
        [HttpDelete("{id}")]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response = await _bookService.DeleteBookAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
