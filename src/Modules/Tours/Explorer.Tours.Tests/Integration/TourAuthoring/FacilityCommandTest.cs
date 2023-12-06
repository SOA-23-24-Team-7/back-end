using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;

[Collection("Sequential")]
public class FacilityCommandTests : BaseToursIntegrationTest
{
    public FacilityCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new FacilityCreateDto
        {
            Name = "Parking",
            Description = "Ogroman parking sa cak 200 mesta.",
            ImagePath = "url",
            AuthorId = 1,
            Category = FacilityCategory.ParkingLot,
            Longitude = 45.0,
            Latitude = 17.0,
            //Status=PublicStatus.Accepted,
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as FacilityResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);

        // Assert - Database
        var storedEntity = dbContext.Facilities.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new FacilityCreateDto
        {
            Description = "Test"
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
        var updatedEntity = new FacilityUpdateDto
        {
            Id = -1,
            Name = "Apoteka",
            Description = "Veoma uredna apoteka sa pristupacnim cenama",
            ImagePath = "url2",
            AuthorId = 1,
            Category = FacilityCategory.Pharmacy,
            Longitude = 45.0,
            Latitude = 17.0
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as FacilityResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Name.ShouldBe(updatedEntity.Name);
        result.Description.ShouldBe(updatedEntity.Description);
        result.ImagePath.ShouldBe(updatedEntity.ImagePath);
        result.AuthorId.ShouldBe(1);
        result.Category.ShouldBe(updatedEntity.Category);
        result.Longitude.ShouldBe(updatedEntity.Longitude);
        result.Latitude.ShouldBe(updatedEntity.Latitude);

        // Assert - Database
        var storedEntity = dbContext.Facilities.FirstOrDefault(i => i.Name == "Apoteka");
        storedEntity.ShouldNotBeNull();
        storedEntity.Description.ShouldBe(updatedEntity.Description);
        storedEntity.Category.ToString().ShouldBe(updatedEntity.Category.ToString());
        storedEntity.ImagePath.ShouldBe(updatedEntity.ImagePath);
        storedEntity.AuthorId.ShouldBe(1);
        storedEntity.Longitude.ShouldBe(updatedEntity.Longitude);
        storedEntity.Latitude.ShouldBe(updatedEntity.Latitude);
        var oldEntity = dbContext.Facilities.FirstOrDefault(i => i.Name == "Test");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new FacilityUpdateDto
        {
            Id = -1000,
            Name = "Test"
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedEntity).Result;

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
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.Facilities.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
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

    private static FacilityController CreateController(IServiceScope scope)
    {
        return new FacilityController(scope.ServiceProvider.GetRequiredService<IFacilityService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}