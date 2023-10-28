using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IProblemRepository
    {
        PagedResult<Problem> GetByUserId(int page, int pageSize, long id);
        long GetTourIdByProblemId(long problemId);
    }
}
