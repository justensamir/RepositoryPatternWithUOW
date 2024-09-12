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
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetBookByName(string name)
        {
            var book = await _unitOfWork.Books.FindAsync(b => b.Title == name, ["Author"]);
            return Ok(book);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderedBooksAsc()
        {
            var books = await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains("c"), null, null, null, o => o.Id);
            return Ok(books);
        }

        [HttpGet("[action]/{search}")]
        public async Task<IActionResult> GetOrderedBooksDesc(string search)
        {
            var books = await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains(search), null, null, null, o => o.Id, OrderBy.Descending);
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

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();

            bookDto.Id = book.Id;
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, bookDto);
        }
    }
}
