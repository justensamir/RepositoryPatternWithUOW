using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Interfaces;

namespace RepositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            return Ok(author);
        }

    }
}
