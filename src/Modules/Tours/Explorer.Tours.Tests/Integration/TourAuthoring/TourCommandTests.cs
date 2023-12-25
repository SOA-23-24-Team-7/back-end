using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;
[Collection("Sequential")]
public class TourCommandTests : BaseToursIntegrationTest
{
    public TourCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourCreateDto
        {
            AuthorId = 1,
            Name = "Tura Novog Sada",
            Description = "The best!",
            Difficulty = 3,
            Tags = new List<string> { "istorija", "kultura" },
            /*Status = TourStatus.Draft,
            Price = 0,
            IsDeleted = false*/
        };

        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.AuthorId.ShouldBe(newEntity.AuthorId);
        result.Name.ShouldBe(newEntity.Name);
        result.Description.ShouldBe(newEntity.Description);
        result.Difficulty.ShouldBe(newEntity.Difficulty);
        result.Price.ShouldBe(newEntity.Price);
        result.Status.ShouldBe(newEntity.Status);
        result.IsDeleted.ShouldBe(newEntity.IsDeleted);
        result.Tags.ShouldBe(newEntity.Tags);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
        storedEntity.Name.ShouldBe(result.Name);
        storedEntity.Description.ShouldBe(result.Description);
        storedEntity.Difficulty.ShouldBe(result.Difficulty);
        storedEntity.Tags.ShouldBe(result.Tags);
        storedEntity.Price.ShouldBe(result.Price);
        storedEntity.Status.ToString().ShouldBe(result.Status.ToString());
        storedEntity.IsDeleted.ShouldBe(result.IsDeleted);
    }



    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourCreateDto
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
        var updatedEntity = new TourUpdateDto
        {
            Id = -1,
            AuthorId = 1,
            Name = "Tečnost",
            Description = "The best!",
            Difficulty = 5,
            Tags = new List<string> { "sport", "priroda" },
            Status = TourStatus.Draft,
            Price = 0,
            IsDeleted = false,
            Category = TourCategory.Adventure
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.AuthorId.ShouldBe(1);
        result.Name.ShouldBe(updatedEntity.Name);
        result.Description.ShouldBe(updatedEntity.Description);
        result.Difficulty.ShouldBe(updatedEntity.Difficulty);
        result.Tags.ShouldBe(updatedEntity.Tags);
        result.Price.ShouldBe(updatedEntity.Price);
        result.Status.ShouldBe(updatedEntity.Status);
        result.IsDeleted.ShouldBeFalse();
        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == "Tečnost");
        storedEntity.ShouldNotBeNull();
        storedEntity.Description.ShouldBe(updatedEntity.Description);
        storedEntity.Difficulty.ShouldBe(updatedEntity.Difficulty);
        storedEntity.Tags.ShouldBe(updatedEntity.Tags);
        storedEntity.Price.ShouldBe(updatedEntity.Price);
        storedEntity.Status.ToString().ShouldBe(updatedEntity.Status.ToString());
        storedEntity.IsDeleted.ShouldBe(updatedEntity.IsDeleted);
        var oldEntity = dbContext.Tours.FirstOrDefault(i => i.Name == "Voda");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourUpdateDto
        {
            Id = -1000,
            Name = "Test",
            Description = "Nes",
            Difficulty = 1,
            Tags = new List<string> { "sport", "priroda" }
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
        var storedCourse = dbContext.Tours.FirstOrDefault(i => i.Id == -3);
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

    [Fact]
    public void Create_Tour_Equipment()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        int tourId = -10;
        int equipmentId = -3;

        //Act
        var result = (OkResult)controller.AddEquipment(tourId, equipmentId);

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        //Assert-Database
        var tour_eq = dbContext.Equipment
                                .Where(e => e.Tours.Any(t => t.Id == tourId));
        tour_eq.ShouldNotBeNull();
        tour_eq.Count().ShouldBe(2);
    }

    [Fact]
    public void Create_tour_equipment_fails_invalid_tourId()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        int tourId = -50000;
        int equipmentId = -3;

        //Act
        var result = (ObjectResult)controller.AddEquipment(tourId, equipmentId);

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Create_tour_equipment_fails_invalid_equipmentId()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        int tourId = -3;
        int equipmentId = -40000;

        //Act
        var result = (ObjectResult)controller.AddEquipment(tourId, equipmentId);

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Delete_Tour_Equipment()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        int tourId = -1;
        int equipmentId = -3;

        //Act
        var result = (OkResult)controller.DeleteEquipment(tourId, equipmentId);

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        //Assert-Database
        var tour_eq = dbContext.Equipment
                                .Where(e => e.Tours.Any(t => t.Id == tourId));
        tour_eq.ShouldNotBeNull();
        tour_eq.Count().ShouldBe(2);
    }

    [Fact]
    public void Delete_tour_equipment_fails_invalid_tourId()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        int tourId = -50000;
        int equipmentId = -3;

        //Act
        var result = (ObjectResult)controller.DeleteEquipment(tourId, equipmentId);

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Delete_tour_equipment_fails_invalid_equipmentId()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        int tourId = -1;
        int equipmentId = -30000;

        //Act
        var result = (ObjectResult)controller.DeleteEquipment(tourId, equipmentId);

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Publish_succeeds()
    {
        // Arrange - Input data
        var tourId = -3;
        var expectedResponseCode = 200;
        var expectedStatus = TourStatus.Published;
        var expectedDate = "0001-01-01 12:00:00.789123+00:00";

        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-11") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        // Act
        var result = (OkResult)controller.Publish(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        storedEntity.PublishDate.ToString().Equals(expectedDate);
    }

    [Fact]
    public void Publish_fails_invalid_keypoints()
    {
        // Arrange - Input data
        var tourId = -4;
        var expectedResponseCode = 400;
        var expectedStatus = TourStatus.Draft;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.Publish(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
    }

    // Supericika moja smicika pametnicika

    [Fact]
    public void Publish_fails_invalid_durations()
    {
        // Arrange - Input data
        var tourId = -5;
        var expectedResponseCode = 400;
        var expectedStatus = TourStatus.Draft;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.Publish(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
    }

    [Fact]
    public void Publish_fails_wrong_author()
    {
        // Arrange - Input data
        var tourId = -6;
        var expectedResponseCode = 400;
        var expectedStatus = TourStatus.Draft;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.Publish(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
    }

    [Fact]
    public void Archive_succeeds()
    {
        // Arrange - Input data
        var tourId = -7;
        var expectedResponseCode = 200;
        var expectedStatus = TourStatus.Archived;
        //var expectedDate = "0001-01-01 12:00:00.789123+00:00";

        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-11") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        // Act
        var result = (OkResult)controller.Archive(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        //storedEntity.ArchiveDate.ToString().Equals(expectedDate);
    }

    [Fact]
    public void Archive_fails_invalid_status()
    {
        // Arrange - Input data
        var tourId = -9;
        var expectedResponseCode = 400;
        var expectedStatus = TourStatus.Draft;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.Archive(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
    }

    [Fact]
    public void Archive_fails_wrong_author()
    {
        // Arrange - Input data
        var tourId = -8;
        var expectedResponseCode = 400;
        var expectedStatus = TourStatus.Published;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.Archive(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
    }

    [Fact]
    public void Ready_succeeds()
    {
        // Arrange - Input data
        var tourId = -10;
        var expectedResponseCode = 200;
        var expectedStatus = TourStatus.Ready;
        //var expectedDate = "0001-01-01 12:00:00.789123+00:00";

        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-11") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        // Act
        var result = (OkResult)controller.MarkAsReady(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        //storedEntity.PublishDate.ToString().Equals(expectedDate);
    }

    [Fact]
    public void Ready_fails_invalid_keypoints()
    {
        // Arrange - Input data
        var tourId = -9;
        var expectedResponseCode = 400;
        var expectedStatus = TourStatus.Draft;
        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.MarkAsReady(tourId).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
    }



    private static TourController CreateController(IServiceScope scope)
    {
        return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
