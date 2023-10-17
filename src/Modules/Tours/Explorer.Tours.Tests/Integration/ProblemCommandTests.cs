using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration;

[Collection("Sequential")]
public class ProblemCommandTests : BaseToursIntegrationTest
{
    public ProblemCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new ProblemDto
        {
            Category="Kategorija1",
            Priority="Bitno",
            Description="Smislicu",
            ReportedTime="10:00AM",
            TourId = -3,
            TouristId=-1,
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ProblemDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.TourId.ShouldBe(newEntity.TourId);
        result.TouristId.ShouldBe(newEntity.TouristId);
        // Assert - Database
        var storedEntity = dbContext.Problem.FirstOrDefault(i => i.TourId == newEntity.TourId && i.TouristId==newEntity.TouristId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new ProblemDto
        {
            Id = 1, 
            Category = "", 
            Priority = "High", 
            Description = "", 
            ReportedTime = "9",
            TourId = 0, 
            TouristId = 0 
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
        var updatedEntity = new ProblemDto
        {
            Id = -1,
            Category = "Kategorija1",
            Priority = "Veoma bitno",
            Description = "Nije ukljuceno u turu sve sto je bilo navedeno.",
            ReportedTime = "10:00AM",
            TourId = -3,
            TouristId = -5,
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ProblemDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Priority.ShouldBe(updatedEntity.Priority);
        result.Description.ShouldBe(updatedEntity.Description);

        // Assert - Database
        var storedEntity = dbContext.Problem.FirstOrDefault(i => i.TourId == -3 && i.TouristId==-5);
        storedEntity.ShouldNotBeNull();
        storedEntity.TourId.ShouldBe(updatedEntity.TourId);
        var oldEntity = dbContext.Problem.FirstOrDefault(i => i.TourId == -1 && i.TouristId==-1);
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new ProblemDto
        {
            Id = -1000,
            Category = "Kategorija1",
            Priority = "Veoma bitno",
            Description = "Nije ukljuceno u turu sve sto je bilo navedeno.",
            ReportedTime = "10:00AM",
            TourId = -3,
            TouristId = -5,
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
        var storedCourse = dbContext.Problem.FirstOrDefault(i => i.Id == -3);
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

    private static ProblemController CreateController(IServiceScope scope)
    {
        return new ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}