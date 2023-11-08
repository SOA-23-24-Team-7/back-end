using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemService : CrudService<ProblemResponseDto, Problem>, IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IInternalTourService _tourService;
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper, IProblemRepository problemRepository, IInternalTourService tourService) : base(repository, mapper)
        {
            _problemRepository = problemRepository;
            _tourService = tourService;
        }

        public Result<ProblemResponseDto> ResolveProblem(long problemId)
        {
            try
            {
                Problem problem = CrudRepository.Get(problemId);
                problem.IsResolved = true;

                var result = CrudRepository.Update(problem);
                return MapToDto<ProblemResponseDto>(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<PagedResult<ProblemResponseDto>> GetAll(int page, int pageSize)
        {
            var results = MapToDto<ProblemResponseDto>(_problemRepository.GetAll(page, pageSize));
            foreach (var problemDto in results.Value.Results)
            {
                problemDto.TourName = _tourService.GetToursName(problemDto.TourId);
            }

            return results;
        }

        public Result<PagedResult<ProblemResponseDto>> GetByUserId(int page, int pageSize, long id)
        {
            var results = MapToDto<ProblemResponseDto>(_problemRepository.GetByUserId(page, pageSize, id));
            foreach (var problemDto in results.Value.Results)
            {
                problemDto.TourName = _tourService.GetToursName(problemDto.TourId);
            }

            return results;
        }

        public Result<PagedResult<ProblemResponseDto>> GetByAuthor(int page, int pageSize, long id)
        {
            var results = MapToDto<ProblemResponseDto>(_problemRepository.GetByAuthor(page, pageSize, _tourService.GetAuthorsTours(id).ToList()));
            foreach (var problemDto in results.Value.Results)
            {
                problemDto.TourName = _tourService.GetToursName(problemDto.TourId);
            }

            return results;
        }

        public long GetTourIdByProblemId(long problemId)
        {
            return _problemRepository.GetTourIdByProblemId(problemId);
        }
    }
}

