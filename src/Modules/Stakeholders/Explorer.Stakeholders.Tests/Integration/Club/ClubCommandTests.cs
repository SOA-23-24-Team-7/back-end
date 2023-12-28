using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Stakeholders.Tests.Integration.Club
{
    [Collection("Sequential")]
    public class ClubCommandTests : BaseStakeholdersIntegrationTest
    {
        public ClubCommandTests(StakeholdersTestFactory factory) : base(factory) { }
        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ClubCreateDto
            {
                OwnerId = -12,
                Name = "nekoime",
                Description = "opis",
                Image = "slika"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            // Assert - Database
            var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        [Fact]
        public void Create_fails_invalid_data()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-12") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            // Arrange

            var updatedEntity = new ClubCreateDto
            {
                Description = "Test"
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
            var updatedEntity = new ClubResponseDto
            {
                Id = -2,
                OwnerId = -11,
                Name = "izmenjenoime",
                Description = "izmenjen opis",
                Image = "izmenjena slika"
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ClubResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Description.ShouldBe(updatedEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == "izmenjenoime");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == "ime");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubResponseDto
            {
                Id = -1000,
                OwnerId = -1,
                Name = "Test",
                Description = "Test",
                Image = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        private static ClubController CreateController(IServiceScope scope)
        {

            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>(), scope.ServiceProvider.GetRequiredService<IClubMemberManagementService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
