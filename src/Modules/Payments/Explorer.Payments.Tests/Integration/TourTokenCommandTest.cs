using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class TourTokenCommandTest : BasePaymentsIntegrationTest
    {
        public TourTokenCommandTest(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new TourTokenCreateDto
            {
                TouristId = 4,
                TourId = -1800
            };

            // Act
            var helperResult = ((ObjectResult)controller.AddToken(newEntity.TourId, newEntity.TouristId,0,0).Result);
            var result = helperResult?.Value as TourTokenResponseDto;
            

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TouristId.ShouldBe(newEntity.TouristId);
            result.TourId.ShouldBe(newEntity.TourId);
            helperResult?.StatusCode.ShouldBe(200); //zbog recorda
            // Assert - Database
            var storedEntity = dbContext.tourTokens.FirstOrDefault(i => i.TouristId == newEntity.TouristId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.TouristId.ShouldBe(result.TouristId);
            storedEntity.TourId.ShouldBe(result.TourId);
        }


        [Theory]
        [InlineData(-11, 1)]
        [InlineData(-12, 3)]
        public void StatisticsForTour(long tourId, int bougthNumber)
        {

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result1 = (ObjectResult)controller.GetBoughtNumber(tourId).Result;
            


            // Assert - Database
            (int)result1.Value.ShouldBe(bougthNumber);
            

        }


        [Theory]
        [InlineData(-11, 0)]
        [InlineData(-12, 4)]
        public void StatisticsForAuthorsTours(long authorId, int bougthNumber)
        {

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result1 = (ObjectResult)controller.GetTotalBoughtNumber(authorId).Result;


            // Assert - Database
            (int)result1.Value.ShouldBe(bougthNumber);

        }


        private static TourTokenController CreateController(IServiceScope scope)
        {
            return new TourTokenController(scope.ServiceProvider.GetRequiredService<ITourTokenService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
