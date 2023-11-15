using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;
using Xunit;

namespace Explorer.Blog.Tests.Integration.Blog
{
    public class CommentCommandTests : BaseBlogIntegrationTest
    {
        public CommentCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new CommentCreateDto()
            {
                BlogId = -1,
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed id metus diam. Donec neque orci, laoreet a sollicitudin vitae, bibendum a mauris."
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CommentResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.AuthorId.ShouldBe(-21);
            result.BlogId.ShouldBe(newEntity.BlogId);
            result.Text.ShouldBe(newEntity.Text);

            // Assert - Database
            var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.AuthorId.ShouldBe(-21);
            storedEntity.BlogId.ShouldBe(newEntity.BlogId);
            storedEntity.Text.ShouldBe(newEntity.Text);
        }

        [Fact]
        public void Create_fails_invalid_text()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new CommentCreateDto
            {
                BlogId = -1,
                Text = ""
            };

            // Act
            var response = (ObjectResult)controller.Create(newEntity).Result;
            var result = response?.Value as CommentResponseDto;

            // Assert - Response
            response.StatusCode.ShouldBe(400);
            result.ShouldBeNull();

            // Assert - Database
            var storedEntity = dbContext.Comments.FirstOrDefault(i => i.AuthorId == -21 && i.Text == newEntity.Text && i.BlogId == newEntity.BlogId);
            storedEntity.ShouldBeNull();
        }

        private static CommentController CreateController(IServiceScope scope)
        {
            return new CommentController(scope.ServiceProvider.GetRequiredService<ICommentService>(), scope.ServiceProvider.GetRequiredService<IBlogService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }

}
