using RepositoryPatternWithUOW.Core.Constants;
using System.Linq.Expressions;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes, int take, int skip);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderDirection = OrderBy.Ascending);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> criteria);


    }
}
