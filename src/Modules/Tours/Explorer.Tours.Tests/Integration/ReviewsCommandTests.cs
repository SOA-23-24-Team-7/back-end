using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration;

[Collection("Sequential")]
public class ReviewsCommandTests : BaseToursIntegrationTest
{
    public ReviewsCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new ReviewCreateDto
        {
            Rating = 2,
            Comment = "Not so good.",
            TouristId = 3,
            TourVisitDate = DateOnly.MinValue,
            CommentDate = DateOnly.MinValue,
            TourId = -2,
            Images = new List<string> { "https://img.freepik.com/free-photo/painting-mountain-lake-with-mountain-background_188544-9126.jpg?size=626&ext=jpg&ga=GA1.1.1413502914.1697414400&semt=sph" }
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ReviewResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Rating.ShouldBe(newEntity.Rating);
        result.Comment.ShouldBe(newEntity.Comment);
        result.TouristId.ShouldBe(newEntity.TouristId);
        result.TourVisitDate.ShouldBe(newEntity.TourVisitDate);
        result.CommentDate.ShouldBe(newEntity.CommentDate);
        result.TourId.ShouldBe(newEntity.TourId);
        result.Images.ShouldBe(newEntity.Images);

        // Assert - Database
        var storedEntity = dbContext.Reviews.FirstOrDefault(i => i.Rating == newEntity.Rating);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
        storedEntity.Rating.ShouldBe(result.Rating);
        storedEntity.Comment.ShouldBe(result.Comment);
        storedEntity.TouristId.ShouldBe(result.TouristId);
        storedEntity.TourVisitDate.ShouldBe(result.TourVisitDate);
        storedEntity.CommentDate.ShouldBe(result.CommentDate);
        storedEntity.TourId.ShouldBe(result.TourId);
        storedEntity.Images.ShouldBe(result.Images);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new ReviewCreateDto
        {
            Comment = "Test"
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
        var updatedEntity = new ReviewUpdateDto
        {
            Id = -1,
            Rating = 4,
            Comment = "Good, but could be better.",
            TouristId = 3,
            TourVisitDate = new DateOnly(),
            CommentDate = new DateOnly(),
            TourId = -2,
            Images = new List<string> { "https://img.freepik.com/free-photo/painting-mountain-lake-with-mountain-background_188544-9126.jpg?size=626&ext=jpg&ga=GA1.1.1413502914.1697414400&semt=sph", "https://media.istockphoto.com/id/517188688/photo/mountain-landscape.jpg?s=612x612&w=0&k=20&c=A63koPKaCyIwQWOTFBRWXj_PwCrR4cEoOw2S9Q7yVl8=" }
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ReviewResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Rating.ShouldBe(updatedEntity.Rating);
        result.Comment.ShouldBe(updatedEntity.Comment);
        result.TouristId.ShouldBe(updatedEntity.TouristId);
        result.TourVisitDate.ShouldBe(updatedEntity.TourVisitDate);
        result.CommentDate.ShouldBe(updatedEntity.CommentDate);
        result.TourId.ShouldBe(updatedEntity.TourId);
        result.Images.ShouldBe(updatedEntity.Images);


        // Assert - Database
        var storedEntity = dbContext.Reviews.FirstOrDefault(i => i.Comment == "Good, but could be better.");
        storedEntity.ShouldNotBeNull();
        storedEntity.Rating.ShouldBe(updatedEntity.Rating);
        storedEntity.TouristId.ShouldBe(updatedEntity.TouristId);
        storedEntity.TourVisitDate.ShouldBe(updatedEntity.TourVisitDate);
        storedEntity.CommentDate.ShouldBe(updatedEntity.CommentDate);
        storedEntity.TourId.ShouldBe(updatedEntity.TourId);
        storedEntity.Images.ShouldBe(updatedEntity.Images);
        var oldEntity = dbContext.Reviews.FirstOrDefault(i => i.Comment == "Great and exciting tour.");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new ReviewUpdateDto
        {
            Id = -1000,
            Rating = 2,
            Comment = "Not so good.",
            TouristId = 3,
            TourVisitDate = DateOnly.MinValue,
            CommentDate = DateOnly.MinValue,
            TourId = 2,
            Images = new List<string> { "https://img.freepik.com/free-photo/painting-mountain-lake-with-mountain-background_188544-9126.jpg?size=626&ext=jpg&ga=GA1.1.1413502914.1697414400&semt=sph" }
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
        var storedCourse = dbContext.Reviews.FirstOrDefault(i => i.Id == -3);
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

    private static ReviewController CreateController(IServiceScope scope)
    {
        return new ReviewController(scope.ServiceProvider.GetRequiredService<IReviewService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}