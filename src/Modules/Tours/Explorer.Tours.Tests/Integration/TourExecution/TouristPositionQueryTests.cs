using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist.TourExecution;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.TourExecution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration;

[Collection("Sequential")]
public class TouristPositionQueryTests : BaseToursIntegrationTest
{
    public TouristPositionQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_for_single_tourist()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetByTouristId(-1).Result)?.Value as TouristPositionResponseDto;

        // Assert
        result.ShouldNotBeNull();
    }

    private static TouristPositionController CreateController(IServiceScope scope)
    {
        return new TouristPositionController(scope.ServiceProvider.GetRequiredService<ITouristPositionService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}