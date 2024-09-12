using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Constants;
using RepositoryPatternWithUOW.Core.DTOs;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _bookRepository;
        public BooksController(IBaseRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetBookByName(string name)
        {
            var book = await _bookRepository.FindAsync(b => b.Title == name, ["Author"]);
            return Ok(book);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderedBooksAsc()
        {
            var books = await _bookRepository.FindAllAsync(b => b.Title.Contains("c"), null, null, null, o => o.Id);
            return Ok(books);
        }

        [HttpGet("[action]/{search}")]
        public async Task<IActionResult> GetOrderedBooksDesc(string search)
        {
            var books = await _bookRepository.FindAllAsync(b => b.Title.Contains(search), null, null, null, o => o.Id, OrderBy.Descending);
            return Ok(books);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddBook(BookDTO bookDto)
        {
            var book = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId
            };
            await _bookRepository.AddAsync(book);
            bookDto.Id = book.Id;
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, bookDto);
        }
    }
}
