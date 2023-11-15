using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;


namespace Explorer.Stakeholders.Tests.Integration;

[Collection("Sequential")]
public class ProblemCommandTests : BaseStakeholdersIntegrationTest
{
    public ProblemCommandTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var newEntity = new ProblemCreateDto
        {
            Category = "Kategorija1",
            Priority = "Bitno",
            Description = "Smislicu",
            ReportedTime = DateTime.UtcNow,
            TourId = -5,
            TouristId = -21,
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ProblemResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.TourId.ShouldBe(newEntity.TourId);
        result.TouristId.ShouldBe(newEntity.TouristId);
        // Assert - Database
        var storedEntity = dbContext.Problem.FirstOrDefault(i => i.Id == result.Id);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);
        var updatedEntity = new ProblemCreateDto
        {
            Category = "",
            Priority = "High",
            Description = "",
            ReportedTime = DateTime.UtcNow,
            TourId = 0,
            TouristId = 0
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    [Fact]
    public void Problem_comment_creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var newEntity = new ProblemCommentCreateDto
        {
            CommenterId = -21,
            Text = "Bitno",
        };

        // Act
        var result = (ObjectResult)controller.CreateComment(newEntity, -4).Result;
        // Assert
        result.StatusCode.ShouldBe(200);
    }

    [Fact]
    public void Problem_comment_fails_invalid_commenter_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var newEntity = new ProblemCommentCreateDto
        {
            CommenterId = -10,
            Text = "Bitno",
        };

        // Act
        var result = (ForbidResult)controller.CreateComment(newEntity, -4).Result;
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var updatedEntity = new ProblemUpdateDto
        {
            Id = -1,
            Category = "Kategorija4",
            Priority = "Bitno",
            Description = "Nije bilo nekih vecih problema.",
            ReportedTime = DateTime.UtcNow,
            TourId = -1,
            TouristId = -21,
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ProblemResponseDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Priority.ShouldBe(updatedEntity.Priority);
        result.TouristId.ShouldBe(updatedEntity.TouristId);
        result.ReportedTime.ShouldBe(updatedEntity.ReportedTime);
        result.TourId.ShouldBe(updatedEntity.TourId);
        result.Description.ShouldBe(updatedEntity.Description);
        // Assert - Database
        var storedEntity = dbContext.Problem.FirstOrDefault(i => i.Description == "Nije bilo nekih vecih problema.");
        storedEntity.ShouldNotBeNull();
        storedEntity.Priority.ShouldBe(updatedEntity.Priority);
        storedEntity.ReportedTime.ShouldBe(updatedEntity.ReportedTime);
        storedEntity.TouristId.ShouldBe(updatedEntity.TouristId);
        storedEntity.TourId.ShouldBe(updatedEntity.TourId);
        var oldEntity = dbContext.Problem.FirstOrDefault(i => i.Description == "Nije ukljuceno u turu sve sto je bilo navedeno.");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);
        var updatedEntity = new ProblemUpdateDto
        {
            Id = -1000,
            Category = "Kategorija1",
            Priority = "Veoma bitno",
            Description = "Nije ukljuceno u turu sve sto je bilo navedeno.",
            ReportedTime = DateTime.UtcNow,
            TourId = -3,
            TouristId = -5,
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
        var controller = CreateTouristController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

        // Act
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.Problem.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }


    [Fact]
    public void Problem_answer_creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateAuthorController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-11") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var newProblemAnswer = new ProblemAnswerDto()
        {
            AuthorId = -11,
            Answer = "Neki jako bitan odgovor"
        };

        // Act
        var result = (OkResult)controller.CreateAnswer(newProblemAnswer, -1);

        // Assert - Response
        result.StatusCode.ShouldBe(200);
        // Assert - Database
        var storedCourse = dbContext.Problem.FirstOrDefault(x => x.Id == -1).Answer;
        storedCourse.ShouldNotBeNull();
    }

    [Fact]
    public void Problem_answer_create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateAuthorController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-11") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var newProblemAnswer = new ProblemAnswerDto()
        {
            AuthorId = -1,
            Answer = "Neki jako bitan odgovor"
        };

        // Act
        var result = (ForbidResult)controller.CreateAnswer(newProblemAnswer, -1);


        // Assert - Database
        var storedCourse = dbContext.Problem.FirstOrDefault(x => x.Id == -1).Answer;
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Problem_resolve_succeeds()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateTouristController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        // Act
        var result = (OkResult)controller.ResolveProblem(-4);

        // Assert - Database
        var storedCourse = dbContext.Problem.FirstOrDefault(x => x.Id == -4).IsResolved;
        storedCourse.ShouldBe(true);
    }


    private static Explorer.API.Controllers.Author.ProblemController CreateAuthorController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Author.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

    private static Explorer.API.Controllers.Tourist.ProblemController CreateTouristController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Tourist.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

    private static Explorer.API.Controllers.Administrator.ProblemController CreateAdministratorController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Administrator.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())

        {
            ControllerContext = BuildContext("-1")
        };
    }
}