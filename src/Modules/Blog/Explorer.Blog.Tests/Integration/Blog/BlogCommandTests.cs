using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;
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
            // Arrangeusing var scope = Factory.Services.CreateScope();
            using var scope = Factory.Services.CreateScope();
            var controller = CreateBlogController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-12") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new BlogCreateDto
            {
                Title = "Predlog",
                Description = "Test",
                Date = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Status = API.Dtos.BlogStatus.Published,
                AuthorId = -12,
                VisibilityPolicy = 0
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
            var controller = CreateBlogController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-12") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };
            var updatedEntity = new BlogCreateDto
            {
                //Title ="Predlog",
                Description = "Test",
                Date = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Status = BlogStatus.Published,
                AuthorId = -12
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void UpdateBlogStatus()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controllerBlog1 = CreateBlogController(scope);
            var controllerBlog2 = CreateBlogController(scope);
            var controllerComment1 = CreateCommentController(scope);
            var controllerComment2 = CreateCommentController(scope);

            var contextUser1 = new ClaimsIdentity(new Claim[] { new Claim("id", "-11") }, "test1");
            var contextUser2 = new ClaimsIdentity(new Claim[] { new Claim("id", "-13") }, "test2");

            var context1 = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser1)
            };

            var context2 = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser2)
            };

            controllerBlog1.ControllerContext = new ControllerContext
            {
                HttpContext = context1
            };
            controllerBlog2.ControllerContext = new ControllerContext
            {
                HttpContext = context2
            };
            controllerComment1.ControllerContext = new ControllerContext
            {
                HttpContext = context1
            };
            controllerComment2.ControllerContext = new ControllerContext
            {
                HttpContext = context2
            };
            var newEntity1 = new CommentCreateDto()
            {
                BlogId = -1,
                Text = "Komentar 2"
            };

            // Act
            controllerBlog1.Upvote(-1);
            controllerBlog2.Upvote(-1);
            controllerComment1.Create(newEntity1);
            var result = controllerBlog1.Get(-1).Result as ObjectResult;

            // Assert
            result.ShouldNotBeNull();
            var resultValue = result.Value as BlogResponseDto;
            resultValue.Status.ShouldBe(API.Dtos.BlogStatus.Active);
        }

        private static BlogController CreateBlogController(IServiceScope scope)
        {
            return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>(),
                                      scope.ServiceProvider.GetRequiredService<IClubMemberManagementService>(),
                                      scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

        private static CommentController CreateCommentController(IServiceScope scope)
        {
            return new CommentController(scope.ServiceProvider.GetRequiredService<ICommentService>(), scope.ServiceProvider.GetRequiredService<IBlogService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
