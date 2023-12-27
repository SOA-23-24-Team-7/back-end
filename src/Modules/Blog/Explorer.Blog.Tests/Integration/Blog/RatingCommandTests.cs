using Explorer.API.Controllers;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Explorer.Blog.Tests.Integration.Blog
{
    [Collection("Sequential")]
    public class RatingCommandTests : BaseBlogIntegrationTest
    {
        public RatingCommandTests(BlogTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void GetUpvote()
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
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = controller.Upvote(-1).ToResult();
            // Assert - Response
            result.Value.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Id == -1);
            storedEntity.ShouldNotBeNull();
            storedEntity.Votes.FirstOrDefault(v => v.UserId == -12 && v.VoteType == Core.Domain.VoteType.UPVOTE).ShouldNotBeNull();
        }

        [Fact]
        public void GetDownvote()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-13") }, "test");

            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = controller.Downvote(-1).ToResult();
            // Assert - Response
            result.Value.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Id == -1);
            storedEntity.ShouldNotBeNull();
            storedEntity.Votes.FirstOrDefault(v => v.UserId == -13 && v.VoteType == Core.Domain.VoteType.DOWNVOTE).ShouldNotBeNull();
        }

        private static BlogController CreateController(IServiceScope scope)
        {
            return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>(),
                                      scope.ServiceProvider.GetRequiredService<IClubMemberManagementService>(),
                                      scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
