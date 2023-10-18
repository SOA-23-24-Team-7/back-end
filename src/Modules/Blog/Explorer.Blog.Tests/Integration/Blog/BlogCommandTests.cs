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
            var newEntity = new BlogDto
            {
                Title = "Neki naslov",
                Description = "Opis neki",
                Date = DateTime.Now.ToUniversalTime(),
                Pictures = new List<string> { "slika1.jpg", "slika2.jpg" }, // Primer slika
                Status = BlogStatus.Published
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BlogDto;
            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == newEntity.Title);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);

        }
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogDto
            {
                Description = "Test"
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
