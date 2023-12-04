using Explorer.API.Controllers;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Encounters.Tests.Integration.SocialEncounter;

[Collection("Sequential")]
public class SocialEncounterCommandTests : BaseEncountersIntegrationTest
{
    public SocialEncounterCommandTests(EncountersTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public void Succesfully_activate_social_encounter()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var socialEncounterDto = SetupSocialEncounterDto();
        var controller = CreateSocialEncounterController(scope);
        // Act
        //var result = ((ObjectResult)controller.Complete(socialEncounterDto).Result)?.Value as SocialEncounterResponseDto;

        // Assert - Response
        //result.ShouldNotBeNull();
        // Assert - Database
    }

    private SocialEncounterCompleteDto SetupSocialEncounterDto()
    {
        return new SocialEncounterCompleteDto
        {
            UserId = 0,
            EncounterId = 0   //lokacija ispred studentskog doma Car Lazar na Limanu
        };
    }

    private static SocialEncounterController CreateSocialEncounterController(IServiceScope scope)
    {
        return new SocialEncounterController(scope.ServiceProvider.GetRequiredService<ISocialEncounterService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
