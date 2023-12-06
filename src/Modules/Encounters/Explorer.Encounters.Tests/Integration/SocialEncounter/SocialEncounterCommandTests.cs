namespace Explorer.Encounters.Tests.Integration.SocialEncounter;

[Collection("Sequential")]
public class SocialEncounterCommandTests : BaseEncountersIntegrationTest
{
    public SocialEncounterCommandTests(EncountersTestFactory factory) : base(factory)
    {
    }

    //[Fact]
    //public void Succesfully_activate_social_encounter()
    //{
    //    Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
    //    var touristPositionDto = new TouristPositionCreateDto
    //    {
    //        TouristId = -1,
    //        Longitude = 45.45,
    //        Latitude = 45.45
    //    };
    //    var controller = CreateSocialEncounterController(scope);
    //    Act
    //   var result = (ObjectResult)controller.Activate(touristPositionDto, -2).Result;

    //    Assert - Response
    //    result.ShouldNotBeNull();
    //    result.StatusCode.ShouldBe(500);
    //    Assert - Database
    //    var storedEntity = dbContext.Encounters.FirstOrDefault(e => e.Id = -2);
    //    storedEntity.ShouldNotBeNull();
    //}

    //[Fact]
    //public void Unsucesfully_activate_social_encounter()
    //{
    //    Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var touristPositionDto = new TouristPositionCreateDto
    //    {
    //        TouristId = -1,
    //        Longitude = 45.45,
    //        Latitude = 45.45
    //    };
    //    var controller = CreateSocialEncounterController(scope);
    //    Act
    //   var result = (ObjectResult)controller.Activate(touristPositionDto, -2).Result;

    //    Assert - Response
    //    result.ShouldNotBeNull();
    //    Assert - Database
    //}

    //[Fact]
    //public void Succesfully_complete_social_encounter()
    //{
    //    Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var socialEncounterDto = SetupSocialEncounterDto();
    //    var controller = CreateSocialEncounterController(scope);
    //    // Act
    //    var result = ((ObjectResult)controller.Complete(socialEncounterDto).Result)?.Value as SocialEncounterResponseDto;

    //    // Assert - Response
    //    result.ShouldNotBeNull();
    //    Assert - Database
    //}

    //[Fact]
    //public void Unsucesfullt_complete_social_encounter()
    //{
    //    Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var socialEncounterDto = ;
    //    var controller = CreateSocialEncounterController(scope);
    //    // Act
    //    var result = ((ObjectResult)controller.Complete(socialEncounterDto).Result)?.Value as SocialEncounterResponseDto;

    //    // Assert - Response
    //    result.ShouldNotBeNull();
    //    Assert - Database
    //}

    //private static EncounterController CreateSocialEncounterController(IServiceScope scope)
    //{
    //    return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
    //    {
    //        ControllerContext = BuildContext("-1")
    //    };
    //}
}
