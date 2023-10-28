using Explorer.API.Controllers;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Explorer.Blog.Tests.Integration.Blog
{
    [Collection("Sequential")]
    public class BlogCommandTests : BaseBlogIntegrationTest
    {
        public BlogCommandTests(BlogTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new BlogCreateDto
            {
                Title = "Predlog",
                Description = "Test",
                Date = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Pictures = new List<string> { },
                Status = BlogStatus.Published
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BlogResponseDto;
            // Assert - Response
            result.ShouldNotBeNull();

            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);



        }
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogCreateDto
            {
                //Title ="Predlog",
                Description = "Test",
                Date = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Pictures = new List<string> { },
                Status = BlogStatus.Published
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }


        private static BlogController CreateController(IServiceScope scope)
        {
            return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
