using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases;

public class TourService : CrudService<TourResponseDto, Tour>, ITourService, IInternalTourService
{
    private readonly ICrudRepository<Tour> _repository;
    private readonly IMapper _mapper;
    private readonly ITourRepository _tourRepository;
    public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _tourRepository = tourRepository;
    }

    public Result<PagedResult<TourResponseDto>> GetAuthorsPagedTours(long authorId, int page, int pageSize)
    {
        var allTours = _repository.GetPaged(page, pageSize);
        var toursByAuthor = allTours.Results.Where(t => t.AuthorId == authorId).ToList();
        var pagedResult = new PagedResult<Tour>(toursByAuthor, toursByAuthor.Count);
        return MapToDto<TourResponseDto>(pagedResult);
    }

    public Result<PagedResult<EquipmentResponseDto>> GetEquipment(long tourId)
    {
        var equipment = _tourRepository.GetEquipment(tourId);
        var result = new PagedResult<Equipment>(equipment, equipment.Count);
        var items = result.Results.Select(_mapper.Map<EquipmentResponseDto>).ToList();
        return new PagedResult<EquipmentResponseDto>(items, result.TotalCount);
    }

    public Result AddEquipment(long tourId, long equipmentId)
    {
        try
        {
            _tourRepository.AddEquipment(tourId, equipmentId);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result DeleteEquipment(long tourId, int equipmentId)
    {
        try
        {
            _tourRepository.DeleteEquipment(tourId, equipmentId);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }


    public IEnumerable<long> GetAuthorsTours(long id)
    {
        return _tourRepository.GetAuthorsTours(id);
    }

    public string GetToursName(long id)
    {
        return _tourRepository.GetToursName(id);
    }

    public long GetAuthorsId(long id)
    {
        return _tourRepository.GetAuthorsId(id);
    }
}
