

using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;
using Xunit;

namespace Explorer.Blog.Tests.Integration.Blog
{
    public class VoteCommandTests : BaseBlogIntegrationTest
    {
        public VoteCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Upvote()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "6") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            long blogId = 1;

            // Act
            var result = controller.Upvote(blogId);

            // Assert - Response
            result.ShouldBeOfType<OkResult>();

            // Assert - Database
            var storedEntity = dbContext.Blogs.Include(x => x.Votes).FirstOrDefault(i => i.Id == blogId);
            storedEntity.ShouldNotBeNull();
            storedEntity.VoteCount.ShouldBe(4);
            storedEntity.UpvoteCount.ShouldBe(5);
            storedEntity.DownvoteCount.ShouldBe(1);
        }

        [Fact]
        public void Upvote_already_upvoted_blog()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "6") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            long blogId = 2;

            // Act
            var result = controller.Upvote(blogId);

            // Assert - Response
            result.ShouldBeOfType<OkResult>();

            // Assert - Database
            var storedEntity = dbContext.Blogs.Include(x => x.Votes).FirstOrDefault(i => i.Id == blogId);
            storedEntity.ShouldNotBeNull();
            storedEntity.VoteCount.ShouldBe(0);
            storedEntity.UpvoteCount.ShouldBe(0);
            storedEntity.DownvoteCount.ShouldBe(0);
        }

        [Fact]
        public void Downvote()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "6") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            long blogId = 3;

            // Act
            var result = controller.Downvote(blogId);

            // Assert - Response
            result.ShouldBeOfType<OkResult>();

            // Assert - Database
            var storedEntity = dbContext.Blogs.Include(x => x.Votes).FirstOrDefault(i => i.Id == blogId);
            storedEntity.ShouldNotBeNull();
            storedEntity.VoteCount.ShouldBe(-1);
            storedEntity.UpvoteCount.ShouldBe(0);
            storedEntity.DownvoteCount.ShouldBe(1);
        }

        [Fact]
        public void Downvote_already_downvoted_blog()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "6") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            long blogId = 4;

            // Act
            var result = controller.Downvote(blogId);

            // Assert - Response
            result.ShouldBeOfType<OkResult>();


            // Assert - Database
            var storedEntity = dbContext.Blogs.Include(x => x.Votes).FirstOrDefault(i => i.Id == blogId);
            storedEntity.ShouldNotBeNull();
            storedEntity.VoteCount.ShouldBe(0);
            storedEntity.UpvoteCount.ShouldBe(0);
            storedEntity.DownvoteCount.ShouldBe(0);
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
