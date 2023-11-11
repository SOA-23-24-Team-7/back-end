using Explorer.BuildingBlocks.Core.Domain;
using System.Linq.Expressions;

namespace Explorer.BuildingBlocks.Core.UseCases;

public interface ICrudRepository<TEntity> where TEntity : Entity
{
    PagedResult<TEntity> GetPaged(int page, int pageSize);
    TEntity Get(long id);
    List<TEntity> GetAll();
    TEntity Get(Expression<Func<TEntity, bool>> filter);
    List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
    TEntity Create(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(long id);
}