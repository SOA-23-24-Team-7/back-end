using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;

[Collection("Sequential")]
public class TourQueryTests : BaseToursIntegrationTest
{
    public TourQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(16);
        result.TotalCount.ShouldBe(16);
    }

    [Fact]
    public void Retrieves_published()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetPublished(0, 0).Result)?.Value as PagedResult<TourResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(8);
        result.TotalCount.ShouldBe(8);
    }

    [Fact]
    public void Retrieve_Tour_Equipment()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetEquipment(tourId: -1))?.Value as PagedResult<EquipmentResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(3);
        result.TotalCount.ShouldBe(3);
    }
    private static TourController CreateController(IServiceScope scope)
    {
        return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
