using System.Linq.Expressions;

namespace Application.Interfaces.Repositories;

public interface IQueryRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
}