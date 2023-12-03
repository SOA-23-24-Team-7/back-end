using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class TourSaleService : BaseService<TourSale>, ITourSaleService
{
    protected readonly ICrudRepository<TourSale> CrudRepository;

    private Action<TourSale> CheckIfTourIsAlreadyOnSale(TourSale sale)
    {
        return s => { if (sale.EndDate >= s.StartDate && sale.StartDate <= s.EndDate && sale.TourIds.Any(t => s.TourIds.Contains(t))) throw new InvalidOperationException("At least one of the tours is already on sale."); };
    }

    public TourSaleService(ICrudRepository<TourSale> crudRepository, IMapper mapper) : base(mapper)
    {
        CrudRepository = crudRepository;
    }

    public Result<TourSaleResponseDto> Create(TourSaleCreateDto sale)
    {
        try
        {
            var saleDomain = MapToDomain(sale);
            List<TourSale> tourSales = CrudRepository.GetAll();
            tourSales.ForEach(CheckIfTourIsAlreadyOnSale(saleDomain));
            var result = CrudRepository.Create(saleDomain);
            return MapToDto<TourSaleResponseDto>(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}
