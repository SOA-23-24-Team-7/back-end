using Explorer.API.Controllers;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;
using System.Xml.Linq;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class BundleCommandTests : BasePaymentsIntegrationTest
    {
        public BundleCommandTests(PaymentsTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData("test bundle 1", 500, new long[] { -8, -7 })]
        public void Creates(string name, long price, long[] tourIds)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new BundleCreationDto
            {
                Name = name,
                Price = price,
                TourIds = tourIds.ToList()
            };

            var authorId = -11;
            var claims = new[] { new Claim("id", authorId.ToString()) };
            var identity = new ClaimsIdentity(claims, "autor1");
            var user = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = user };
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BundleResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(name);
            result.Price.ShouldBe(price);
            result.AuthorId.ShouldBe(authorId);
            result.Status.ShouldBe("Draft");

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(b => b.Id == result.Id );
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.Price.ShouldBe(result.Price);
            storedEntity.Name.ShouldBe(result.Name);
            storedEntity.AuthorId.ShouldBe(result.AuthorId);
            storedEntity.Status.ToString().ShouldBe(result.Status);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var addedEntity = new BundleCreationDto
            {
                Name = "test bundle 2",
                Price = 450,
                TourIds = new List<long>() { 8, 9}
            };

            // Act
            var result = (ObjectResult)controller.Create(addedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Theory]
        [InlineData(-10, "test bundle 11111", 650, new long[] { -8 })]
        public void Edits(long id, string name, long price, long[] tourIds)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var editedEntity = new BundleEditDto
            {
                Name = name,
                Price = price,
                TourIds = tourIds.ToList()
            };

            var authorId = -11;
            var claims = new[] { new Claim("id", authorId.ToString()) };
            var identity = new ClaimsIdentity(claims, "autor1");
            var user = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = user };
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            // Act
            var result = ((ObjectResult)controller.Edit(id, editedEntity).Result)?.Value as BundleResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(name);
            result.Price.ShouldBe(price);
            result.AuthorId.ShouldBe(authorId);
            result.Status.ShouldBe("Draft");

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(b => b.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(id);
            storedEntity.Price.ShouldBe(result.Price);
            storedEntity.Name.ShouldBe(result.Name);
            storedEntity.AuthorId.ShouldBe(result.AuthorId);
            storedEntity.Status.ToString().ShouldBe(result.Status);
        }

        [Theory]
        [InlineData(-10, -11)]
        public void Publishes(long bundleId, long authorId)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var claims = new[] { new Claim("id", authorId.ToString()) };
            var identity = new ClaimsIdentity(claims, "autor1");
            var user = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = user };
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            // Act
            var result = ((ObjectResult)controller.Publish(bundleId).Result)?.Value as BundleResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe("Bundle1");
            result.Price.ShouldBe(25);
            result.AuthorId.ShouldBe(authorId);
            result.Status.ShouldBe("Published");

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(b => b.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.Price.ShouldBe(result.Price);
            storedEntity.Name.ShouldBe(result.Name);
            storedEntity.AuthorId.ShouldBe(result.AuthorId);
            storedEntity.Status.ToString().ShouldBe(result.Status);
        }

        private static BundleController CreateController(IServiceScope scope)
        {
            return new BundleController(scope.ServiceProvider.GetRequiredService<IBundleService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
