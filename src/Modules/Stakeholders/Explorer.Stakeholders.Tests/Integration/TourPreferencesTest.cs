using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.API.Controllers;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.API.Controllers.Tourist;
using Newtonsoft.Json.Linq;

namespace Explorer.Stakeholders.Tests.Integration;
[Collection("Sequential")]
public class TourPreferencesTest : BaseStakeholdersIntegrationTest
{
    public TourPreferencesTest(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Can_Create_TourPreferences()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var controller = CreateController(scope);
        var preference = new TourPreferencesDto
        {
            UserId = 3,
            DifficultyLevel = 1,
            WalkingRating = 1,
            CyclingRating = 1,
            CarRating = 1,
            BoatRating = 1,
            SelectedTags = new List<string> { "tag1", "tag2" }
        };

        // Act
        var createPreferencesResponse = ((ObjectResult)controller.CreateTourPreference(preference).Result).Value as TourPreferencesDto;


        // Assert
        createPreferencesResponse.ShouldNotBeNull();
        createPreferencesResponse.UserId.ShouldBe(3);
        createPreferencesResponse.DifficultyLevel.ShouldBe(1);
        createPreferencesResponse.WalkingRating.ShouldBe(1);
        createPreferencesResponse.CyclingRating.ShouldBe(1);
        createPreferencesResponse.CarRating.ShouldBe(1);
        createPreferencesResponse.BoatRating.ShouldBe(1);
        createPreferencesResponse.SelectedTags.ShouldBe(new[] { "tag1", "tag2" });
    }

    private static TourPreferencesController CreateController(IServiceScope scope)
    {
        return new TourPreferencesController(scope.ServiceProvider.GetRequiredService<ITourPreferencesService>());
    }
}
