using Application.Interfaces.Repositories;
using Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Command;

public class CommandRepository<T> : ICommandRepository<T> where T : class
{
    protected readonly OrderManagementDbContext _context;

    public CommandRepository(OrderManagementDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}