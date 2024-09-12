using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Author> Authors { get; }
        public IBaseRepository<Book> Books { get; }

        Task<int> CompleteAsync();
    }
}
