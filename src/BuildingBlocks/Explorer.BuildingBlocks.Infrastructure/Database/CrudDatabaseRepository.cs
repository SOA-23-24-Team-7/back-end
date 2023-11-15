using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Explorer.BuildingBlocks.Infrastructure.Database;

public class CrudDatabaseRepository<TEntity, TDbContext> : ICrudRepository<TEntity>
    where TEntity : Entity
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;
    private readonly DbSet<TEntity> _dbSet;

    public CrudDatabaseRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
        _dbSet = DbContext.Set<TEntity>();
    }

    public PagedResult<TEntity> GetPaged(int page, int pageSize)
    {
        var task = _dbSet.GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

    public TEntity Get(long id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null) throw new KeyNotFoundException("Not found: " + id);
        return entity;
    }

    public TEntity Create(TEntity entity)
    {
        _dbSet.Add(entity);
        DbContext.SaveChanges();
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        try
        {
            DbContext.Update(entity);
            DbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        return entity;
    }

    public void Delete(long id)
    {
        var entity = Get(id);
        _dbSet.Remove(entity);
        DbContext.SaveChanges();
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, string? include = null)
    {
        IQueryable<TEntity> query = _dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrEmpty(include))
        {
            foreach (var property in include.Split(","))
            {
                query = query.Include(property);
            }
        }
        var entities = query.ToList();
        return entities;
    }

    public List<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter, string? include = null)
    {
        IQueryable<TEntity> query = _dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrEmpty(include))
        {
            foreach (var property in include.Split(","))
            {
                query = query.Include(property);
            }
        }
        var entity = query.FirstOrDefault();
        if (entity == null) { throw new KeyNotFoundException("Not found."); }
        return entity;
    }
}