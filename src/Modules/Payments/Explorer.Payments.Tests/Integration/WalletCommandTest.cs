using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
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

            var newEntity = new WalletUpdateDto(-67, 15);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-1234") }, "test");
            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var result = ((ObjectResult)controller.UpdateWallet(newEntity).Result)?.Value as WalletResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-67);
            result.AdventureCoin.ShouldBe(newEntity.AdventureCoin);

            // Assert - Database
            var storedEntity = dbContext.Wallets.FirstOrDefault(w => w.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.TouristId.ShouldBe(result.TouristId);
            storedEntity.AdventureCoin.ShouldBe(newEntity.AdventureCoin);
        }

        [Fact]
        public void Get()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-42") }, "test");
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
            result.TouristId.ShouldBe(-42);
        }

        private static WalletController CreateController(IServiceScope scope)
        {
            return new WalletController(scope.ServiceProvider.GetRequiredService<IWalletService>(),
                                        scope.ServiceProvider.GetRequiredService<ITransactionRecordService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
