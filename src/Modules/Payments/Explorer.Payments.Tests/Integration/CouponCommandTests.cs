using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class CouponCommandTests : BasePaymentsIntegrationTest
    {
        public CouponCommandTests(PaymentsTestFactory factory) : base(factory) { }
        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new CouponCreateDto
            {
                Discount = 50,
                TourId = -1500, //ovo ce se mozda morati promijeniti ako dodamo da 1 tura ne smije imati vise kupona
                ExpirationDate = DateTime.UtcNow.AddDays(5),
                AllFromAuthor = false,
                AuthorId = -1000,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CouponResponseDto;

            // Assert - Response
           result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Discount.ShouldBe(newEntity.Discount);
            result.TourId.ShouldBe(newEntity.TourId);
            result.ExpirationDate.ShouldBe(newEntity.ExpirationDate);
            result.AllFromAuthor.ShouldBe(newEntity.AllFromAuthor);

            // Assert - Database
            var storedEntity = dbContext.Coupons.FirstOrDefault(i => i.Discount == newEntity.Discount);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.Discount.ShouldBe(result.Discount);
            storedEntity.TourId.ShouldBe(result.TourId);
            storedEntity.ExpirationDate.ToString().ShouldBe(result.ExpirationDate.ToString());
            storedEntity.AllFromAuthor.ShouldBe(result.AllFromAuthor);
        }
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var addedEntity = new CouponCreateDto
            {
                Discount = -12
            };

            // Act
            var result = (ObjectResult)controller.Create(addedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }






        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var updatedEntity = new CouponUpdateDto
            {
                Id = -2,
                Discount = 18,
                TourId = -1600, //ovo ce se mozda morati promijeniti ako dodamo da 1 tura ne smije imati vise kupona
                ExpirationDate = DateTime.UtcNow.AddDays(10),
                AllFromAuthor = false
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as CouponResponseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.Discount.ShouldBe(updatedEntity.Discount);
            result.TourId.ShouldBe(updatedEntity.TourId);
            result.ExpirationDate.ShouldBe(updatedEntity.ExpirationDate);
            result.AllFromAuthor.ShouldBe(updatedEntity.AllFromAuthor); 


            // Assert - Database
            var storedEntity = dbContext.Coupons.FirstOrDefault(i => i.Discount == 18);
            storedEntity.ShouldNotBeNull();
            storedEntity.Discount.ShouldBe(updatedEntity.Discount);
            storedEntity.TourId.ShouldBe(updatedEntity.TourId);
            storedEntity.ExpirationDate.ToString().ShouldBe(updatedEntity.ExpirationDate.ToString());   //javljalo gresku pa pretvorila u string
            storedEntity.AllFromAuthor.ShouldBe(updatedEntity.AllFromAuthor);
            var oldEntity = dbContext.Coupons.FirstOrDefault(i => i.Discount == 22);
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CouponUpdateDto
            {
                Id = -1000,
                TourId = -1500, //ovo ce se mozda morati promijeniti ako dodamo da 1 tura ne smije imati vise kupona
                ExpirationDate = DateTime.UtcNow.AddDays(5),
                AllFromAuthor = false
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Coupons.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static CouponController CreateController(IServiceScope scope)
        {
            return new CouponController(scope.ServiceProvider.GetRequiredService<ICouponService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
