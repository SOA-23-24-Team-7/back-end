using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos.TouristEquipment;
using Explorer.Tours.API.Dtos.TouristEquipment;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Campaign
{
    public class CampaignCommandTests : BaseToursIntegrationTest
    {
        public CampaignCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new CampaignCreateDto
            {
                TouristId = -23,
                Name = "nova kampanja",
                Description = "opis",
                TourIds = new List<long> { -1, -2 }
            };

            var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-23") }, "test");
            var context = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(contextUser)
            };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };


            // Act
            var result = (controller.SaveCampaign(newEntity).Result as ObjectResult)?.Value as CampaignResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);
            result.Equipments.Count.ShouldBe(3);

            // Assert - Database
            var storedEntity = dbContext.Campaigns.FirstOrDefault(i => i.TouristId == newEntity.TouristId);
            storedEntity.ShouldNotBeNull();
            storedEntity.AverageDifficulty.ShouldBe(result.AverageDifficulty);
            storedEntity.Name.ShouldBe(result.Name);
            storedEntity.EquipmentIds.Count.ShouldBe(result.Equipments.Count);
            
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
