using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.ClubMemberships;

public class ClubMembershipsQueryTests : BaseStakeholdersIntegrationTest
{
    public ClubMembershipsQueryTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_club_members()
    {
        // Arange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var clubId = -2;
        var page = 1;
        var pageSize = 10;

        // Act
        var result = ((ObjectResult)controller.GetMembers(clubId).Result!).Value as PagedResult<ClubMemberDto>;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(2);
        result.TotalCount.ShouldBe(2);
    }

    [Fact]
    public void Retrieves_club_members_invalid_clubId()
    {
        // Arange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var clubId = 0;
        var page = 1;
        var pageSize = 10;

        // Act
        var result = (ObjectResult)controller.GetMembers(clubId).Result!;

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
