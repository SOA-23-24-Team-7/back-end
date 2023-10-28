using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemCommentService : CrudService<ProblemCommentResponseDto, ProblemComment>, IProblemCommentService
    {
        private readonly IProblemCommentRepository _problemCommentRepository;
        public ProblemCommentService(ICrudRepository<ProblemComment> repository, IProblemCommentRepository problemCommentRepository, IMapper mapper) : base(repository, mapper)
        {
            _problemCommentRepository = problemCommentRepository;
        }

        public Result<PagedResult<ProblemCommentResponseDto>> GetPagedByProblemAnswerId(int page, int pageSize, long adminId)
        {
            return MapToDto<ProblemCommentResponseDto>(_problemCommentRepository.GetPagedByProblemAnswerId(page, pageSize, adminId));
        }
    }
}
