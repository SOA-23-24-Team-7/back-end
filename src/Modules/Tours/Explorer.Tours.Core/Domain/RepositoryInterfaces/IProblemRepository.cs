using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IProblemRepository
    {
        PagedResult<Problem> GetByUserId(int page, int pageSize, int id);
        PagedResult<Problem> GetByAuthor(int page, int pageSize, List<TourResponseDto> authorsTours);
    }
}
