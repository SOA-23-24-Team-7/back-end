using Explorer.API.Controllers;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
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
    public class FollowerCommandTests : BaseStakeholdersIntegrationTest
    {
        public FollowerCommandTests(StakeholdersTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new FollowerCreateDto
            {
                UserId = -11,
                FollowedById = -23,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as FollowerResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.UserId.ShouldBe(-11);
            result.FollowedById.ShouldBe(-23);

            // Assert - Database
            var storedEntity = dbContext.Followers.FirstOrDefault(f => f.FollowedById == newEntity.FollowedById && f.UserId == newEntity.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Followers.FirstOrDefault(i => i.Id == -2);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
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
