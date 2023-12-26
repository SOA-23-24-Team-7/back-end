using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases;

public class TourService : CrudService<TourResponseDto, Tour>, ITourService, IInternalTourService
{
    private readonly ICrudRepository<Tour> _repository;
    private readonly IMapper _mapper;
    private readonly ITourRepository _tourRepository;
    private readonly ICrudRepository<Tour> _tourCrudRepository;
    private readonly ITourExecutionSessionRepository _tourExecutionSessionRepository;
    private readonly IInternalProblemService _problemService;
    private readonly IReviewRepository _reviewRepository;
    private readonly IKeyPointRepository _keyPointRepository;
    private readonly ICrudRepository<PublicKeyPoint> _publicKeyPointRepository;

    public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository, ITourExecutionSessionRepository tourExecutionSessionRepository, IReviewRepository reviewRepository, IInternalProblemService problemService, IKeyPointRepository keyPointRepository, ICrudRepository<PublicKeyPoint> publicKeyPointRepository, ICrudRepository<Tour> tourCrudRepository) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _tourRepository = tourRepository;
        _problemService = problemService;
        _reviewRepository = reviewRepository;
        _tourExecutionSessionRepository = tourExecutionSessionRepository;
        _keyPointRepository = keyPointRepository;
        _publicKeyPointRepository = publicKeyPointRepository;
        _tourCrudRepository = tourCrudRepository;
    }

    public Result<PagedResult<TourResponseDto>> GetAuthorsPagedTours(long authorId, int page, int pageSize)
    {
        //var allTours = _repository.GetPaged(page, pageSize);
        var allTours = _tourRepository.GetAll(page, pageSize);  //anja dodala 
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

    public Result DeleteCascade(long tourId)
    {
        try
        {
            _problemService.DeleteProblemByTour(tourId);
            CrudRepository.Delete(tourId);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public List<long> GetAuthorsTours(long id)
    {
        return _tourRepository.GetAuthorsTours(id).ToList();
    }

    public string GetToursName(long id)
    {
        return _tourRepository.GetToursName(id);
    }

    public long GetAuthorsId(long id)
    {
        return _tourRepository.GetAuthorsId(id);
    }

    public Result<TourResponseDto> GetById(long id)
    {
        var entity = _tourRepository.GetById(id);
        var dto = MapToDto<TourResponseDto>(entity);
        return dto;
    }

    public Result Publish(long id, long authorId)
    {
        try
        {
            var entity = _tourRepository.GetById(id);
            if (entity.Publish(authorId))
            {
                _repository.Update(entity);
                return Result.Ok();
            }

            return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument provided.");
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result Archive(long id, long authorId)
    {
        try
        {
            var entity = _tourRepository.GetById(id);
            if (entity.Archive(authorId))
            {
                _repository.Update(entity);
                return Result.Ok();
            }

            return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument provided.");
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<PagedResult<TourResponseDto>> GetPublished(int page, int pageSize)
    {
        var allTours = _tourRepository.GetAll(page, pageSize);
        var publishedTours = allTours.Results.Where(t => t.Status == Domain.Tours.TourStatus.Published).ToList();
        var pagedResult = new PagedResult<Tour>(publishedTours, publishedTours.Count);
        var dtos = MapToDto<TourResponseDto>(pagedResult);
        for (int i = 0; i < publishedTours.Count; i++)
        {
            var averageRating = publishedTours.ElementAt(i).GetAverageRating();
            dtos.Value.Results.ElementAt(i).AverageRating = averageRating;
        }
        return dtos;
    }

    public Result<PagedResult<TourResponseDto>> GetAllPaged(int page, int pageSize)
    {
        var allTours = _tourRepository.GetAll(page, pageSize).Results;
        var pagedResult = new PagedResult<Tour>(allTours, allTours.Count);
        var dtos = MapToDto<TourResponseDto>(pagedResult);
        for (int i = 0; i < allTours.Count; i++)
        {
            var averageRating = allTours.ElementAt(i).GetAverageRating();
            dtos.Value.Results.ElementAt(i).AverageRating = averageRating;
        }
        return dtos;
    }

    public Result<bool> CanTourBeRated(long tourId, long userId)
    {
        var tourExecutions = _tourExecutionSessionRepository.GetAll(te => te.TourId == tourId &&
                                                                   (te.Status == Domain.TourExecutionSessionStatus.Completed || te.Status == Domain.TourExecutionSessionStatus.Abandoned) &&
                                                                    te.TouristId == userId && te.Progress >= 35 && (te.LastActivity > DateTime.UtcNow.AddDays(-7)) && !te.IsCampaign);
        return tourExecutions.Any();
    }

    public Result<PagedResult<LimitedTourViewResponseDto>> GetPublishedLimitedView(int page, int pageSize)
    {
        try
        {
            var result = _tourRepository.GetPublishedTours(page, pageSize);
            List<LimitedTourViewResponseDto> dtos = new List<LimitedTourViewResponseDto>();
            foreach (var tour in result.Results)
            {
                LimitedTourViewResponseDto dto = _mapper.Map<LimitedTourViewResponseDto>(tour);
                dto.KeyPoint = _mapper.Map<KeyPointResponseDto>(tour.KeyPoints.First());
                var reviews = _reviewRepository.GetPagedByTourId(0, 0, tour.Id);
                dto.Reviews = reviews.Results.Select(_mapper.Map<ReviewResponseDto>).ToList();
                dtos.Add(dto);
            }
            return new PagedResult<LimitedTourViewResponseDto>(dtos, dtos.Count);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
        catch (Exception ex)
        {
            return Result.Fail(FailureCode.Internal);
        }
    }
    public Result<PagedResult<LimitedTourViewResponseDto>> GetLimitedInfoTours(int page, int pageSize, List<long> ids)
    {
        try
        {
            List<LimitedTourViewResponseDto> dtos = new List<LimitedTourViewResponseDto>();
            foreach (var id in ids)
            {
                Tour tour = _tourRepository.GetById(id);

                LimitedTourViewResponseDto dto = _mapper.Map<LimitedTourViewResponseDto>(tour);
                dto.KeyPoint = _mapper.Map<KeyPointResponseDto>(tour.KeyPoints.First());
                var reviews = _reviewRepository.GetPagedByTourId(0, 0, tour.Id);
                dto.Reviews = reviews.Results.Select(_mapper.Map<ReviewResponseDto>).ToList();
                dtos.Add(dto);
            }
            return new PagedResult<LimitedTourViewResponseDto>(dtos, dtos.Count);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
        catch (Exception ex)
        {
            return Result.Fail(FailureCode.Internal);
        }
    }

    public Result<List<TourResponseDto>> GetTours(List<long> toursIds)
    {
        List<TourResponseDto> tourResponseDtos = new List<TourResponseDto>();
        foreach (long id in toursIds)
        {
            TourResponseDto tour = MapToDto<TourResponseDto>(_tourRepository.GetById(id));
            tourResponseDtos.Add(tour);
        }
        return tourResponseDtos;
    }

    public Result<TourResponseDto> Get(long id)
    {
        var entity = _tourRepository.GetById(id);
        var dto = MapToDto<TourResponseDto>(entity);
        return dto;
    }

    public Result MarkAsReady(long id, long touristId)
    {
        try
        {
            var entity = _tourRepository.GetById(id);
            if (entity.MarkAsReady(touristId))
            {
                _repository.Update(entity);
                return Result.Ok();
            }

            return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument provided.");
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    //Dobavlja ture koje imaju sve kljucne tacke koje je korisnik uzeo
    public Result<PagedResult<TourResponseDto>> GetToursBasedOnSelectedKeyPoints(int page, int pageSize, List<long> publicKeyPointIds, long authorId)
    {
        List<TourResponseDto> tourResponseDtos = new List<TourResponseDto>();
        var allTours = _tourRepository.GetAll(page, pageSize).Results.ToList();
        var publicKeyPoints = _publicKeyPointRepository.GetAll();
        int publicKeyPointNumber = publicKeyPointIds.Count;
        var wantedList = new List<KeyPoint>();

        foreach(long id in publicKeyPointIds)
        {
            wantedList.Add(_keyPointRepository.Get(id));
        }


        foreach (var tour in allTours)
        {
            if(tour.AuthorId != authorId && tour.GetStatusName() == "published")
            {
                int counter = 0;
                foreach (var publicKeyPoint in wantedList)
                {
                    foreach (var keyPoint in tour.KeyPoints)
                    {
                        if (publicKeyPoint.Name == keyPoint.Name && publicKeyPoint.Description == keyPoint.Description && publicKeyPoint.Longitude == keyPoint.Longitude && publicKeyPoint.Latitude == keyPoint.Latitude && publicKeyPoint.LocationAddress == keyPoint.LocationAddress && publicKeyPoint.ImagePath == keyPoint.ImagePath)
                        {
                            counter++;
                        }
                    }
                }
                if (counter == publicKeyPointNumber)
                {
                    TourResponseDto tourResponse = MapToDto<TourResponseDto>(tour);
                    tourResponseDtos.Add(tourResponse);
                }
            }
            
        }
        return new PagedResult<TourResponseDto>(tourResponseDtos, tourResponseDtos.Count);
    }
    public Result<PagedResult<TourResponseDto>> GetAdventureTours(int page, int pageSize)
    {
        var tours = _tourCrudRepository.GetAll(t => t.Status == Domain.Tours.TourStatus.Published && t.Category == Domain.Tours.TourCategory.Adventure, include: "Reviews,KeyPoints");
        //var allTours = _tourRepository.GetPopularAdventureTours(page, pageSize).Results.ToList();
        var pagedResult = new PagedResult<Tour>(tours, tours.Count);
        var dtos = MapToDto<TourResponseDto>(pagedResult);
        var newDtos = new List<TourResponseDto>();
        return dtos;
    }

    public Result<PagedResult<TourResponseDto>> GetFamilyTours(int page, int pageSize)
    {
        var tours = _tourCrudRepository.GetAll(t => t.Status == Domain.Tours.TourStatus.Published && t.Category == Domain.Tours.TourCategory.FamilyTrips, include: "Reviews,KeyPoints");
        //var allTours = _tourRepository.GetPopularFamilyTours(page, pageSize).Results.ToList();
        var pagedResult = new PagedResult<Tour>(tours, tours.Count);
        var dtos = MapToDto<TourResponseDto>(pagedResult);
        return dtos;
    }

    public Result<PagedResult<TourResponseDto>> GetCruiseTours(int page, int pageSize)
    {
        var tours = _tourCrudRepository.GetAll(t => t.Status == Domain.Tours.TourStatus.Published && t.Category==Domain.Tours.TourCategory.Cruise, include: "Reviews,KeyPoints");
        //var allTours = _tourRepository.GetPopularCruiseTours(page, pageSize).Results.ToList();
        var pagedResult = new PagedResult<Tour>(tours, tours.Count);
        var dtos = MapToDto<TourResponseDto>(pagedResult);
        return dtos;
    }

    public Result<PagedResult<TourResponseDto>> GetCulturalTours(int page, int pageSize)
    {
        var tours = _tourCrudRepository.GetAll(t => t.Status == Domain.Tours.TourStatus.Published && t.Category == Domain.Tours.TourCategory.Cultural, include: "Reviews,KeyPoints");
        //var allTours = _tourRepository.GetPopularCulturalTours(page, pageSize).Results.ToList();
        var pagedResult = new PagedResult<Tour>(tours, tours.Count);
        var dtos = MapToDto<TourResponseDto>(pagedResult);
        return dtos;
    }

    public List<long> GetKeyPointIds(long tourId)
    {
        List<long> ids = new();
        var tour = _tourRepository.GetById(tourId);
        
        foreach (var keyPoint in tour.KeyPoints)
        {
            ids.Add(keyPoint.Id);
        }

        return ids;
    }
}

