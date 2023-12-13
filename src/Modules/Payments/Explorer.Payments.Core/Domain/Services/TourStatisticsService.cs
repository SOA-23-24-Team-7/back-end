using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Tours.API.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Services;

public class TourStatisticsService : ITourStatisticsService
{
    private ITourTokenService _tourTokenService;
    private IInternalTourService _tourService;
    public TourStatisticsService(ITourTokenService tourTokenService, IInternalTourService tourService)
    {
        _tourTokenService = tourTokenService;
        _tourService = tourService;
    }

    public int GetNumberOfBoughtToursForAuthor(long authorId)
    {
        IEnumerable<long> tourIds = _tourService.GetAuthorsTours(authorId);
        var tokens = _tourTokenService.GetAll();
        int counter = 0;

        foreach (var token in tokens.Value)
        {
            if (tourIds.Contains(token.TourId))
            {
                counter++;
            }
        }

        return counter;
    }


    public int GetNumberOfTimesTheTourWasSold(long tourId)
    {
        var tokens = _tourTokenService.GetAll();
        int counter = 0;

        foreach (var token in tokens.Value)
        {
            if (tourId == token.TourId)
            {
                counter++;
            }
        }

        return counter;
    }


}
