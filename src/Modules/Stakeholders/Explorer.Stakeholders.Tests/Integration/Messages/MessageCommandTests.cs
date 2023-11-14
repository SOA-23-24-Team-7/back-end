using Explorer.API.Controllers;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Messages
{
    public class MessageCommandTests : BaseStakeholdersIntegrationTest
    {
        public MessageCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new MessageCreateDto
            {
                UserSenderId = -21,
                UserReciverId = -22,
                Text = "Okej je.",
                StatusOfMessage = MessageStatus.NotSeen
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as MessageResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.UserReciverId.ShouldBe(-22);
            result.Text.ShouldBe(newEntity.Text);

            // Assert - Database
            var storedEntity = dbContext.Messages.FirstOrDefault(m => m.Text == newEntity.Text);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Updates_Status()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new MessageUpdateDto
            {
                Id = -2,
                UserSenderId = -12,
                UserReciverId = -21,
                Text = "Nzm",
                StatusOfMessage = MessageStatus.NotSeen
            };

            // Act
            var result = ((ObjectResult)controller.UpdateStatus(updatedEntity).Result)?.Value as MessageResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.UserSenderId.ShouldBe(-12);
            result.UserReciverId.ShouldBe(-21);
            result.Text.ShouldBe(updatedEntity.Text);
            result.StatusOfMessage.ShouldBe(Core.Domain.MessageStatus.Seen.ToString());

            // Assert - Database
            var storedEntity = dbContext.Messages.FirstOrDefault(m => m.Text == "Nzm" && m.StatusOfMessage == Core.Domain.MessageStatus.Seen);
            storedEntity.ShouldNotBeNull();
            storedEntity.Text.ShouldBe(updatedEntity.Text);
            var oldEntity = dbContext.Messages.FirstOrDefault(m => m.Text == "Nzm" && m.StatusOfMessage == Core.Domain.MessageStatus.NotSeen);
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_status_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new MessageUpdateDto
            {
                Id = -1000,
                UserSenderId = -21,
                UserReciverId = -11,
                Text = "nesto",
                StatusOfMessage = MessageStatus.NotSeen
            };

            // Act
            var result = (ObjectResult)controller.UpdateStatus(updatedEntity).Result;

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
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Messages.FirstOrDefault(i => i.Id == -3);
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
        private static MessageController CreateController(IServiceScope scope)
        {
            return new MessageController(scope.ServiceProvider.GetRequiredService<IUserService>(), scope.ServiceProvider.GetRequiredService<IMessageService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
