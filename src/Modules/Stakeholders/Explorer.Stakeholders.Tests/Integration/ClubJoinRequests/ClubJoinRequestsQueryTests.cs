using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Stakeholders.Tests.Integration.ClubJoinRequests
{
    [Collection("Sequential")]
    public class ClubJoinRequestsQueryTests : BaseStakeholdersIntegrationTest
    {
        public ClubJoinRequestsQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_paged_by_tourist()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var touristId = -11;
            var page = 1;
            var pageSize = 10;

            var claims = new[] { new Claim("id", touristId.ToString()) };
            var identity = new ClaimsIdentity(claims, "test");
            var user = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = user };
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            // Act
            var result = ((ObjectResult)controller.GetAllByTourist(page, pageSize).Result)?.Value as PagedResult<ClubJoinRequestByTouristDto>;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        [Fact]
        public void Retrieves_paged_by_club()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var clubId = -1;
            var page = 1;
            var pageSize = 10;

            // Act
            var result = ((ObjectResult)controller.GetAllByClub(clubId, page, pageSize).Result)?.Value as PagedResult<ClubJoinRequestByClubDto>;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        [Fact]
        public void Retrieves_paged_by_club_fails_invalid_club_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var clubId = -1000;
            var page = 1;
            var pageSize = 10;

            // Act
            var result = (ObjectResult)controller.GetAllByClub(clubId, page, pageSize).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static ClubJoinRequestController CreateController(IServiceScope scope)
        {
            return new ClubJoinRequestController(scope.ServiceProvider.GetRequiredService<IClubJoinRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
