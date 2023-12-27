using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Explorer.Blog.Tests.Integration.Blog
{
    [Collection("Sequential")]
    public class RatingQueryTests : BaseBlogIntegrationTest
    {
        public RatingQueryTests(BlogTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (controller.Get(-2).Result as ObjectResult)?.Value as BlogResponseDto;

            // Assert
            result.ShouldNotBeNull();
            result.Votes.Count.ShouldBe(2);
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
