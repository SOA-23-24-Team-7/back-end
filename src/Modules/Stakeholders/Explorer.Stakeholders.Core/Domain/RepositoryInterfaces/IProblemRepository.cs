using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IProblemRepository
    {
        PagedResult<Problem> GetByAuthor(int page, int pageSize, List<TourResponseDto> authorsTours);
        PagedResult<Problem> GetAll(int page, int pageSize);
        PagedResult<Problem> GetByUserId(int page, int pageSize, long id);
        long GetTourIdByProblemId(long problemId);
    }
}
