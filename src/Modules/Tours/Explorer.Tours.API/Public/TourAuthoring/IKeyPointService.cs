using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.TourAuthoring;

public interface IKeyPointService
{
    Result<List<KeyPointDto>> GetByTourId(long tourId);
    Result<KeyPointDto> Create(KeyPointDto keyPoint);
    Result<KeyPointDto> Update(KeyPointDto keyPoint);
    Result Delete(long id);
}
