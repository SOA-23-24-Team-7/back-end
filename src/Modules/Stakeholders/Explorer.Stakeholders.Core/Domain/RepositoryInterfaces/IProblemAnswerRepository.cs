namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IProblemAnswerRepository
    {
        bool DoesAnswerExistsForProblem(long problemId);
    }
}
