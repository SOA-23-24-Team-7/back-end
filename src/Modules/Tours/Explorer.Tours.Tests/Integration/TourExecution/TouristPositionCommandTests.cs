using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist.TourExecution;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration;

[Collection("Sequential")]
public class TouristPositionCommandTests : BaseToursIntegrationTest
{
    public TouristPositionCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TouristPositionCreateDto
        {
            TouristId = -3,
            Longitude = 4.0,
            Latitude = 13.0
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TouristPositionResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Longitude.ShouldBe(newEntity.Longitude);
        result.Latitude.ShouldBe(newEntity.Latitude);

        // Assert - Database
        var storedEntity = dbContext.TouristPositions.FirstOrDefault(i => i.TouristId == newEntity.TouristId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Longitude.ShouldBe(result.Longitude);
        storedEntity.Latitude.ShouldBe(result.Latitude);
    }

    [Theory]
    [InlineData(200.0, 0.0)]
    [InlineData(0.0, 200.0)]
    public void Create_fails_invalid_data(double longitude, double latitude)
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TouristPositionCreateDto
        {
            TouristId = -4,
            Longitude = longitude,
            Latitude = latitude
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

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
        var updatedEntity = new TouristPositionUpdateDto
        {
            TouristId = -2,
            Longitude = 20.322,
            Latitude = 54.321
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TouristPositionResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Longitude.ShouldBe(updatedEntity.Longitude);
        result.Latitude.ShouldBe(updatedEntity.Latitude);

        // Assert - Database
        var storedEntity = dbContext.TouristPositions.FirstOrDefault(i => i.TouristId == -2);
        storedEntity.ShouldNotBeNull();
        storedEntity.Longitude.ShouldBe(updatedEntity.Longitude);
        storedEntity.Latitude.ShouldBe(updatedEntity.Latitude);
    }

    private static TouristPositionController CreateController(IServiceScope scope)
    {
        return new TouristPositionController(scope.ServiceProvider.GetRequiredService<ITouristPositionService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}