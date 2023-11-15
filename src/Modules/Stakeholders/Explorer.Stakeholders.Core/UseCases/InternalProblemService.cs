using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class InternalProblemService : IInternalProblemService
{
    private readonly IProblemRepository _problemRepository;

    public InternalProblemService(IProblemRepository problemRepository)
    {
        _problemRepository = problemRepository;
    }

    public Result DeleteProblemByTour(long tourId)
    {
        try
        {
            _problemRepository.DeleteByTour(tourId);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}