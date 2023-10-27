using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourAuthoring;

public class KeyPointService : BaseService<KeyPoint>, IKeyPointService
{
    private readonly IKeyPointRepository _keyPointRepository;

    public KeyPointService(IKeyPointRepository keyPointRepository, IMapper mapper) : base(mapper)
    {
        _keyPointRepository = keyPointRepository;
    }

    public Result<List<KeyPointDto>> GetByTourId(long tourId)
    {
        try
        {
            var result = _keyPointRepository.GetByTourId(tourId);
            return MapToDto<KeyPointDto>(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<KeyPointDto> Create(KeyPointDto keyPoint)
    {
        try
        {
            var result = _keyPointRepository.Create(MapToDomain(keyPoint));
            return MapToDto<KeyPointDto>(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<KeyPointDto> Update(KeyPointDto keyPoint)
    {
        try
        {
            var result = _keyPointRepository.Update(MapToDomain(keyPoint));
            return MapToDto<KeyPointDto>(result);
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

    public Result Delete(long id)
    {
        try
        {
            _keyPointRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}
