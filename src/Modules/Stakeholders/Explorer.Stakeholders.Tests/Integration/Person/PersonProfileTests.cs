using Explorer.API.Controllers;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Person
{
    [Collection("Sequential")]
    public class PersonProfileTests : BaseStakeholdersIntegrationTest
    {
        public PersonProfileTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Correct_update_profile_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var updatedEntity = new PersonUpdateDto
            {
                Id = -11,
                UserId = -11,
                Name = "Nikola",
                Surname = "Nikolic",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Motto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(updatedEntity, updatedEntity.Id).Result;
            var updateProfileResult = updateProfileResponse?.Value as PersonResponseDto;

            // Assert - Response
            updateProfileResponse.StatusCode.ShouldBe(200);
            updateProfileResult.ShouldNotBeNull();
            updateProfileResult.Name.ShouldBe(updatedEntity.Name);
            updateProfileResult.Surname.ShouldBe(updatedEntity.Surname);
            updateProfileResult.ProfilePicture.ShouldBe(updatedEntity.ProfilePicture);
            updateProfileResult.Bio.ShouldBe(updatedEntity.Bio);
            updateProfileResult.Motto.ShouldBe(updatedEntity.Motto);

            // Assert - Database
            var storedPersonEntity = dbContext.People.FirstOrDefault(i => i.Id == -11);
            var storedUserEntity = dbContext.Users.FirstOrDefault(i => i.Id == -11);
            storedPersonEntity.ShouldNotBeNull();
            storedPersonEntity.Name.ShouldBe(updatedEntity.Name);
            storedPersonEntity.Surname.ShouldBe(updatedEntity.Surname);
            storedUserEntity.ProfilePicture.ShouldBe(updatedEntity.ProfilePicture);
            storedPersonEntity.Bio.ShouldBe(updatedEntity.Bio);
            storedPersonEntity.Motto.ShouldBe(updatedEntity.Motto);
        }

        [Fact]
        public void Empty_update_profile_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var invalidEntity = new PersonUpdateDto
            {
                Id = -11,
                UserId = -11,
                Name = "",
                Surname = "",
                ProfilePicture = "",
                Bio = "",
                Motto = ""
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity, invalidEntity.Id).Result;

            // Assert
            updateProfileResponse.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Invalid_update_profile_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var invalidEntity = new PersonUpdateDto
            {
                Id = -9999,
                UserId = -11,
                Name = "Nikola",
                Surname = "Nikolic",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Motto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity, invalidEntity.Id).Result;

            // Assert
            updateProfileResponse.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Invalid_update_profile_name()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var invalidEntity = new PersonUpdateDto
            {
                Id = -11,
                UserId = -11,
                Name = "",
                Surname = "Nikolic",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Motto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity, invalidEntity.Id).Result;

            // Assert
            updateProfileResponse.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Invalid_update_profile_surname()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var invalidEntity = new PersonUpdateDto
            {
                Id = -11,
                UserId = -11,
                Name = "Nikola",
                Surname = "",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Motto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity, invalidEntity.Id).Result;

            // Assert
            updateProfileResponse.StatusCode.ShouldBe(400);
        }



        private static PersonController CreateController(IServiceScope scope)
        {
            return new PersonController(scope.ServiceProvider.GetRequiredService<IPersonService>());
        }
    }
}
