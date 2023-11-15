using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Tours.Tests.Integration.Preference;
[Collection("Sequential")]

public class PreferenceQueryTest : BaseToursIntegrationTest
{
    public PreferenceQueryTest(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_one()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "3") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        // Act
        var result = (ObjectResult)controller.Get().Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);
    }

    private static PreferenceController CreateController(IServiceScope scope)
    {
        return new PreferenceController(scope.ServiceProvider.GetRequiredService<IPreferenceService>());
    }
}
