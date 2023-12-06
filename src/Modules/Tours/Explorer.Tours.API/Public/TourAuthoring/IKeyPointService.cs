using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.TourAuthoring;

public interface IKeyPointService
{
    Result<List<KeyPointResponseDto>> GetByTourId(long tourId);
    Result<KeyPointResponseDto> Create<KeyPointCreateDto>(KeyPointCreateDto keyPoint);
    Result<KeyPointResponseDto> Update<KeyPointUpdateDto>(KeyPointUpdateDto keyPoint);
    Result Delete(long id);
    Result<KeyPointResponseDto> GetFirstByTourId(long tourId);
    Result<PagedResult<KeyPointResponseDto>> GetPaged(int page, int pageSize);
    Result<List<KeyPointResponseDto>> GetByCampaignId(long campaignId);
}
