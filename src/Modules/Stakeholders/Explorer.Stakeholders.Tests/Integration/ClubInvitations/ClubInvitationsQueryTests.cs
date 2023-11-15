using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Stakeholders.Tests.Integration.ClubInvitations;

public class ClubInvitationsQueryTests : BaseStakeholdersIntegrationTest
{
    public ClubInvitationsQueryTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_paged_by_tourist()
    {
        // Arange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var page = 1;
        var pageSize = 10;
        var touristId = -13;

        var claims = new[] { new Claim("id", touristId.ToString()) };
        var identity = new ClaimsIdentity(claims, "test");
        var user = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = user };
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = ((ObjectResult)controller.GetInvitations().Result!).Value as PagedResult<ClubInvitationWithClubAndOwnerName>;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(1);
        result.TotalCount.ShouldBe(1);
    }

    private static ClubInvitationController CreateController(IServiceScope scope)
    {
        return new ClubInvitationController(scope.ServiceProvider.GetRequiredService<IClubInvitationService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
