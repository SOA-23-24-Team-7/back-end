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
            ImagePath = "kej.png",
            Order = 2
        };

        // Act
        var result = ((ObjectResult)controller.CreateKeyPoint(-1, newEntity).Result)?.Value as KeyPointDto;

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

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new KeyPointDto
        {
            Id = -1,
            TourId = -1,
            Name = "Rimokatolicka crkva Ime Marijino",
            Description = "Rimokatolicka crkva u centru grada",
            Longitude = -12.643,
            Latitude = 78.54,
            ImagePath = "new-image.png",
            Order = 0
        };

        // Act
        var result = ((ObjectResult)controller.Update(-1, -1, updatedEntity).Result)?.Value as KeyPointDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Name.ShouldBe(updatedEntity.Name);
        result.Description.ShouldBe(updatedEntity.Description);
        result.Longitude.ShouldBe(updatedEntity.Longitude);
        result.Latitude.ShouldBe(updatedEntity.Latitude);
        result.ImagePath.ShouldBe(updatedEntity.ImagePath);
        result.Order.ShouldBe(updatedEntity.Order);

        // Assert - Database
        var storedEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Name == updatedEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Description.ShouldBe(updatedEntity.Description);
        storedEntity.Longitude.ShouldBe(updatedEntity.Longitude);
        storedEntity.Latitude.ShouldBe(updatedEntity.Latitude);
        storedEntity.ImagePath.ShouldBe(updatedEntity.ImagePath);
        storedEntity.Order.ShouldBe(updatedEntity.Order);
        var oldEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Name == "Katedrala");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new KeyPointDto
        {
            Id = -1000,
            TourId = -1,
            Name = "Test",
            Description = "Test",
            Longitude = 0,
            Latitude = 0,
            ImagePath = "Test",
            Order = 0
        };

        // Act
        var result = (ObjectResult)controller.Update(-1, -1000, updatedEntity).Result;

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
        var result = (OkResult)controller.Delete(-1, -1);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.KeyPoints.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1, -1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static KeyPointController CreateController(IServiceScope scope)
    {
        return new KeyPointController(scope.ServiceProvider.GetRequiredService<IKeyPointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
