using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemAnswerService : CrudService<ProblemAnswerResponseDto, ProblemAnswer>, IProblemAnswerService
    {
        private readonly IProblemAnswerRepository _problemAnswerRepository;
        public ProblemAnswerService(ICrudRepository<ProblemAnswer> repository, IProblemAnswerRepository problemAnswerRepository, IMapper mapper) : base(repository, mapper)
        {
            _problemAnswerRepository = problemAnswerRepository;
        }

        public bool DoesAnswerExistsForProblem(long problemId)
        {
            return _problemAnswerRepository.DoesAnswerExistsForProblem(problemId);
        }

        public Result<ProblemAnswerResponseDto> GetByProblem(long problemId)
        {
            return MapToDto<ProblemAnswerResponseDto>(_problemAnswerRepository.GetByProblem(problemId));
        }
    }
}
