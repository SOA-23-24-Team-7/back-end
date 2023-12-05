using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class TourSaleService : BaseService<TourSale>, ITourSaleService
{
    private readonly ITourSaleRepository _saleRepository;

    private Action<TourSale> CheckIfTourIsAlreadyOnSale(TourSale sale)
    {
        return s => { if (s.Id != sale.Id && sale.EndDate >= s.StartDate && sale.StartDate <= s.EndDate && sale.TourIds.Any(t => s.TourIds.Contains(t))) throw new InvalidOperationException("At least one of the tours is already on sale."); };
    }

    public TourSaleService(IMapper mapper, ITourSaleRepository saleRepository) : base(mapper)
    {
        _saleRepository = saleRepository;
    }

    public Result<TourSaleResponseDto> Create(TourSaleCreateDto sale)
    {
        try
        {
            var saleDomain = MapToDomain(sale);
            List<TourSale> tourSales = _saleRepository.GetAll();
            tourSales.ForEach(CheckIfTourIsAlreadyOnSale(saleDomain));
            var result = _saleRepository.Create(saleDomain);
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

    public Result<List<TourSaleResponseDto>> GetByAuthorId(long authorId)
    {
        var result = _saleRepository.GetByAuthorId(authorId);
        return MapToDto<TourSaleResponseDto>(result);
    }

    public Result<TourSaleResponseDto> GetById(long id)
    {
        try
        {
            var result = _saleRepository.Get(id);
            return MapToDto<TourSaleResponseDto>(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<TourSaleResponseDto> Update(TourSaleUpdateDto sale)
    {
        try
        {
            var saleDomain = MapToDomain(sale);

            List<TourSale> tourSales = _saleRepository.GetAll();
            tourSales.ForEach(CheckIfTourIsAlreadyOnSale(saleDomain));

            var oldSale = tourSales.Find(s => s.Id == sale.Id);

            if (oldSale == null)
            {
                throw new KeyNotFoundException("Tour sale with this id does not exist.");
            }
            _saleRepository.Update(oldSale, saleDomain);
            return MapToDto<TourSaleResponseDto>(saleDomain);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
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

    public Result Delete(long id)
    {
        try
        {
            _saleRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}
