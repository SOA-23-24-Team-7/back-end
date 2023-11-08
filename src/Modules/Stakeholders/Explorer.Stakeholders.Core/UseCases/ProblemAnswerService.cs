using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemAnswerService : CrudService<ProblemAnswerResponseDto, ProblemAnswer>, IProblemAnswerService
    {
        public ProblemAnswerService(ICrudRepository<ProblemAnswer> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
