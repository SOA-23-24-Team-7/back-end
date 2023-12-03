using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ITourSaleService
{
    Result<TourSaleResponseDto> Create(TourSaleCreateDto sale);
}
