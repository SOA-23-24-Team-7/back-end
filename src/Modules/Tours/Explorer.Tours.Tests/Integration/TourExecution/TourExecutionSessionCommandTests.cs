using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

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

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-12") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };


            var dto = new TourExecutionDto
            {
                TourId = tourId,
                IsCampaign = false
            };

            // Act
            var result = ((ObjectResult)controller.StartTour(dto).Result)?.Value as TourExecutionSessionResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldBe(tourId);

            // Assert - Database
            var storedEntity = dbContext.TourExecutionSessions.FirstOrDefault(t => t.TourId == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        /*
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

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-12") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
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
            storedEntity.NextKeyPointId.ShouldBe(-11);
        }
        */
        [Fact]
        public void AbandonTour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            long tourId = -8;
            var dto = new TourExecutionDto
            {
                TourId = tourId,
                IsCampaign = false
            };

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-12") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            // Act

            var result = ((ObjectResult)controller.AbandonTour(dto).Result)?.Value as TourExecutionSessionResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldBe(dto.TourId);

            // Assert - Database
            var storedEntity = dbContext.TourExecutionSessions.FirstOrDefault(t => t.TourId == dto.TourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(TourExecutionSessionStatus.Abandoned.ToString());
        }

        private static TourExecutionSessionController CreateController(IServiceScope scope)
        {
            return new TourExecutionSessionController(scope.ServiceProvider.GetRequiredService<ITourExecutionSessionService>(),
                scope.ServiceProvider.GetRequiredService<ITourService>(),
                scope.ServiceProvider.GetRequiredService<ITourTokenService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
