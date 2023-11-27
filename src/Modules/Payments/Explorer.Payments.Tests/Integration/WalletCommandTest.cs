using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Payments.Tests.Integration
{
    public class WalletCommandTest : BasePaymentsIntegrationTest
    {
        public WalletCommandTest(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Update()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var newEntity = new WalletUpdateDto
            {
                Id = -1,
                AdventureCoin = 1,
            };

            var result = ((ObjectResult)controller.UpdateWallet(newEntity).Result)?.Value as WalletResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(-1);
            result.AdventureCoin.ShouldBe(newEntity.AdventureCoin);

            // Assert - Database
            //TODO
        }

        [Fact]
        public void Get()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "1") }, "test");
            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var result = ((ObjectResult)controller.GetTouristsWallet().Result)?.Value as WalletResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();

            // Assert - Database
            //TODO
        }

        private static WalletController CreateController(IServiceScope scope)
        {
            return new WalletController()
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
