using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Tours.API.Dtos.TouristPosition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Encounters.Tests.Integration.SocialEncounter;

[Collection("Sequential")]
public class SocialEncounterCommandTests : BaseEncountersIntegrationTest
{
    public SocialEncounterCommandTests(EncountersTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public void Succesfully_create_social_encounter()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var createEncounterDto = new SocialEncounterCreateDto
        {
            Title = "test",
            Description = "test",
            Longitude = 45.45,
            Latitude = 45.45,
            Radius = 100,
            XpReward = 5,
            Status = EncounterStatus.Active,
            PeopleNumber = 1,
            Picture = "https://static.vecteezy.com/system/resources/previews/009/273/280/non_2x/concept-of-loneliness-and-disappointment-in-love-sad-man-sitting-element-of-the-picture-is-decorated-by-nasa-free-photo.jpg"
        };
        var controller = CreateSocialEncounterController(scope);
        // Act
        var result = (ObjectResult)controller.Create(createEncounterDto).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);
        SocialEncounterResponseDto resultValue = (SocialEncounterResponseDto)result.Value;
        long id = resultValue.Id;
        // Assert - Database
        var storedEntity = dbContext.Encounters.FirstOrDefault(e => e.Id == id);
        storedEntity.ShouldNotBeNull();
    }

    [Fact]
    public void Unsuccesfully_create_social_encounter()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var createEncounterDto = new SocialEncounterCreateDto
        {
            Title = "test",
            Description = "test",
            Longitude = 45.45,
            Latitude = 45.45,
            Radius = 100,
            XpReward = 5,
            Status = EncounterStatus.Active,
            PeopleNumber = 0
        };
        var controller = CreateSocialEncounterController(scope);
        // Act
        var result = (ObjectResult)controller.Create(createEncounterDto).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(500);
        // Assert - Database
        var storedEntity = dbContext.Encounters.FirstOrDefault(e => e.Id == 1);
        storedEntity.ShouldBeNull();
    }

    [Fact]
    public void Succesfully_activate_social_encounter()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);
        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var touristPositionDto = new TouristPositionCreateDto
        {
            TouristId = -1,
            Longitude = 45.45,
            Latitude = 45.45
        };
        // Act
        var result = (ObjectResult)controller.Activate(touristPositionDto, -1).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);
    }

    [Fact]
    public void Unsuccesfully_activate_social_encounter()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);
        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var touristPositionDto = new TouristPositionCreateDto
        {
            TouristId = -1,
            Longitude = 46.55,
            Latitude = 45.45
        };
        // Act
        var result = (ObjectResult)controller.Activate(touristPositionDto, -1).Result;

        // Assert - Response
        result.StatusCode.ShouldBe(500);
    }

    [Fact]
    public void Succesfully_complete_social_encounter()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);
        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var touristPositionDto = new TouristPositionCreateDto
        {
            TouristId = -21,
            Longitude = 45.45,
            Latitude = 45.45
        };
        // Act
        var result = (ObjectResult)controller.Complete(-2).Result;

        // Assert - Response
        result.StatusCode.ShouldBe(200);
    }

    [Fact]
    public void Unsuccesfull_complete_social_encounter()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);
        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var touristPositionDto = new TouristPositionCreateDto
        {
            TouristId = -1,
            Longitude = 45.45,
            Latitude = 45.45
        };
        // Act
        var result = (ObjectResult)controller.Complete(-3).Result;

        // Assert - Response
        result.StatusCode.ShouldBe(400);
    }

    private static Explorer.API.Controllers.Author.SocialEncounterController CreateSocialEncounterController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Author.SocialEncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

    private static Explorer.API.Controllers.Tourist.EncounterController CreateEncounterController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Tourist.EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>(), scope.ServiceProvider.GetRequiredService<ITouristProgressService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}