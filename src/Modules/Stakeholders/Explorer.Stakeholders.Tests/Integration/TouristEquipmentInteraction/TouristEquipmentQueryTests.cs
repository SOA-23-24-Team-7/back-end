using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Stakeholders.API.Public;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public.Administration;
using Explorer.Stakeholders.API.Dtos.TouristEquipment;

namespace Explorer.Stakeholders.Tests.Integration.TouristEquipmentInteraction
{
    [Collection("Sequential")]
    public class TouristEquipmentQueryTests : BaseStakeholdersIntegrationTest
    {
        public TouristEquipmentQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAllTouristEquipment(0, 0).Result)?.Value as PagedResult<TouristEquipmentResponseDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2);
            result.TotalCount.ShouldBe(2);
        }

        private static TouristEquipmentController CreateController(IServiceScope scope)
        {
            return new TouristEquipmentController(scope.ServiceProvider.GetRequiredService<ITouristEquipmentService>(), scope.ServiceProvider.GetRequiredService<IEquipmentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
