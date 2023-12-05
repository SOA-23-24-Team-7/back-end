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
    private readonly ICrudRepository<TourSale> _crudRepository;
    private readonly ITourSaleRepository _saleRepository;

    private Action<TourSale> CheckIfTourIsAlreadyOnSale(TourSale sale)
    {
        return s => { if (s.Id != sale.Id && sale.EndDate >= s.StartDate && sale.StartDate <= s.EndDate && sale.TourIds.Any(t => s.TourIds.Contains(t))) throw new InvalidOperationException("At least one of the tours is already on sale."); };
    }

    public TourSaleService(ICrudRepository<TourSale> crudRepository, IMapper mapper, ITourSaleRepository saleRepository) : base(mapper)
    {
        _crudRepository = crudRepository;
        _saleRepository = saleRepository;
    }

    public Result<TourSaleResponseDto> Create(TourSaleCreateDto sale)
    {
        try
        {
            var saleDomain = MapToDomain(sale);
            List<TourSale> tourSales = _crudRepository.GetAll();
            tourSales.ForEach(CheckIfTourIsAlreadyOnSale(saleDomain));
            var result = _crudRepository.Create(saleDomain);
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
            var result = _crudRepository.Get(id);
            return MapToDto<TourSaleResponseDto>(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<TourSaleResponseDto> Update(TourSaleUpdateDto sale, long authorId)
    {
        try
        {
            TourSale foundSale = _crudRepository.Get(sale.Id);

            if (foundSale.AuthorId != sale.AuthorId)
            {
                throw new InvalidOperationException("An author can only update their own sale.");
            }

            var saleDomain = MapToDomain(sale);

            List<TourSale> tourSales = _crudRepository.GetAll();
            tourSales.ForEach(CheckIfTourIsAlreadyOnSale(saleDomain));

            var result = _crudRepository.Update(saleDomain);
            return MapToDto<TourSaleResponseDto>(result);
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
            _crudRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}
