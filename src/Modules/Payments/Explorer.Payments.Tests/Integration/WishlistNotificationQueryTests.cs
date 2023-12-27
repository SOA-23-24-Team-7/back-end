using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class WishlistNotificationQueryTests : BasePaymentsIntegrationTest
    {
        public WishlistNotificationQueryTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all_by_tourist()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var userContext = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(userContext)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var result = ((ObjectResult)controller.GetByTouristId().Result)?.Value as List<WishlistNotificationResponseDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        private static WIshlistNotificationController CreateController(IServiceScope scope)
        {
            return new WIshlistNotificationController(scope.ServiceProvider.GetRequiredService<IWishlistNotificationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
