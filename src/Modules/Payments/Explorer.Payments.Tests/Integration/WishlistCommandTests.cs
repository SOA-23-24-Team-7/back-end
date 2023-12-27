using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class WishlistCommandTests : BasePaymentsIntegrationTest
    {
        public WishlistCommandTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new WishlistCreateDto
            {
                TouristId = -21,
                TourId = -1800
            };

            // Act
            var helperResult = ((ObjectResult)controller.AddTourToWishlist(newEntity.TourId).Result);
            var result = helperResult?.Value as WishlistResponseDto;

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

        private static WishlistController CreateController(IServiceScope scope)
        {
            return new WishlistController(scope.ServiceProvider.GetRequiredService<IWishlistService>(), scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
