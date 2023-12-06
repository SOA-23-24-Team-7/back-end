using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Payments.Tests.Integration;

[Collection("Sequential")]
public class TourSaleQueryTests : BasePaymentsIntegrationTest
{
    public TourSaleQueryTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all_by_author()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var userContext = new ClaimsIdentity(new Claim[] { new Claim("id", "-1") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(userContext)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var result = ((ObjectResult)controller.GetByAuthor().Result)?.Value as List<TourSaleResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public void Retrieves_by_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var result = ((ObjectResult)controller.GetById(-2).Result)?.Value as TourSaleResponseDto;

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-2);
    }

    [Fact]
    public void Retrieves_tour_discount()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var result = ((ObjectResult)controller.GetDiscountForTour(-5).Result)?.Value as double?;

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(0.5);
    }

    private static TourSaleController CreateController(IServiceScope scope)
    {
        return new TourSaleController(scope.ServiceProvider.GetRequiredService<ITourSaleService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
