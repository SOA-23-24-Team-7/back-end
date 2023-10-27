using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProblemCommentService
    {
        Result<ProblemCommentResponseDto> Create<ProblemCommentCreateDto>(ProblemCommentCreateDto problem);
    }
}
