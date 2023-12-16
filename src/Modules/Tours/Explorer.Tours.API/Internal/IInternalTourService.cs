using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Internal;

public interface IInternalTourService
{
    List<long> GetAuthorsTours(long id);
    
    string GetToursName(long id);
    
    long GetAuthorsId(long id);

    Result<TourResponseDto> Get(long id);

    List<long> GetKeyPointIds(long tourId);
}
