using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemResolvingNotificationService : CrudService<ProblemResolvingNotificationResponseDto, ProblemResolvingNotification>, IProblemResolvingNotificationService
    {
        public ProblemResolvingNotificationService(ICrudRepository<ProblemResolvingNotification> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
