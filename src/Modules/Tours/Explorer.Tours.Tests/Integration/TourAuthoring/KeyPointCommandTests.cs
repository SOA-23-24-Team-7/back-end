using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;

[Collection("Sequential")]
public class KeyPointCommandTests : BaseToursIntegrationTest
{
    public KeyPointCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new KeyPointDto
        {
            Name = "Kej",
            Description = "Kej na obali Dunava.",
            Longitude = 12.542,
            Latitude = 54.3221,
            ImagePath = "kej.png"
        };

        // Act
        var result = ((ObjectResult)controller.CreateKeyPoint(1, newEntity).Result)?.Value as KeyPointDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);

        // Assert - Database
        var storedEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new KeyPointDto
        {
            Description = "Test"
        };

        // Act
        var result = (ObjectResult)controller.CreateKeyPoint(1, updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    private static TourController CreateController(IServiceScope scope)
    {
        return new TourController(scope.ServiceProvider.GetRequiredService<IKeyPointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
