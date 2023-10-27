using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ProblemService : CrudService<ProblemResponseDto, Problem>, IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly ITourService _tourService;
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper, IProblemRepository problemRepository, ITourService tourService) : base(repository, mapper)
        {
            _problemRepository = problemRepository;
            _tourService = tourService;
        }
        public Result<PagedResult<ProblemResponseDto>> GetByUserId(int page, int pageSize, int id)
        {
            return MapToDto<ProblemResponseDto>(_problemRepository.GetByUserId(page, pageSize, id));
        }

        public Result<PagedResult<ProblemResponseDto>> GetByAuthor(int page, int pageSize, long id)
        {
            return MapToDto<ProblemResponseDto>(_problemRepository.GetByAuthor(page, pageSize, _tourService.GetAuthorsPagedTours(id, page, pageSize).Value.Results));
        }
    }
}

