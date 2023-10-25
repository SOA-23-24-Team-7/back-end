using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
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

namespace Explorer.Stakeholders.Tests.Integration.TourPreference;
[Collection("Sequential")]

public class TourPreferenceQueryTest : BaseStakeholdersIntegrationTest
{
    public TourPreferenceQueryTest(StakeholdersTestFactory factory) : base(factory) { }

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

    private static TourPreferenceController CreateController(IServiceScope scope)
    {
        return new TourPreferenceController(scope.ServiceProvider.GetRequiredService<ITourPreferenceService>());
    }
}
