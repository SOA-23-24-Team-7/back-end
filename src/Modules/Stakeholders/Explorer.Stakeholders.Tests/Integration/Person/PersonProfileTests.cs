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
            var updatedEntity = new PersonResponseDto
            {
                Id = -11,
                UserId = -11,
                Name = "Nikola",
                Surname = "Nikolic",
                Email = "nikola.nikolic@gmail.com",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Moto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(updatedEntity).Result;
            var updateProfileResult = updateProfileResponse?.Value as PersonResponseDto;

            // Assert - Response
            updateProfileResponse.StatusCode.ShouldBe(200);
            updateProfileResult.ShouldNotBeNull();
            updateProfileResult.Name.ShouldBe(updatedEntity.Name);
            updateProfileResult.Surname.ShouldBe(updatedEntity.Surname);
            updateProfileResult.Email.ShouldBe(updatedEntity.Email);
            updateProfileResult.ProfilePicture.ShouldBe(updatedEntity.ProfilePicture);
            updateProfileResult.Bio.ShouldBe(updatedEntity.Bio);
            updateProfileResult.Moto.ShouldBe(updatedEntity.Moto);

            // Assert - Database
            var storedEntity = dbContext.People.FirstOrDefault(i => i.Id == -11);
            storedEntity.ShouldNotBeNull();
            storedEntity.Name.ShouldBe(updatedEntity.Name);
            storedEntity.Surname.ShouldBe(updatedEntity.Surname);
            storedEntity.Email.ShouldBe(updatedEntity.Email);
            storedEntity.ProfilePicture.ShouldBe(updatedEntity.ProfilePicture);
            storedEntity.Bio.ShouldBe(updatedEntity.Bio);
            storedEntity.Moto.ShouldBe(updatedEntity.Moto);
        }

        [Fact]
        public void Empty_update_profile_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var invalidEntity = new PersonResponseDto
            {
                Id = -11,
                UserId = -11,
                Name = "",
                Surname = "",
                Email = "",
                ProfilePicture = "",
                Bio = "",
                Moto = ""
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity).Result;

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
            var invalidEntity = new PersonResponseDto
            {
                Id = -9999,
                UserId = -11,
                Name = "Nikola",
                Surname = "Nikolic",
                Email = "nikola.nikolic@gmail.com",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Moto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity).Result;

            // Assert
            updateProfileResponse.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Invalid_update_profile_userId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var invalidEntity = new PersonResponseDto
            {
                Id = -11,
                UserId = -9999,
                Name = "Nikola",
                Surname = "Nikolic",
                Email = "nikola.nikolic@gmail.com",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Moto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity).Result;

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
            var invalidEntity = new PersonResponseDto
            {
                Id = -11,
                UserId = -9999,
                Name = "",
                Surname = "Nikolic",
                Email = "nikola.nikolic@gmail.com",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Moto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity).Result;

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
            var invalidEntity = new PersonResponseDto
            {
                Id = -11,
                UserId = -9999,
                Name = "Nikola",
                Surname = "",
                Email = "nikola.nikolic@gmail.com",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Moto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity).Result;

            // Assert
            updateProfileResponse.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Invalid_update_profile_email_format()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var invalidEntity = new PersonResponseDto
            {
                Id = -11,
                UserId = -9999,
                Name = "Nikola",
                Surname = "Nikolic",
                Email = "nikola.nikolic",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Moto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity).Result;

            // Assert
            updateProfileResponse.StatusCode.ShouldBe(400);
        }

        public void Invalid_update_profile_email()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var invalidEntity = new PersonResponseDto
            {
                Id = -11,
                UserId = -9999,
                Name = "Nikola",
                Surname = "Nikolic",
                Email = "nikola.nikolic",
                ProfilePicture = "nikola.jpg",
                Bio = "Programator",
                Moto = "Clean code bajo"
            };

            // Act
            var updateProfileResponse = (ObjectResult)controller.Update(invalidEntity).Result;

            // Assert
            updateProfileResponse.StatusCode.ShouldBe(400);
        }

        private static PersonController CreateController(IServiceScope scope)
        {
            return new PersonController(scope.ServiceProvider.GetRequiredService<IPersonService>());
        }
    }
}
