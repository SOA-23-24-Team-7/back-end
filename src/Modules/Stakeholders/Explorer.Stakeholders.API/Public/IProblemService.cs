using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IProblemService
{
    Result<PagedResult<ProblemResponseDto>> GetAll(int page, int pageSize);
    Result<ProblemResponseDto> Get(long id);
    Result<ProblemResponseDto> Create<ProblemCreateDto>(ProblemCreateDto problem);
    Result<ProblemResponseDto> Update<ProblemUpdateDto>(ProblemUpdateDto problem);
    Result ResolveProblem(long problemId);
    Result Delete(long id);
    Result<PagedResult<ProblemResponseDto>> GetByAuthor(int page, int pageSize, long id);
    Result<PagedResult<ProblemResponseDto>> GetByUserId(int page, int pageSize, long id);
    Result<ProblemResponseDto> UpdateDeadline(long problemId, DateTime deadline, long adminID);
    Result CreateAnswer(ProblemAnswerDto problemAnswer, long problemId);
    Result<ProblemCommentResponseDto> CreateComment(ProblemCommentCreateDto problemComment, long problemId);
    Result<ProblemAnswerDto> GetAnswer(long problemId);
    Result<PagedResult<ProblemCommentResponseDto>> GetComments(long problemId);
}