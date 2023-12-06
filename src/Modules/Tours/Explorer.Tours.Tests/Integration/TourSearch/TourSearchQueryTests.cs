using Explorer.API.Controllers;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourSearch
{
    [Collection("Sequential")]
    public class TourSearchQueryTests : BaseToursIntegrationTest
    { 
        public TourSearchQueryTests(ToursTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData(15.31, 28.04, 100, 2, 2)]
        [InlineData(50, 50, 100, 0, 0)]
        public void Searches(double longitude, double latitude, double maxDistance, int expectedCount, int expectedTotalCount)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            int page = 1;
            int pageSize = 10;

            // Act
            var result = ((ObjectResult)controller.SearchByGeoLocation(longitude, latitude, maxDistance, page, pageSize).Result)?.Value as PagedResult<LimitedTourViewResponseDto>;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(expectedCount);
            result.TotalCount.ShouldBe(expectedTotalCount);
        }

        [Theory]
        [InlineData(-1000, 56.32, 100)]
        [InlineData(12.54, -1000, 100)]
        [InlineData(12.54, 56.32, -1000)]
        public void Searches_fails_invalid_arguments(double longitude, double latitude, double maxDistance)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            int page = 1;
            int pageSize = 10;

            // Act
            var result = (ObjectResult)controller.SearchByGeoLocation(longitude, latitude, maxDistance, page, pageSize).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        private static TourSearchController CreateController(IServiceScope scope)
        {
            return new TourSearchController(scope.ServiceProvider.GetRequiredService<ITourSearchService>(), null)
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
