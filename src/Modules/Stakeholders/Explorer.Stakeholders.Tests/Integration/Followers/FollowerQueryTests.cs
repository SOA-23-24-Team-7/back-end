using Explorer.API.Controllers;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Followers
{
    public class FollowerQueryTests : BaseStakeholdersIntegrationTest
    {
        public FollowerQueryTests(StakeholdersTestFactory factory) : base(factory) {}

        [Fact]
        public void Retrieves_Followers()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetFollowers(0, 0, -21).Result)?.Value as PagedResult<FollowerResponseWithUserDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(1);
            result.TotalCount.ShouldBe(1);
        }

        [Fact]
        public void Retrieves_Followings()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetFollowings(0, 0, -21).Result)?.Value as PagedResult<FollowingResponseWithUserDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(0);
            result.TotalCount.ShouldBe(1);
        }

        private static FollowerController CreateController(IServiceScope scope)
        {
            return new FollowerController(scope.ServiceProvider.GetRequiredService<IFollowerService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
