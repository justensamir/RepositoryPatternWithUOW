using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF.Contexts;
using RepositoryPatternWithUOW.EF.Repositories;

namespace RepositoryPatternWithUOW.EF.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Author> Authors { get; private set; }

        public IBaseRepository<Book> Books { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Authors = new BaseRepository<Author>(_context);
            Books = new BaseRepository<Book>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
