using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
namespace Explorer.Tours.Core.UseCases;

public class TouristPositionService : CrudService<TouristPositionResponseDto, TouristPosition>, ITouristPositionService
{
    private readonly ITouristPositionRepository _repository;

    public TouristPositionService(ICrudRepository<TouristPosition> crudRepository, ITouristPositionRepository repository, IMapper mapper) : base(crudRepository, mapper)
    {
        _repository = repository;
    }

    public virtual Result<TouristPositionResponseDto> Update<PDto>(PDto entity)
    {
        try
        {
            var result = _repository.Update(MapToDomain(entity));
            return MapToDto<TouristPositionResponseDto>(result);
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

    public Result<TouristPositionResponseDto> GetByTouristId(long touristId)
    {
        try
        {
            var result = _repository.GetByTouristId(touristId);

            return MapToDto<TouristPositionResponseDto>(result);

        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}
