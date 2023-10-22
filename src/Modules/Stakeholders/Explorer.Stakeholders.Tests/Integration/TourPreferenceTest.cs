using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.API.Controllers;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.API.Controllers.Tourist;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Explorer.Stakeholders.Tests.Integration;
[Collection("Sequential")]
public class TourPreferenceTest : BaseStakeholdersIntegrationTest
{
    public TourPreferenceTest(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Can_Create_TourPreferences()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var preference = new TourPreferenceCreateDto
        {
            UserId = -21,
            DifficultyLevel = 1,
            WalkingRating = 1,
            CyclingRating = 1,
            CarRating = 1,
            BoatRating = 1,
            SelectedTags = new List<string> { "tag1", "tag2" }
        };

        // Act
        var createPreferencesResponse = ((ObjectResult)controller.Create(preference).Result).Value as TourPreferenceResponseDto;


        // Assert
        createPreferencesResponse.ShouldNotBeNull();
        createPreferencesResponse.UserId.ShouldBe(-21);
        createPreferencesResponse.DifficultyLevel.ShouldBe(1);
        createPreferencesResponse.WalkingRating.ShouldBe(1);
        createPreferencesResponse.CyclingRating.ShouldBe(1);
        createPreferencesResponse.CarRating.ShouldBe(1);
        createPreferencesResponse.BoatRating.ShouldBe(1);
        createPreferencesResponse.SelectedTags.ShouldBe(new[] { "tag1", "tag2" });
    }

    private static TourPreferenceController CreateController(IServiceScope scope)
    {
        return new TourPreferenceController(scope.ServiceProvider.GetRequiredService<ITourPreferenceService>());
    }
}
