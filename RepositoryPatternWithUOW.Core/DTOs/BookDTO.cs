using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternWithUOW.Core.DTOs
{
    public class BookDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Title Is Required"),
         MaxLength(250, ErrorMessage = "Title Max Length is 250")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "AuthorId Is Required"),
        Range(1, int.MaxValue, ErrorMessage = "AuthorId must be positive")]
        public int AuthorId { get; set; }
    }
}
