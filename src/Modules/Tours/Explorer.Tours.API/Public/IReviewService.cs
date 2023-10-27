using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public
{
    public interface IReviewService
    { 
        Result<PagedResult<ReviewResponseDto>> GetPagedByTourId(int page, int pageSize, long tourId);
        Result<Boolean> ReviewExists(long touristId, long tourId);
        Result<ReviewResponseDto> Create<ReviewCreateDto>(ReviewCreateDto review);
        Result<ReviewResponseDto> Update<ReviewUpdateDto>(ReviewUpdateDto review);
        Result Delete(long id);
    }
}
