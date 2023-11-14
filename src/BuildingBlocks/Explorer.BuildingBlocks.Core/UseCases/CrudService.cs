using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using FluentResults;

namespace Explorer.BuildingBlocks.Core.UseCases;

/// <summary>
/// A base service class that offers CRUD methods for persisting TDomain objects, based on the passed TDto object.
/// </summary>
/// <typeparam name="TDto">Type of output data transfer object.</typeparam>
/// <typeparam name="TDomain">Type of domain object that maps to TDto</typeparam>
public abstract class CrudService<TDto, TDomain> : BaseService<TDomain> where TDomain : Entity
{
    protected readonly ICrudRepository<TDomain> CrudRepository;
    private IMapper mapper;

    protected CrudService(ICrudRepository<TDomain> crudRepository, IMapper mapper) : base(mapper)
    {
        CrudRepository = crudRepository;
    }

    public Result<PagedResult<TDto>> GetPaged(int page, int pageSize)
    {
        var result = CrudRepository.GetPaged(page, pageSize);
        return MapToDto<TDto>(result);
    }

    public Result<TDto> Get(long id)
    {
        try
        {
            var result = CrudRepository.Get(id);
            return MapToDto<TDto>(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public virtual Result<TDto> Create<PDto>(PDto entity)
    {
        try
        {
            var result = CrudRepository.Create(MapToDomain<PDto>(entity));
            return MapToDto<TDto>(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public virtual Result<TDto> Update<PDto>(PDto entity)
    {
        try
        {
            var result = CrudRepository.Update(MapToDomain<PDto>(entity));
            return MapToDto<TDto>(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public virtual Result Delete(long id)
    {
        try
        {
            CrudRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}