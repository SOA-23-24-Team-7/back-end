using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;

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
        var result = ((ObjectResult)controller.GetKeyPoints(-1).Result)?.Value as List<KeyPointDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
    }

    private static TourController CreateController(IServiceScope scope)
    {
        return new TourController(scope.ServiceProvider.GetRequiredService<IKeyPointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
