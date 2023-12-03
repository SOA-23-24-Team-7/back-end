﻿using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration;

public class TourSaleCommandTests : BasePaymentsIntegrationTest
{
    public TourSaleCommandTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var newEntity = new TourSaleCreateDto
        {
            AuthorId = -1,
            Name = "Autumn sale",
            StartDate = new DateOnly(2023, 12, 1),
            EndDate = new DateOnly(2023, 12, 1),
            DiscountPercentage = 0.33,
            TourIds = new List<long> { -1, -2, -3 }
        };

        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourSaleResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.AuthorId.ShouldBe(newEntity.AuthorId);
        result.Name.ShouldBe(newEntity.Name);
        result.StartDate.ShouldBe(newEntity.StartDate);
        result.EndDate.ShouldBe(newEntity.EndDate);
        result.DiscountPercentage.ShouldBe(newEntity.DiscountPercentage);
        result.TourIds.ShouldBe(newEntity.TourIds);
    }

    [Theory]
    [InlineData(0, "Autumn sale", 1, 1, 0.33, -1, -2, -3)]
    [InlineData(-1, "", 1, 1, 0.33, -1, -2, -3)]
    [InlineData(-1, "Autumn sale", 2, 1, 0.33, -1, -2, -3)]
    [InlineData(-1, "Autumn sale", 1, 1, 1.5, -1, -2, -3)]
    [InlineData(-1, "Autumn sale", 1, 1, -0.33, -1, -2, -3)]
    [InlineData(-1, "Autumn sale", 1, 1, 0.33)]
    [InlineData(-1, "Autumn sale", 1, 1, 0.33, -1, 0, -3)]
    public void Create_fails_invalid_data(long authorId, string name, int startDay, int endDay, double discountPercentage, params long[] tourIds)
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var newEntity = new TourSaleCreateDto
        {
            AuthorId = authorId,
            Name = name,
            StartDate = new DateOnly(2023, 12, startDay),
            EndDate = new DateOnly(2023, 12, endDay),
            DiscountPercentage = discountPercentage,
            TourIds = tourIds
        };

        // Act
        var result = (ObjectResult)controller.Create(newEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    private static TourSaleController CreateController(IServiceScope scope)
    {
        return new TourSaleController()
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
