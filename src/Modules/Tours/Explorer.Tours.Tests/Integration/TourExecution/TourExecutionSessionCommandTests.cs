using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourExecution
{
    public class TourExecutionSessionCommandTest : BaseToursIntegrationTest
    {
        public TourExecutionSessionCommandTest(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            long tourId = -8;

            // Act
            var result = ((ObjectResult)controller.StartTour(tourId).Result)?.Value as TourExecutionSessionResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldBe(tourId);

            // Assert - Database
            var storedEntity = dbContext.TourExecutionSessions.FirstOrDefault(t => t.TourId == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        
        [Fact]
        public void CompletesKeyPoint()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            long tourId = -8;
            var position = new TouristPositionResponseDto
            {
                Longitude = 15.31,
                Latitude = 28.04
            };
            // Act
            controller.StartTour(tourId);
            var result = ((ObjectResult)controller.CompleteKeyPoint(tourId, position).Result)?.Value as TourExecutionSessionResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldBe(tourId);

            // Assert - Database
            var storedEntity = dbContext.TourExecutionSessions.FirstOrDefault(t => t.TourId == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.NextKeyPointId.ShouldBe(-10);
        }

        [Fact]
        public void AbandonTour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            long tourId = -8;
            // Act
            var result = ((ObjectResult)controller.AbandonTour(tourId).Result)?.Value as TourExecutionSessionResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldBe(tourId);

            // Assert - Database
            var storedEntity = dbContext.TourExecutionSessions.FirstOrDefault(t => t.TourId == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(TourExecutionSessionStatus.Abandoned);
        }
        private static TourExecutionSessionController CreateController(IServiceScope scope)
        {
            return new TourExecutionSessionController(scope.ServiceProvider.GetRequiredService<ITourExecutionSessionService>(),
                scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
