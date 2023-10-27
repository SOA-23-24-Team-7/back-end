using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

public interface IProblemService
{
    Result<PagedResult<ProblemResponseDto>> GetPaged(int page, int pageSize);
    Result<ProblemResponseDto> Create<ProblemCreateDto>(ProblemCreateDto problem);
    Result<ProblemResponseDto> Update<ProblemUpdateDto>(ProblemUpdateDto problem);
    Result Delete(long id);
    Result<PagedResult<ProblemResponseDto>> GetByUserId(int page, int pageSize, int id);
    Result<PagedResult<ProblemResponseDto>> GetByAuthor(int page, int pageSize, long id);
}