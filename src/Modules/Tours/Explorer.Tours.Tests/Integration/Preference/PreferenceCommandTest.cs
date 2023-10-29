using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Explorer.API.Controllers.Tourist;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Explorer.Tours.Tests.Integration.Preference;
[Collection("Sequential")]
public class PreferenceCommandTest : BaseToursIntegrationTest
{
    public PreferenceCommandTest(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
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

        var preference = new PreferenceCreateDto
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
        var createPreferencesResponse = ((ObjectResult)controller.Create(preference).Result).Value as PreferenceResponseDto;

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

    [Fact]
    public void Create_fails_invalid_data()
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

        var preference = new PreferenceCreateDto
        {
            UserId = -21,
            DifficultyLevel = 1,
            WalkingRating = 4,
            CyclingRating = 1,
            CarRating = 1,
            BoatRating = 1,
            SelectedTags = new List<string> { "tag1", "tag2" }
        };

        // Act
        var result = (ObjectResult)controller.Create(preference).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "3") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var updatedPreference = new PreferenceUpdateDto
        {
            Id = 25,
            UserId = 0,
            DifficultyLevel = 1,
            WalkingRating = 1,
            CyclingRating = 1,
            CarRating = 1,
            BoatRating = 1,
            SelectedTags = new List<string> { "tag1", "tag2" }
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedPreference).Result)?.Value as PreferenceResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.UserId.ShouldBe(3);
        result.DifficultyLevel.ShouldBe(1);
        result.WalkingRating.ShouldBe(1);
        result.CyclingRating.ShouldBe(1);
        result.CarRating.ShouldBe(1);
        result.BoatRating.ShouldBe(1);
        result.SelectedTags.ShouldBe(new[] { "tag1", "tag2" });

        // Assert - Database
        var storedEntity = dbContext.Preferences.FirstOrDefault(i => i.Id == 25);
        storedEntity.ShouldNotBeNull();
        storedEntity.DifficultyLevel.ShouldBe(1);
        storedEntity.WalkingRating.ShouldBe(1);
        storedEntity.CyclingRating.ShouldBe(1);
        storedEntity.CarRating.ShouldBe(1);
        storedEntity.BoatRating.ShouldBe(1);
        storedEntity.SelectedTags.ShouldBe(new[] { "tag1", "tag2" });
        var oldEntity = dbContext.Preferences.FirstOrDefault(i => i.DifficultyLevel == 25);
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "3") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var updatedPreference = new PreferenceUpdateDto
        {
            Id = -1000,
            UserId = 0,
            DifficultyLevel = 1,
            WalkingRating = 1,
            CyclingRating = 1,
            CarRating = 1,
            BoatRating = 1,
            SelectedTags = new List<string> { "tag1", "tag2" }
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedPreference).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(28);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        var storedPreference = dbContext.Preferences.FirstOrDefault(i => i.Id == 28);
        storedPreference.ShouldBeNull();
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

    private static PreferenceController CreateController(IServiceScope scope)
    {
        return new PreferenceController(scope.ServiceProvider.GetRequiredService<IPreferenceService>());
    }
}
