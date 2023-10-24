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
        var newEntity = new ClubInvitationDto()
        {
            ClubId = -1,
            TouristId = -11
        };

        //var claims = new[] { new Claim("id", touristId.ToString()) };
        //var identity = new ClaimsIdentity(claims, "test");
        //var user = new ClaimsPrincipal(identity);
        //var context = new DefaultHttpContext { User = user };
        //controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var result = ((ObjectResult)controller.Invite(newEntity).Result)?.Value as ClubInvitationCreatedDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.TouristId.ShouldBe(newEntity.TouristId);
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

    private static ClubInvitationController CreateController(IServiceScope scope)
    {
        return new ClubInvitationController(scope.ServiceProvider.GetRequiredService<IClubInvitationService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
