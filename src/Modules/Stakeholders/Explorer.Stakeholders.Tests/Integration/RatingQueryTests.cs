using Explorer.API.Controllers.Administrator;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration
{
    public class RatingQueryTests : BaseStakeholdersIntegrationTest
    {
        public RatingQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<RatingWithUserDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }
        private static RatingController CreateController(IServiceScope scope)
        {
            return new RatingController(scope.ServiceProvider.GetRequiredService<IRatingService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
