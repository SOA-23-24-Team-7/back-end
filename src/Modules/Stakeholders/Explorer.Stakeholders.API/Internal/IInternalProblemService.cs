using FluentResults;

namespace Explorer.Stakeholders.API.Internal;

public interface IInternalProblemService
{
    Result DeleteProblemByTour(long tourId);
}