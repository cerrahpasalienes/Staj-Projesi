using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Query;

public class QueryRepository<T> : IQueryRepository<T> where T : class
{
    protected readonly OrderManagementDbContext _context;

    public QueryRepository(OrderManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached; 
        }
        return entity;
    }

    public async Task<IReadOnlyList<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
    }
}