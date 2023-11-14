using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Stakeholders.Tests.Integration.ClubMemberships;

public class ClubMembershipsCommandTests : BaseStakeholdersIntegrationTest
{
    public ClubMembershipsCommandTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Kicks()
    {
        // Arange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var ownerId = -11;
        var membershipId = -4;

        var claims = new[] { new Claim("id", ownerId.ToString()) };
        var identity = new ClaimsIdentity(claims, "test");
        var user = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = user };
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = controller.KickTourist(membershipId);

        // Assert - Response
        Assert.IsType<OkResult>(result);

        // Assert - Database
        var storedMembership = dbContext.ClubMemberships.FirstOrDefault(m => m.Id == membershipId);
        storedMembership.ShouldBeNull();
    }

    [Fact]
    public void Kicks_invalid_ownerId()
    {
        // Arange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var ownerId = 0;
        var membershipId = -4;

        var claims = new[] { new Claim("id", ownerId.ToString()) };
        var identity = new ClaimsIdentity(claims, "test");
        var user = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = user };
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = (ObjectResult)controller.KickTourist(membershipId);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }


    [Fact]
    public void Kicks_invalid_membershipId()
    {
        // Arange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var ownerId = -11;
        var membershipId = 0;

        var claims = new[] { new Claim("id", ownerId.ToString()) };
        var identity = new ClaimsIdentity(claims, "test");
        var user = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = user };
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = (ObjectResult)controller.KickTourist(membershipId);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static ClubMemberManagementController CreateController(IServiceScope scope)
    {
        return new ClubMemberManagementController(scope.ServiceProvider.GetRequiredService<IClubMemberManagementService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
