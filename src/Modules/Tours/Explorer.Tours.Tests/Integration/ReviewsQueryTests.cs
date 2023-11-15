using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration;

[Collection("Sequential")]
public class ReviewsQueryTests : BaseToursIntegrationTest
{
    public ReviewsQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAllByTourId(0, 0, -5).Result)?.Value as PagedResult<ReviewResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(3);
        result.TotalCount.ShouldBe(3);
    }

    [Fact]
    public void Exists()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = controller.ReviewExists(3, 5);


        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ActionResult<Boolean>>();
       
    }

    private static ReviewController CreateController(IServiceScope scope)
    {
        return new ReviewController(scope.ServiceProvider.GetRequiredService<IReviewService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}