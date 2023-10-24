using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var minDateTime = DateTime.Now;
        var newEntity = new ClubInvitationDto()
        {
            ClubId = -1,
            TouristId = -11
        };

        // Act
        //var result = ((ObjectResult)controller.GetInvitations)
    }

    private static ClubInvitationController CreateController(IServiceScope scope)
    {
        return new ClubInvitationController(scope.ServiceProvider.GetRequiredService<IClubInvitationService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
