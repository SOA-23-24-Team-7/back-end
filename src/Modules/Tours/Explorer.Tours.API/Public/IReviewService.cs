using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public
{
    public interface IReviewService
    {
        Result<PagedResult<ReviewDto>> GetPaged(int page, int pageSize);
        Result<ReviewDto> Create(ReviewDto review);
        Result<ReviewDto> Update(ReviewDto review);
        Result Delete(int id);
    }
}
