using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProblemAnswerService
    {
        Result<ProblemAnswerResponseDto> Create<ProblemAnswerCreateDto>(ProblemAnswerCreateDto problemAnswer);
        Result<ProblemAnswerResponseDto> Get(long id);
        bool DoesAnswerExistsForProblem(long problemId);
    }
}
