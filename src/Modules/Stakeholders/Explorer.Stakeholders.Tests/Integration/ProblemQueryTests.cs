using Shouldly;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
        var controller = CreateTouristController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ProblemResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(5);
        result.TotalCount.ShouldBe(5);
    }


    [Fact]
    public void Retrieves_all_comments_for_problem()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);

        // Act
        var result = ((ObjectResult)controller.GetComments(-5).Result)?.Value as PagedResult<ProblemCommentResponseDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(1);
        result.TotalCount.ShouldBe(1);
    }

    [Fact]
    public void Retrieves_tourist_answer()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);

        // Act
        var result = ((ObjectResult)controller.GetProblemAnswer(-4).Result)?.Value as ProblemAnswerDto;

        // Assert
        result.ShouldNotBeNull();
    }

    [Fact]
    public void Retrieves_author_answer()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateAuthorController(scope);

        // Act
        var result = ((ObjectResult)controller.GetProblemAnswer(-4).Result)?.Value as ProblemAnswerDto;

        // Assert
        result.ShouldNotBeNull();
    }

    [Fact]
    public void Retrieves_admin_answer()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateAdminController(scope);

        // Act
        var result = ((ObjectResult)controller.GetProblemAnswer(-4).Result)?.Value as ProblemAnswerDto;

        // Assert
        result.ShouldNotBeNull();
    }

    private static Explorer.API.Controllers.Tourist.ProblemController CreateTouristController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Tourist.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

    private static Explorer.API.Controllers.Author.ProblemController CreateAuthorController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Author.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

    private static Explorer.API.Controllers.Administrator.ProblemController CreateAdminController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Administrator.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}