using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections;
using System.Security.Claims;

namespace Explorer.Payments.Tests.Integration;

[Collection("Sequential")]
public class TourSaleCommandTests : BasePaymentsIntegrationTest
{
    public TourSaleCommandTests(PaymentsTestFactory factory) : base(factory) { }

    public class TourSaleTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 0, "Autumn sale", 1, 1, 0.33, new long[] { -4 } };
            yield return new object[] { -1, "", 1, 1, 0.33, new long[] { -4 } };
            yield return new object[] { -1, "Autumn sale", 2, 1, 0.33, new long[] { -4 } };
            yield return new object[] { -1, "Autumn sale", 1, 1, 1.5, new long[] { -4 } };
            yield return new object[] { -1, "Autumn sale", 1, 1, -0.33, new long[] { -4 } };
            yield return new object[] { -1, "Autumn sale", 1, 1, 0.33, new long[] { } };
            yield return new object[] { -1, "Autumn sale", 1, 1, 0.33, new long[] { 0 } };
            yield return new object[] { -1, "Autumn sale", 10, 11, 0.33, new long[] { -1, -2, -3 } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

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
    [ClassData(typeof(TourSaleTestData))]
    public void Create_fails_invalid_data(long authorId, string name, int startDay, int endDay, double discountPercentage, long[] tourIds)
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

    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var updatedEntity = new TourSaleUpdateDto
        {
            Id = -2,
            AuthorId = -2,
            Name = "Autumn sale",
            StartDate = new DateOnly(2023, 12, 1),
            EndDate = new DateOnly(2023, 12, 1),
            DiscountPercentage = 0.33,
            TourIds = new List<long> { -11, -12, -13 }
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourSaleResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(updatedEntity.Id);
        result.AuthorId.ShouldBe(-2);
        result.Name.ShouldBe(updatedEntity.Name);
        result.StartDate.ShouldBe(updatedEntity.StartDate);
        result.EndDate.ShouldBe(updatedEntity.EndDate);
        result.DiscountPercentage.ShouldBe(updatedEntity.DiscountPercentage);
        result.TourIds.ShouldBe(updatedEntity.TourIds);
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var updatedEntity = new TourSaleUpdateDto
        {
            Id = -1000,
            AuthorId = -1,
            Name = "Autumn sale",
            StartDate = new DateOnly(2023, 12, 1),
            EndDate = new DateOnly(2023, 12, 1),
            DiscountPercentage = 0.33,
            TourIds = new List<long> { -11, -12, -13 }
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

        // Act
        var result = (OkResult)controller.Delete(-2);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);
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

    private static TourSaleController CreateController(IServiceScope scope)
    {
        return new TourSaleController(scope.ServiceProvider.GetRequiredService<ITourSaleService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
