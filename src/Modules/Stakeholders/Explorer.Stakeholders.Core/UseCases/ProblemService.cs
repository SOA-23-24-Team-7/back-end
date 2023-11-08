using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemService : CrudService<ProblemResponseDto, Problem>, IProblemService
{
        private readonly IProblemRepository _problemRepository;
    public ProblemService(ICrudRepository<Problem> repository, IMapper mapper) : base(repository, mapper) { }
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper,IProblemRepository problemRepository) : base(repository, mapper) {
            _problemRepository = problemRepository;
        }
        public Result<PagedResult<ProblemResponseDto>> GetByUserId(int page, int pageSize, long id)
        {
            return MapToDto<ProblemResponseDto>(_problemRepository.GetByUserId(page, pageSize, id));
        }
    }
}

