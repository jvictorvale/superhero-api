using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces;
using SuperHero.Domain.Interfaces.Repositories;
using SuperHero.Infrastructure.Data.Context;

namespace SuperHero.Infrastructure.Data.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
{
    protected readonly BaseApplicationDbContext Context;
    private readonly DbSet<T> _dbSet;
    private bool _isDisposed;

    protected Repository(BaseApplicationDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public IUnitOfWork UnitOfWork => Context;
    
    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> Any(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            Context.Dispose();
        }

        _isDisposed = true;
    }

    ~Repository()
    {
        Dispose(false);
    }
}