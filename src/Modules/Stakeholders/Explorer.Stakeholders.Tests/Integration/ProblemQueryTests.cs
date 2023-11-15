using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration;

[Collection("Sequential")]
public class ProblemQueryTests : BaseStakeholdersIntegrationTest
{
    public ProblemQueryTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ProblemResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(4);
        result.TotalCount.ShouldBe(4);
    }


    [Fact]
    public void Retrieves_all_comments_for_problem_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetComments(-5).Result)?.Value as PagedResult<ProblemCommentResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(1);
        result.TotalCount.ShouldBe(1);
    }

    private static ProblemController CreateController(IServiceScope scope)
    {
        return new ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}