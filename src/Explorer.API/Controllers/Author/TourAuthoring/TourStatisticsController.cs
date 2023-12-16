using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Author.TourAuthoring
{
    [Route("api/tour/statistics")]
    public class TourStatisticsController : BaseApiController
    {

        private readonly Explorer.Tours.API.Public.ITourStatisticsService _tourExecutionStatisticsService;
        private readonly Explorer.Payments.API.Public.ITourStatisticsService _tourPurchasingStatisticsService;
        private readonly Explorer.Encounters.API.Public.ITourStatisticsService _tourEncounterStatisticsService;

        public TourStatisticsController(ITourStatisticsService tourExecutionStatisticsService, Explorer.Payments.API.Public.ITourStatisticsService tourPurchasingStatisticsService, Encounters.API.Public.ITourStatisticsService tourEncounterStatisticsService)
        {
            _tourExecutionStatisticsService = tourExecutionStatisticsService;
            _tourPurchasingStatisticsService = tourPurchasingStatisticsService;
            _tourEncounterStatisticsService = tourEncounterStatisticsService;
        }

        [Authorize(Roles = "author")]
        [HttpGet("started")]
        public int GetNumberOfStartedToursByPurchase()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long authorId = -11;
            if (identity != null && identity.IsAuthenticated)
            {
                authorId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourExecutionStatisticsService.GetNumberOfStartedToursByPurchase(authorId);
            return result;
        }

        [Authorize(Roles = "author")]
        [HttpGet("completed")]
        public int GetNumberOfCompletedToursByPurchase()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long authorId = -11;
            if (identity != null && identity.IsAuthenticated)
            {
                authorId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourExecutionStatisticsService.GetNumberOfCompletedToursByPurchase(authorId);
            return result;
        }

        [Authorize(Roles = "author")]
        [HttpGet("completed/{tourId:int}")]
        public int GetNumberOfCompletedTourExecutionSessions(int tourId)
        {
            var result = _tourExecutionStatisticsService.GetNumberOfCompletedTourExecutionSessions(tourId);
            return result;
        }


        [Authorize(Roles = "author")]
        [HttpGet("started/{tourId:int}")]
        public int GetNumberOfStartedTourExecutionSessions(int tourId)
        {
            var result = _tourExecutionStatisticsService.GetNumberOfTourExecutionSessions(tourId);
            return result;
        }

        [Authorize(Roles = "author")]
        [HttpGet("bought/{tourId:int}")]
        public int GetNumberOfTimesTheTourWasSold(int tourId)
        {
            var result = _tourPurchasingStatisticsService.GetNumberOfTimesTheTourWasSold(tourId);
            return result;
        }

        [Authorize(Roles = "author")]
        [HttpGet("bought")]
        public int GetNumberOfBoughtToursForAuthor()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long authorId = -11;
            if (identity != null && identity.IsAuthenticated)
            {
                authorId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourPurchasingStatisticsService.GetNumberOfBoughtToursForAuthor(authorId);
            return result;
        }

        [Authorize(Roles = "author")]
        [HttpGet("distribution")]
        public List<long> GetMaxProgressDistribution()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long authorId = -11;
            if (identity != null && identity.IsAuthenticated)
            {
                authorId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourExecutionStatisticsService.GetMaxProgressDistribution(authorId);
            return result;
        }

        [Authorize(Roles = "author")]
        [HttpGet("keyPointVisitPercentage/{tourId:int}")]
        public List<double> GetKeyPointVisitPercentage(int tourId)
        {
            return _tourExecutionStatisticsService.GetKeyPointVisitPercentage(tourId);
        }

        [Authorize(Roles = "author")]
        [HttpGet("keyPointEncounterCompletionPercentage/{tourId:int}")]
        public Dictionary<long, double> GetKeyPointEncounterCompletionPercentage(int tourId)
        {
            return _tourEncounterStatisticsService.GetKeyPointEncounterCompletionPercentage(tourId);
        }
    }
}


