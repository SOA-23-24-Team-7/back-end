using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;
[Collection("Sequential")]

public class TourStatisticsCommandTests : BaseToursIntegrationTest
{

    public TourStatisticsCommandTests(ToursTestFactory factory) : base(factory) { }

    [Theory]
    [InlineData(-11, 3, 1)]
    [InlineData(-12, 0, 0)]
    public void StatisticsForTour(int tourId, int startedNumber, int completedNumber)
    {

        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result1 = controller.GetNumberOfStartedTourExecutionSessions(tourId);
        var result2 = controller.GetNumberOfCompletedTourExecutionSessions(tourId);

        // Assert - Database
        result1.ShouldBe(startedNumber);
        result2.ShouldBe(completedNumber);
    }


    [Theory]
    [InlineData(1, 1)]
    public void StatisticsForAuthorsTours(int startedNumber, int completedNumber)
    {

        // Arrange - Controller and dbContext
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result1 = controller.GetNumberOfStartedToursByPurchase();
        var result2 = controller.GetNumberOfCompletedToursByPurchase();


        // Assert - Database
        result1.ShouldBe(startedNumber);
        result2.ShouldBe(completedNumber);
    }

    private static TourStatisticsController CreateController(IServiceScope scope)
    {
        return new TourStatisticsController(scope.ServiceProvider.GetRequiredService<Explorer.Tours.API.Public.ITourStatisticsService>(),
                                            scope.ServiceProvider.GetRequiredService<Explorer.Payments.API.Public.ITourStatisticsService>(),
                                            scope.ServiceProvider.GetRequiredService<Explorer.Encounters.API.Public.ITourStatisticsService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
