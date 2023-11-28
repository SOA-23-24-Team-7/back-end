using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class DisableAccountTest : BaseStakeholdersIntegrationTest
    {
        public DisableAccountTest(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Invalid_disabling_user()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            long userId = 0;

            //Act
            var result = ((ObjectResult)controller.DisableAccount(userId).Result);

            //Assert - response
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Valid_disabling_user()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            long userId = -11;

            //Act
            var result = ((ObjectResult)controller.DisableAccount(userId).Result);

            //Assert - response
            result.StatusCode.ShouldBe(200);
            var data = result?.Value as UserResponseDto;
            data.ShouldNotBeNull();
            data.IsActive.ShouldBe(false);

            //Assert - Database
            var entity = dbContext.Users.FirstOrDefault(i => i.Id == userId);
            entity.ShouldNotBeNull();
            entity.IsActive.ShouldBe(false);
        }

        private static UserController CreateController(IServiceScope scope)
        {
            return new UserController(scope.ServiceProvider.GetRequiredService<IUserService>(),
                scope.ServiceProvider.GetRequiredService<IWalletService>());
        }
    }
}
