using Explorer.API.Controllers.Tourist.MarketPlace;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.MarketPlace;

[Collection("Sequential")]
public class KeyPointQueryTests : BaseToursIntegrationTest
{
    public KeyPointQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all_for_single_tour()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetKeyPoints(-1).Result)?.Value as List<KeyPointResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
    }

    private static KeyPointController CreateController(IServiceScope scope)
    {
        return new KeyPointController(scope.ServiceProvider.GetRequiredService<IKeyPointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
