using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Stakeholders.Tests.Integration.ClubInvitations;

public class ClubInvitationsCommandTests : BaseStakeholdersIntegrationTest
{
    public ClubInvitationsCommandTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Sends()
    {
        // Arange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var newEntity = new ClubInvitationWithUsernameDto()
        {
            ClubId = -2,
            Username = "autor2"
        };

        // Act
        var result = ((ObjectResult)controller.Invite(newEntity).Result!).Value as ClubInvitationCreatedDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.ClubId.ShouldBe(newEntity.ClubId);
        result.Status.ShouldBe("Waiting");

        // Assert - Database
        var storedEntity = dbContext.ClubInvitations.FirstOrDefault(i => i.Id == result.Id);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
        storedEntity.TouristId.ShouldBe(result.TouristId);
        storedEntity.ClubId.ShouldBe(result.ClubId);
        storedEntity.TimeCreated.ShouldBe(result.TimeCreated);
        storedEntity.Status.ToString().ShouldBe(result.Status);
    }

    [Fact]
    public void Sends_fails_invalid_tourist_username()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var newEntity = new ClubInvitationWithUsernameDto()
        {
            ClubId = -1,
            Username = "asdf"
        };

        // Act
        var result = (ObjectResult)controller.Invite(newEntity).Result!;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Sends_fails_invalid_club_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var newEntity = new ClubInvitationWithUsernameDto()
        {
            ClubId = 0,
            Username = "admin@gmail.com"
        };

        // Act
        var result = (ObjectResult)controller.Invite(newEntity).Result!;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Accepts()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var touristId = -22;
        var invitationId = -4;

        var claims = new[] { new Claim("id", touristId.ToString()) };
        var identity = new ClaimsIdentity(claims, "test");
        var user = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = user };
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = controller.Accept(invitationId);

        // Assert - Response
        Assert.IsType<OkResult>(result);

        // Assert - Database
        var storedInvitation = dbContext.ClubInvitations.FirstOrDefault(i => i.Id == invitationId);
        storedInvitation.ShouldNotBeNull();
        storedInvitation.Status.ShouldBe(Core.Domain.InvitationStatus.Accepted);

        var storedMembership = dbContext.ClubMemberships.FirstOrDefault(m => m.TouristId == touristId);
        storedMembership.ShouldNotBeNull();
        storedMembership.TouristId.ShouldBe(touristId);
    }


    [Fact]
    public void Accepts_invalid_invitation_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var touristId = -22;
        var invitationId = 0;

        var claims = new[] { new Claim("id", touristId.ToString()) };
        var identity = new ClaimsIdentity(claims, "test");
        var user = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = user };
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = (ObjectResult)controller.Accept(invitationId);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }


    [Fact]
    public void Rejects()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var touristId = -13;
        var invitationId = -5;

        var claims = new[] { new Claim("id", touristId.ToString()) };
        var identity = new ClaimsIdentity(claims, "test");
        var user = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = user };
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = controller.Reject(invitationId);

        // Assert - Response
        Assert.IsType<OkResult>(result);

        // Assert - Database
        var storedInvitation = dbContext.ClubInvitations.FirstOrDefault(i => i.Id == invitationId);
        storedInvitation.ShouldNotBeNull();
        storedInvitation.Status.ShouldBe(Core.Domain.InvitationStatus.Declined);
    }

    [Fact]
    public void Rejects_invalid_invitation_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var touristId = -13;
        var invitationId = 0;

        var claims = new[] { new Claim("id", touristId.ToString()) };
        var identity = new ClaimsIdentity(claims, "test");
        var user = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = user };
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = (ObjectResult)controller.Accept(invitationId);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static ClubInvitationController CreateController(IServiceScope scope)
    {
        return new ClubInvitationController(scope.ServiceProvider.GetRequiredService<IClubInvitationService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
