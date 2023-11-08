using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class RatingCommandTests : BaseStakeholdersIntegrationTest
    {
        public RatingCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new RatingCreateDto
            {
                UserId = -22,
                Grade = 4,
                Comment = "Okej je.",
                DateTime = DateTime.UtcNow
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as RatingResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Grade.ShouldBe(4);
            result.Comment.ShouldBe(newEntity.Comment);

            // Assert - Database
            var storedEntity = dbContext.Ratings.FirstOrDefault(i => i.Comment == newEntity.Comment);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new RatingCreateDto
            {
                Grade = 6
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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new RatingUpdateDto
            {
                Id = -1,
                UserId = -23,
                Grade = 3,
                Comment = "Dobro.",
                DateTime = DateTime.UtcNow
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as RatingResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Grade.ShouldBe(3);
            result.Comment.ShouldBe(updatedEntity.Comment);

            // Assert - Database
            var storedEntity = dbContext.Ratings.FirstOrDefault(i => i.Comment == "Dobro.");
            storedEntity.ShouldNotBeNull();
            storedEntity.Comment.ShouldBe(updatedEntity.Comment);
            var oldEntity = dbContext.Ratings.FirstOrDefault(i => i.Comment == "top");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new RatingUpdateDto
            {
                Id = -1000,
                Grade = 3,
                UserId = -23,
                DateTime = DateTime.UtcNow
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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Ratings.FirstOrDefault(i => i.Id == -2);
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

        private static RatingController CreateController(IServiceScope scope)
        {
            return new RatingController(scope.ServiceProvider.GetRequiredService<IRatingService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
