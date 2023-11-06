using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IProblemCommentRepository
    {
        PagedResult<ProblemComment> GetPagedByProblemAnswerId(int page, int pageSize, long problemAnswerId);
    }
}
