using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Campaign
{
    public class CampaignQueryTests : BaseToursIntegrationTest
    {

        public CampaignQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            /*
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetCampaign(-1).Result)?.Value as TourCampaignResponseDto;

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("kampanja");
            result.Distance.ShouldBe(0);*/

        }

        private static CampaignController CreateController(IServiceScope scope)
        {
            return new CampaignController(scope.ServiceProvider.GetRequiredService<ICampaignService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
