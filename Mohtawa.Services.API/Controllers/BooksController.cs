using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mohtawa.Services.Application.Interfaces;
using Mohtawa.Services.Application.Models.Requests.Book;

namespace Mohtawa.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        /// <summary>
        /// Retrieve all books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await _bookService.GetAllBooks();
            return StatusCode((int)response.HttpStatusCode, response);
        }

        /// <summary>
        /// Retrieve a book by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _bookService.GetBookById(id);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        /// <summary>
        /// Add a new book.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest book)
        {
            var response = await _bookService.AddBook(book);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        /// <summary>
        /// Update an existing book.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookRequest book)
        {
            var response = await _bookService.UpdateBook(id, book);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        /// <summary>
        /// Delete a book by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _bookService.DeleteBook(id);
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
