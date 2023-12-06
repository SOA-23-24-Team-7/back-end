using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ITourSaleService
{
    Result<TourSaleResponseDto> Create(TourSaleCreateDto sale);
    Result<List<TourSaleResponseDto>> GetByAuthorId(long authorId);
    Result<TourSaleResponseDto> GetById(long id);
    Result<TourSaleResponseDto> Update(TourSaleUpdateDto sale);
    Result Delete(long id);
    Result<double?> GetDiscountForTour(long tourId);
}
